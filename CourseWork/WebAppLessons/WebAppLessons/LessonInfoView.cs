using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LessonDB;

namespace WebAppLessons
{
    public class LessonInfoView
    {
        private static List<LessonInfoId> _lessons;

        public static List<LessonInfoId> Lessons
        {
            get { return _lessons; }
            set { _lessons = value; }
        }

        // Получение всех элементов из источника данных
        public List<LessonInfoId> SelectAllProducts()
        {
            return Lessons;
        }
        

        // Удаление элемента из источника данных.
        public void RemoveProduct(LessonInfoId p)
        {
            Lessons.Remove(p);
        }
    }
}