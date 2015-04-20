using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LessonDB;
using LessonLibrary;
using FontStyle = System.Windows.FontStyle;

namespace educationProject.Simple_classes
{
    public static class FactoryButton
    {
        public static Button GetLessonButton(LessonInfoId lesson)
        {
            var stackPanel = new StackPanel();
            var TitleTextBlock = new TextBlock
            {
                Text = lesson.Title,
                FontSize = 20,
                TextAlignment = TextAlignment.Center
            };
            
            var AutorTextBlock = new TextBlock
            {
                Text = lesson.Autor,
                FontSize = 20,
                TextAlignment = TextAlignment.Center
            };
            var DateTextBlock = new TextBlock
            {
                Text = lesson.DateCreate.ToShortDateString(),
                FontSize = 15,
                TextAlignment = TextAlignment.Center
            };
            stackPanel.Children.Add(TitleTextBlock);
            stackPanel.Children.Add(AutorTextBlock);
            stackPanel.Children.Add(DateTextBlock);
            var button = new Button
            {
                Height = 100,
                Content = stackPanel,
                Tag = lesson.Id,
                Background = new ImageBrush(new BitmapImage(new Uri(@"PanelForLabel.png", UriKind.Relative))),
                Padding = new Thickness(5),
                Margin = new Thickness(5)
            };
            return button;
        }
    }
}
