using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LessonDB
{
    public class LessonInfoId : LessonInfo
    {
        [XmlAttribute]
        public int Id { get; set; }

        public LessonInfoId(string title, string autor, int id, DateTime dateCreate)
        {
            Id = id;
            Title = title;
            Autor = autor;
            DateCreate = dateCreate;
        }
    }
}
