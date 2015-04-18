using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using LessonDB.LessonDals;
using LessonLibrary;
namespace LessonDB
{
    public abstract class LessonsDal
    {
        public abstract void InsertLesson(Lesson lesson);
        public abstract Lesson GetLessonById(int id);
        public abstract void DeleteLessonById(int id);

        protected abstract SqlConnection _sqlConnection { get; set; }

        protected List<Lesson> GetLessonsByCommand(SqlCommand command)
        {
            _sqlConnection.Open();
            var lessons = new List<Lesson>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    byte[] data = (byte[]) reader["Data"];
                    lessons.Add(LessonsInfoData.GetLesson(data));
                }
            }
            _sqlConnection.Close();
            return lessons;
            
        }
        protected List<LessonInfoId> GetLessonsInfoByCommand(SqlCommand command)
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
        protected string GetNameField(TypeSearch typeSearch)
        {
            switch (typeSearch)
            {
                case TypeSearch.Title:
                    return "Title";
                case TypeSearch.Autor:
                    return "Autor";
                case TypeSearch.DateCreate:
                    return "DateCreate";
            }
            throw new InstanceNotFoundException();
        }

        public abstract List<LessonInfoId> GetLessonsByFieldsExactly(List<KeyValuePair<TypeSearch, string>> parameters);

        public abstract List<LessonInfoId> GetLessonsByFieldsLikely(List<KeyValuePair<TypeSearch, string>> parameters);


    }
}
