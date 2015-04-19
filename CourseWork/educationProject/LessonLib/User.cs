using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LessonLibrary
{
    [Serializable]
    public class User : IEquatable<User>
    {

        public string Name { get; private set; }

        public string LessonName { get; private set; }

        public int CountRightAnswer { get; private set; }
        public int CountAllAnswer { get; private set; }

        public DateTime EndTime { get; private set; }
        public TimeSpan PerformTime { get; private set; }

        public User() { }

        public User(string name, string lesName, int count_RightAnswer, int count_AllAnswer, DateTime TimeOfEnd, TimeSpan Perform)
        {
            Name = name;
            LessonName = lesName;
            CountRightAnswer = count_RightAnswer;
            CountAllAnswer = count_AllAnswer;
            EndTime = TimeOfEnd;
            PerformTime = Perform;
        }

        public bool Equals(User other)
        {
            return other.Name == Name;
        }
    }

}
