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
            string sqlInsertLessonsInfo =
                string.Format("Insert Into LessonsInfo (Title, DateCreate, Size) Values (@Title, @DateCreate, @Size)");
            string sqlInsertBinaryData =
                string.Format("Insert into BinaryData (id, Data) Values ((select COUNT(id) from LessonsInfo), @Data)");
            string sqlInsertAutors =
                string.Format("Insert into Autors (id, Autor) Values ((select COUNT(id) from LessonsInfo), @Autor)");
            
            var lessonInfo = new LessonInfo(lesson);
            GetCommandeInsert(sqlInsertLessonsInfo, lessonInfo).ExecuteNonQuery();
            GetCommandeInsert(sqlInsertBinaryData, lessonInfo).ExecuteNonQuery();
            GetCommandeInsert(sqlInsertAutors, lessonInfo).ExecuteNonQuery();
            
        }

        public SqlCommand GetCommandeInsert(string sqlCommande, LessonInfo lessonInfo)
        {
            var command = new SqlCommand(sqlCommande, _sqlConnection);
            command.Parameters.AddWithValue("@Title", lessonInfo.Title);
            command.Parameters.AddWithValue("@Autor", lessonInfo.Autor);
            command.Parameters.AddWithValue("@Size", lessonInfo.Size);
            command.Parameters.AddWithValue("@Data", lessonInfo.Data);
            command.Parameters.AddWithValue("@DateCreate", lessonInfo.DateCreate);
            return command;
        }

        public SqlCommand GetCommandSelectById(string sqlCommand, int id)
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
                    lessons.Add(LessonInfo.GetLesson(data));
                }
            }
            return lessons;
        }

        private Lesson GetLessonById(int id)
        {
            string sqlSelectById =
                string.Format(
                    "select LessonsInfo.Title, LessonsInfo.Size, LessonsInfo.DateCreate, BinaryData.Data, Autors.Autor from LessonsInfo, Autors, BinaryData " +
                    "where LessonsInfo.id = BinaryData.id and LessonsInfo.id = Autors.id and LessonsInfo.id = @id");

            var lessons = GetLessonsByCommand(GetCommandSelectById(sqlSelectById, id));
            if(lessons.Count > 1)
                throw new InvalidDataException("There are lessons more than one in the data base");

            return lessons.First();
        }

    }
}
