using System.IO;
using System.Windows;
using educationProject.Simple_classes;

namespace educationProject
{
    public partial class MainWindow
    {
        private void ViewWindowCreateSection_OnClick(object sender, RoutedEventArgs e)
        {
            PathsPanel.Visibility = Visibility.Hidden;
            TitleCreatingLesson.Visibility = Visibility.Hidden;
            CreateSectionGrid.Visibility = Visibility.Visible;
        }
        private void CreateSection_OnClick(object sender, RoutedEventArgs e)
        {
            //Защита от placeHolder
            if (NameSection.Text == NameSection.ToolTip.ToString())
            {
                MessageBox.Show("Введите название раздела!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var sectionName = NameSection.Text;

            var path = ViewWindowCreateSection.Tag + "/" + NameSection.Text;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            {
                MessageBox.Show("Такой каталог уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var folderPreview = new FolderPreview(sectionName);
            folderPreview.MouseDown += SectionButtonClick;
            SectionPanel.Children.Add(folderPreview);

            NameSection.Text = "";

            CreateSectionGrid.Visibility = Visibility.Hidden;
            PathsPanel.Visibility = Visibility.Visible;

            RefreshDirectories();
        }

        private void SectionButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            MenuLesson.Visibility = Visibility.Visible;

            var folderPreview = (FolderPreview)sender;
            ViewWindowCreateSection.Tag += "/" + folderPreview.TextPreview.Content;
            RefreshDirectories();
        }

        private void LogOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            LogInMenu.Visibility = Visibility.Visible;
            MenuLesson.Visibility = Visibility.Hidden;
        }
    }
}
