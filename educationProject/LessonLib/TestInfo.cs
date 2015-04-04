using System;
using System.Collections.Generic;

namespace LessonLibrary
{
    [Serializable]
    public class TestInfo : IData
    {
        public string Question { get; private set; }
        public int NumberRightAnswer { get; private set; }
        public List<string> Answers { get; private set; }

        public TestInfo(string question, List<string> answers, int numberRightAnswer)
        {
            Question = question;
            NumberRightAnswer = numberRightAnswer;
            Answers = answers;
        }

        public bool IsRight(int numberAnswer)
        {
            return numberAnswer == NumberRightAnswer;
        }
    }
}
