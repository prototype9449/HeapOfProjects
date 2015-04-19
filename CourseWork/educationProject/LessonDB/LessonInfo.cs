using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LessonLibrary;

namespace LessonDB
{
    public class LessonInfo
    {
        [XmlAttribute]
        public string Title { get; set; }
        [XmlAttribute]
        public string Autor { get; set; }
        [XmlAttribute]
        public DateTime DateCreate { get; set; }
    }
}
