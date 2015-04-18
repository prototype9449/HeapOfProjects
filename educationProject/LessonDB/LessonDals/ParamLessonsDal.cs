using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using LessonLibrary;

namespace LessonDB.LessonDals
{
    public class ParamLessonsDal : LessonsDal
    {
        protected override sealed SqlConnection _sqlConnection { get; set; }

        public ParamLessonsDal(SqlConnectionStringBuilder builder)
        {
            _sqlConnection = new SqlConnection(builder.ConnectionString);
        }
        public ParamLessonsDal(string cnString)
        {
            _sqlConnection = new SqlConnection(cnString);
        }
        public void Dispose()
        {
            _sqlConnection.Close();
        }
        public override void InsertLesson(Lesson lesson)
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
        public override Lesson GetLessonById(int id)
        {
            string sqlSelectById =
                string.Format("select Title, Size, DateCreate, Data, Autor from Lessons where id = @id");

            var lessons = GetLessonsByCommand(SetParamForCommand(sqlSelectById, id));

            if (lessons.Count > 1)
                throw new InvalidDataException("There are lessons more than one in the data base");

            return lessons.First();
        }

        public override void DeleteLessonById(int id)
        {
            string sqlDeleteById = string.Format("delete from Lessons where id = @id");

            _sqlConnection.Open();
            SetParamForCommand(sqlDeleteById, id).ExecuteNonQuery();
            _sqlConnection.Close();
        }

        private string GetConditionLike(string value)
        {
            return string.Format("like CONCAT('%', @{0}, '%')", value);
        }

        private string GetConditionalEquals(string value)
        {
            return string.Format("=@{0}", value);
        }
        private List<LessonInfoId> GetLessonsByFields(List<KeyValuePair<TypeSearch, string>> parameters, Func<string, string> getConditional)
        {
            string sqlSelect = string.Format("select id, Title, Autor, DateCreate from Lessons where ");
            if (parameters.Count == 0) throw new ArgumentNullException("Amount parameters equals zero");

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

        public override List<LessonInfoId> GetLessonsByFieldsExactly(List<KeyValuePair<TypeSearch, string>> parameters)
        {
            return GetLessonsByFields(parameters, GetConditionalEquals);
        }

        public override List<LessonInfoId> GetLessonsByFieldsLikely(List<KeyValuePair<TypeSearch, string>> parameters)
        {
            return GetLessonsByFields(parameters, GetConditionLike);
        }
    }
}
