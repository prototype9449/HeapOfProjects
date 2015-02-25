using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace educationProject
{
    public class RadioButTextGrid : Grid
    {
        public RadioButTextGrid(int number, bool isEnable,string answer, string groupName, bool userView)
        {
            TextBox answerTextBox;
            if (answer == "")
            {
                answerTextBox = FactoryTextBox.GetTextBoxAnswer(number, !userView);
            }
            else
            {
                answerTextBox = FactoryTextBox.GetTextBoxDefault(answer, !userView);
            }

            answerTextBox.IsEnabled = !userView;

            var radioButton = new RadioButton()
            {
                ToolTip = "Выберите правильный ответ",
                GroupName = groupName,
                IsChecked = isEnable,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                
                
            };

            ColumnDefinitions.Add(new ColumnDefinition(){Width = new GridLength(30)});
            ColumnDefinitions.Add(new ColumnDefinition());

            SetColumn(radioButton, 0);
            SetColumn(answerTextBox, 1);

            Children.Add(answerTextBox);
            Children.Add(radioButton);

            HorizontalAlignment = HorizontalAlignment.Stretch;
            Margin = new Thickness(30,0,0,0);
            Background = new SolidColorBrush(Colors.Transparent);
        }

        public string GetText()
        {
            return (Children[0] as TextBox).Text;
        }

        public bool IsEnable()
        {
            return (Children[1] as RadioButton).IsChecked.Value;
        }
    }
}
