using System;
using System.Windows;
using LessonLibrary;

namespace educationProject
{
    public partial class MainWindow
    {
        private Lesson _lesson;

        private void GoToCreatingLessonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ElementPanel.Tag = false;
                InitializeLesson();
                ElementPanel.Children.Clear();
                MenuLesson.Visibility = Visibility.Hidden;    
                CreatingLesson.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                MessageBox.Show("Your password or login is incorrect");
            }
        }

        private void BackToStartMenu_OnClick(object sender, RoutedEventArgs e)
        {
            CreateSectionGrid.Visibility = Visibility.Hidden;
            TitleCreatingLesson.Visibility = Visibility.Hidden;

            MenuLesson.Visibility = Visibility.Visible;
            PathsPanel.Visibility = Visibility.Visible;
            ReturnPathCreateDirectories();  
            RefreshDirectories();
        }

        private void ReturnPathCreateDirectories()
        {
            //Убираем из пути последний каталог
            var pathArray = ViewWindowCreateSection.Tag.ToString().Split('/');
            Array.Resize(ref pathArray, pathArray.Length - 1);
            ViewWindowCreateSection.Tag = String.Join("/", pathArray);
            if (String.IsNullOrEmpty(ViewWindowCreateSection.Tag.ToString()))
                ViewWindowCreateSection.Tag = "DateBaseEducation";
        }

        private void InitializeLesson()
        {
            if (!string.IsNullOrEmpty(NameLessonTextBox.Text) && !string.IsNullOrEmpty(PassworTextBox.Text))
            {
                _lesson = new Lesson(NameLessonTextBox.Text, PassworTextBox.Text);
            }
            else
            {
                throw new NullReferenceException("Field password and title must not be empty");
            }
        }
    }
}
