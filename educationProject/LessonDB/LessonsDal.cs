using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using LessonLibrary;
namespace LessonDB
{
    public class LessonsDal : IDisposable
    {
        private SqlConnection _sqlConnection;

        public LessonsDal(SqlConnectionStringBuilder builder)
        {
            _sqlConnection = new SqlConnection(builder.ConnectionString);
        }
        public LessonsDal(string cnString)
        {
            _sqlConnection = new SqlConnection(cnString);
        }
        public void Dispose()
        {
            _sqlConnection.Close();
        }

        public void InsertLesson(Lesson lesson)
        {
            _sqlConnection.Open();

            string sqlInsertLesson =
                string.Format("Insert Into Lessons (Title, DateCreate, Size, Data,  Autor) Values (@Title, @DateCreate, @Size, @Data, @Autor)");
            var lessonInfo = new LessonsInfoData(lesson);
            GetCommandeInsert(sqlInsertLesson, lessonInfo).ExecuteNonQuery();

            _sqlConnection.Close();
        }

        private SqlCommand GetCommandeInsert(string sqlCommande, LessonsInfoData lessonInfo)
        {
            var command = new SqlCommand(sqlCommande, _sqlConnection);
            command.Parameters.AddWithValue("@Title", lessonInfo.Title);
            command.Parameters.AddWithValue("@Autor", lessonInfo.Autor);
            command.Parameters.AddWithValue("@Size", lessonInfo.Size);
            command.Parameters.AddWithValue("@Data", lessonInfo.Data);
            command.Parameters.AddWithValue("@DateCreate", lessonInfo.DateCreate);
            return command;
        }

        private SqlCommand SetParamForCommand(string sqlCommand, int id)
        {
            var command = new SqlCommand(sqlCommand, _sqlConnection);
            command.Parameters.AddWithValue("@id", id);
            return command;
        }

        private List<Lesson> GetLessonsByCommand(SqlCommand command)
        {
            var lessons = new List<Lesson>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    byte[] data = (byte[]) reader["Data"];
                    lessons.Add(LessonsInfoData.GetLesson(data));
                }
            }
            return lessons;
        }

        public Lesson GetLessonById(int id)
        {
            _sqlConnection.Open();

            string sqlSelectById =
                string.Format("select Title, Size, DateCreate, Data, Autor from Lessons where id = @id");

            var lessons = GetLessonsByCommand(SetParamForCommand(sqlSelectById, id));

            _sqlConnection.Close();

            if(lessons.Count > 1)
                throw new InvalidDataException("There are lessons more than one in the data base");


            return lessons.First();
        }

        public void DeleteLessonById(int id)
        {
            string sqlSelectById = string.Format( "delete from Lessons where id = @id");

            _sqlConnection.Open();
            SetParamForCommand(sqlSelectById, id).ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public List<LessonInfoId> GetLessonsByFieldsExactly(List<KeyValuePair<TypeSearch, string>> parameters)
        {
            return GetLessonsByFields(parameters, GetConditionalEquals);
        }

        public List<LessonInfoId> GetLessonsByFieldsLikely(List<KeyValuePair<TypeSearch, string>> parameters)
        {
            return GetLessonsByFields(parameters, GetConditionLike);
        }

        private string GetConditionLike(string value)
        {
            return string.Format("like CONCAT('%', @{0}, '%')", value);
        }

        private string GetConditionalEquals(string value)
        {
            return string.Format("=@{0}", value);
        }
        private  List<LessonInfoId> GetLessonsByFields(List<KeyValuePair<TypeSearch, string>> parameters, Func<string, string> getConditional)
        {
            string sqlSelect = string.Format("select id, Title, Autor, DateCreate from Lessons where ");
            if(parameters.Count == 0) throw new ArgumentNullException("Amount parameters equals zero");

            sqlSelect += string.Format("{0} {1}", GetNameField(parameters[0].Key), getConditional(GetNameField(parameters[0].Key)));
            for (int i = 1; i < parameters.Count; i++)
            {
                sqlSelect += string.Format(" and {0} {1}", GetNameField(parameters[i].Key), getConditional(GetNameField(parameters[i].Key)));
            }
            var command = new SqlCommand(sqlSelect, _sqlConnection);

            for (int i = 0; i < parameters.Count; i++)
            {
                command.Parameters.AddWithValue("@" + GetNameField(parameters[i].Key), parameters[i].Value);
            }
            return GetLessonsInfoByCommand(command);
        }

        private List<LessonInfoId> GetLessonsInfoByCommand(SqlCommand command)
        {
            _sqlConnection.Open();

            var lessons = new List<LessonInfoId>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var title = reader["Title"].ToString();
                    var autor = reader["Autor"].ToString();
                    var dateCreate = (DateTime)reader["DateCreate"];
                    lessons.Add(new LessonInfoId(title, autor, id, dateCreate));
                }
            }
            _sqlConnection.Close();

            return lessons;
        }

        private string GetNameField(TypeSearch typeSearch)
        {
            switch (typeSearch)
            {
                case TypeSearch.Title :
                    return "Title";
                case TypeSearch.Autor :
                    return "Autor";
                case TypeSearch.DateCreate:
                    return "DateCreate";
            }
            throw new InstanceNotFoundException();
        }

    }
}
