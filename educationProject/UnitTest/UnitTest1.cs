using System.Collections.Generic;
using LessonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var less =new Lesson("Title", "fsdf");
            var image = new ImageInfo(@"C:\image.jpg");
            var text = new TextInfo("this is text");
            var test = new TestInfo(text, new List<string>{"sdf","fsdf","fsdf","fsdf"},1);
            less.AddDataItem(image);
            less.AddDataItem(text);
            less.AddDataItem(test);
            less.SaveToFile(@"F:\lesson.dat");

            var lessOpen = new Lesson(@"F:\lesson.dat");

            var e = 0;
            Assert.AreEqual(1,1);
        }


    }
}
