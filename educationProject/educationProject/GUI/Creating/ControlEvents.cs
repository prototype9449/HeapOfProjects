using System.Collections.Generic;
using System.Windows;

namespace educationProject
{
    public partial class MainWindow
    {
        private void BackToMenuLesson_OnClick(object sender, RoutedEventArgs e)
        {
            MenuLesson.Visibility = Visibility.Visible;
            CreatingLesson.Visibility = Visibility.Hidden;
            PathsPanel.Visibility = Visibility.Visible;
            TitleCreatingLesson.Visibility = Visibility.Hidden;
            RefreshDirectories();
        }

        private void BackToMenuLessons_OnClick(object sender, RoutedEventArgs e)
        {
            MenuLesson.Visibility = Visibility.Visible;
            TitleCreatingLesson.Visibility = Visibility.Hidden;
            Clean_OnClick(null,null);
        }

        private void Clean_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите очистить панель?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ElementPanel.Children.Clear(); 
                CustomScrollViewer.ScrollToTop();
            }
        }


        private void CreateLesson_OnClick(object sender, RoutedEventArgs e)
        {
            PathsPanel.Visibility = Visibility.Hidden;
            CreateSectionGrid.Visibility = Visibility.Hidden;
            TitleCreatingLesson.Visibility = Visibility.Visible;

            EditNavigationPanel.Visibility = Visibility.Visible;
            CalculatingResult.Visibility = Visibility.Hidden;

            NameLessonTextBox.Text = NameLessonTextBox.ToolTip.ToString();
            PassworTextBox.Text = PassworTextBox.ToolTip.ToString();
        }
        private void DragAndDropField_DropImage(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            //TODO take into account the type of dropped files
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var imageList = new List<string>();
            imageList.AddRange(files);

            var userView = ElementPanel.Tag != null && (bool)ElementPanel.Tag;

            foreach (var pathToImage in imageList)
            {
                ElementPanel.Children.Add(new ImageStackPanel(pathToImage, ElementPanel, userView));
            }
        }
    }
}
