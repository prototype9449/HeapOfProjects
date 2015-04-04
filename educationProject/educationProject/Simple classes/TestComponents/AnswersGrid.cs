using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace educationProject
{
    public class AnswersGrid : Grid
    {
        public const int CountAnswers = 4;

        public AnswersGrid()
        {
            var groupName = GetHashCode().ToString();

            var stackPanel = new StackPanel();
            for (var i = 0; i < CountAnswers; i++)
            {
                stackPanel.Children.Add(GetRadioButtonTextBoxGrid(i, groupName));
            }

            Children.Add(stackPanel);
            Background = new SolidColorBrush(Colors.Transparent);
        }

        public AnswersGrid(List<string> answers, int numberRightAnswer, bool userView)
        {
            var groupName = GetHashCode().ToString();

            var stackPanel = new StackPanel();
            for (var i = 0; i < CountAnswers; i++)
            {
                stackPanel.Children.Add(GetRadioButtonTextBoxGrid(i, groupName, numberRightAnswer, answers[i], userView));
            }

            Children.Add(stackPanel);
            Background = new SolidColorBrush(Colors.Transparent);
        }

        public int GetEnableNumberAnswer()
        {
            var answers = (Children[0] as StackPanel).Children;
            for(var i =0; i < answers.Count; i++)
            {
                if ((answers[i] as RadioButTextGrid).IsEnable())
                {
                    return i;
                }
            }
            return -1;
        }

        public List<string> GetAnswers()
        {
            var answers = (Children[0] as StackPanel).Children;

            var result = new List<string>();
            foreach(var grid in answers)
            {
                result.Add((grid as RadioButTextGrid).GetText());
            }
            return result;
        }

        private RadioButTextGrid GetRadioButtonTextBoxGrid(int number, string groupName, int numberRightAnswer = 0, string answer = "", bool userView = false)
        {
            return new RadioButTextGrid(number, isEnable: number == numberRightAnswer,
                answer: answer, groupName: groupName, userView: userView);
        }
       
    }
}
