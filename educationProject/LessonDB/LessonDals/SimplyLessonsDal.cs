using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using LessonLibrary;

namespace LessonDB.LessonDals
{
    public class SimplyLessonsDal :LessonsDal
    {
        protected override sealed SqlConnection _sqlConnection { get; set; }

        public SimplyLessonsDal(SqlConnectionStringBuilder builder)
        {
            _sqlConnection = new SqlConnection(builder.ConnectionString);
        }
        public SimplyLessonsDal(string cnString)
        {
            _sqlConnection = new SqlConnection(cnString);
        }
        public void Dispose()
        {
            _sqlConnection.Close();
        }
        public override void InsertLesson(Lesson lesson)
        {
            var lessonInfo = new LessonsInfoData(lesson);
            string sqlInsertLesson =
                string.Format("Insert Into Lessons (Title, DateCreate, Size, Data,  Autor) Values ({0}, {1}, {2}, {3}, {4})",
                lessonInfo.Title, lessonInfo.DateCreate, lessonInfo.Size, lessonInfo.Data, lessonInfo.Autor);
            var command = new SqlCommand(sqlInsertLesson, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public override Lesson GetLessonById(int id)
        {
            string sqlSelectById =
                string.Format("select Title, Size, DateCreate, Data, Autor from Lessons where id = {0}",id);
            var command = new SqlCommand(sqlSelectById);
            var lessons = GetLessonsByCommand(command);
            return lessons.First();
        }

        public override void DeleteLessonById(int id)
        {
            string sqlDeletetById = string.Format("delete from Lessons where id = {0}",id);
            _sqlConnection.Open();
            new SqlCommand(sqlDeletetById).ExecuteNonQuery();
            _sqlConnection.Close();
        }

        private string GetConditionLike(string value)
        {
            return string.Format("like CONCAT('%', {0}, '%')", value);
        }

        private string GetConditionalEquals(string value)
        {
            return string.Format("='{0}'", value);
        }
        private List<LessonInfoId> GetLessonsByFields(List<KeyValuePair<TypeSearch, string>> parameters, Func<string, string> getConditional)
        {
            string sqlSelect = string.Format("select id, Title, Autor, DateCreate from Lessons where ");
            if (parameters.Count == 0) throw new ArgumentNullException("Amount parameters equals zero");

            sqlSelect += string.Format("{0} {1}", GetNameField(parameters[0].Key), getConditional(parameters[0].Value));
            for (int i = 1; i < parameters.Count; i++)
            {
                sqlSelect += string.Format(" and {0} {1}", GetNameField(parameters[i].Key), getConditional(parameters[0].Value));
            }
            var command = new SqlCommand(sqlSelect, _sqlConnection);
            
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
