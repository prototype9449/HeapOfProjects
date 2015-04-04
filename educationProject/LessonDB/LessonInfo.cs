using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using LessonLibrary;

namespace LessonDB
{
    public class LessonInfo
    {
        public string Title;
        public string Autor;
        public DateTime DateCreate;
        public string Size;
        public byte[] Data;

        public LessonInfo(Lesson lesson)
        {
            Title = lesson.Title;
            Autor = lesson.Autor;
            DateCreate = lesson.DateCreate;
            BuildByteArray(lesson);
        }

        public static Lesson GetLesson(byte[] data)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(data))
            {
                var lesson = (Lesson) formatter.Deserialize(stream);
                return lesson;
            }
        }

        private void BuildByteArray(Lesson lesson)
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, lesson);
            Data = stream.GetBuffer();
            Size = Data.Length.ToString();
        }
    }
}
