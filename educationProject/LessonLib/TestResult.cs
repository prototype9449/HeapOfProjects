using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessonLibrary
{
    [Serializable]
    public class TestResult
    {
        public int CountRightAnswer { get; private set; }
        public int CountAllAnswer { get; private set; }

        public DateTime EndTime { get; private set; }
        public TimeSpan PerformTime { get; private set; }

        public TestResult(int countRightAnswer, int countAllAnswer, DateTime timeOfEnd)
        {
            CountRightAnswer = countRightAnswer;
            CountAllAnswer = countAllAnswer;
            EndTime = timeOfEnd;
        }
    }
}
