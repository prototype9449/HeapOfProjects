using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace LessonLibrary
{
    [Serializable]
    public class Lesson
    {
       
        
        public List<IData> DataList { get; private set; }

        public string Title { get; set; }
        public string Password { get; private set;}
    
        
        public Lesson(string title, string password)
        {
            Title = title;
            Password = password;
            DataList = new List<IData>();
        }

        public Lesson(string title, string password, List<IData> dataList) : this(title,password)
        {
            DataList = dataList;
        }

        public Lesson(string pathToFile)
        {
            var formatter = new BinaryFormatter();
            using (var fileStream = File.OpenRead(pathToFile))
            {
                var currentLesson = (Lesson)formatter.Deserialize(fileStream);

                DataList = currentLesson.DataList;
                Title = currentLesson.Title;
                Password = currentLesson.Password;
            }
        }

        public void ClearData()
        {
            DataList.Clear();
        }
        
        public void AddDataItem(IData itemData)
        {
            DataList.Add(itemData);
        }

        public void SaveToFile(string pathToFile)
        {
            var formatter = new BinaryFormatter();
            using (var fStream = new FileStream(pathToFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, this);
            } 
        }

        public List<int> GetRightAnswers()
        {
            return DataList.OfType<TestInfo>().Select(data => data.NumberRightAnswer).ToList();
        }
    }
}