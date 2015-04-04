using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonDB
{
    public class LessonInfoId : LessonInfo
    {
        public int Id;

        public LessonInfoId(string title, string autor, int id, DateTime dateCreate)
        {
            Id = id;
            Title = title;
            Autor = autor;
            DateCreate = dateCreate;
        }
    }
}
