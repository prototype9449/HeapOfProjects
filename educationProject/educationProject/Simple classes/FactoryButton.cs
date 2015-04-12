using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LessonDB;
using LessonLibrary;

namespace educationProject.Simple_classes
{
    public static class FactoryButton
    {
        public static Button GetLessonButton(LessonInfoId lesson)
        {
            var button = new Button
            {
                Height = 70,
                Content = lesson.Title + " - " + lesson.Autor,
                Tag = lesson.Id,
                FontSize = 15,
                Background = new SolidColorBrush(Colors.Goldenrod),
                Padding = new Thickness(10),
                Margin = new Thickness(10)
            };
            return button;
        }
    }
}
