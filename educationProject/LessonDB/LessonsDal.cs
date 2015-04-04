using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LessonLibrary;
namespace LessonDB
{
    public class LessonsDal : IDisposable
    {
        private SqlConnection _sqlConnection;

        public void OpenConnection(string connectionString)
        {
            _sqlConnection = new SqlConnection { ConnectionString = connectionString };
            _sqlConnection.Open();
        }
        public void CloseConnection()
        {
            _sqlConnection.Close();
        }

        public void Dispose()
        {
            _sqlConnection.Close();
        }

        public void InsertLesson(Lesson lesson)
        {
            string sqlInsertLesson =
                string.Format("Insert Into Lessons (Title, DateCreate, Size, Data,  Autor) Values (@Title, @DateCreate, @Size, @Data, @Autor)");
            var lessonInfo = new LessonsInfoData(lesson);
            GetCommandeInsert(sqlInsertLesson, lessonInfo).ExecuteNonQuery();
        }

        public SqlCommand GetCommandeInsert(string sqlCommande, LessonsInfoData lessonInfo)
        {
            var command = new SqlCommand(sqlCommande, _sqlConnection);
            command.Parameters.AddWithValue("@Title", lessonInfo.Title);
            command.Parameters.AddWithValue("@Autor", lessonInfo.Autor);
            command.Parameters.AddWithValue("@Size", lessonInfo.Size);
            command.Parameters.AddWithValue("@Data", lessonInfo.Data);
            command.Parameters.AddWithValue("@DateCreate", lessonInfo.DateCreate);
            return command;
        }

        public SqlCommand GetCommandById(string sqlCommand, int id)
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
            string sqlSelectById =
                string.Format("select Title, Size, DateCreate, Data, Autor from Lessons");

            var lessons = GetLessonsByCommand(GetCommandById(sqlSelectById, id));
            if(lessons.Count > 1)
                throw new InvalidDataException("There are lessons more than one in the data base");

            return lessons.First();
        }

        public void DeleteLessonById(int id)
        {
            string sqlSelectById = string.Format( "delete from Lessons where id = @id");
            GetCommandById(sqlSelectById, id).ExecuteNonQuery();
        }

        public List<LessonInfoId> GetLessonsInfoByField(string field, string value)
        {
            string sqlSelect = string.Format("select id, Title, Autor, DateCreate from Lessons where {0} = @{1}", field, field);
            var command = new SqlCommand(sqlSelect, _sqlConnection);
            command.Parameters.AddWithValue("@" + field, value);

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
            return lessons;
        }

    }
}
