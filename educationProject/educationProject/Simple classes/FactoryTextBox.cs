using System;
using System.Windows;
using System.Windows.Controls;

namespace educationProject
{
    //var lessonNameTextbox = new TextBoxWrapper("Содержание урока:",
    //            new Thickness(0, 5, 0, 0), 450, 455, 600, HorizontalAlignment.Center, true).GetTextBox();

    public static class FactoryTextBox
    {
        private static TextBox GetTextBox(string text, bool IsModific, string toolTip ="")
        {
            var textBox = new TextBox
            {
                Margin = new Thickness(10),
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                FontSize = 20,
                Text = text,
                ToolTip = toolTip,
                TextWrapping = new TextWrapping(),
                AcceptsReturn = true,
                IsEnabled = IsModific
            };

            textBox.GotFocus += ToolTip_RemoveText;
            textBox.LostFocus += ToolTip_AddText;
            return textBox;
        }
        
        public static TextBox GetTextBoxAnswer(int number, bool IsModific)
        {
            return GetTextBox("Ответ #" + (number + 1),IsModific, "Ответ #" + (number + 1));
        }
        public static TextBox GetTextBoxDefault(string text, bool IsModific, string toolTip="")
        {
            return GetTextBox(text, IsModific, toolTip);
        }

        private static void ToolTip_RemoveText(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text == textBox.ToolTip.ToString())
            {
                textBox.Text = "";
            }
        }

        private static void ToolTip_AddText(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text == "")
            {
                textBox.Text = textBox.ToolTip.ToString();
            }
        }
       
    }
    
}
