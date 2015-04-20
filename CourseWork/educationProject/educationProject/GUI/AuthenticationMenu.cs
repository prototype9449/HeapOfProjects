using System.IO;
using System.Windows;
using System.Windows.Input;

namespace educationProject
{
    public partial class MainWindow
    {
        private void GoToMenuSections_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsCorrectLoginField())
            {
                LogInMenu.Visibility = Visibility.Hidden;
                MenuLesson.Visibility = Visibility.Visible;

                CreateDateBasePath();
                RefreshDirectories();
            }
            else
            {
                MessageBox.Show("Имя должно быть не короче 5 символов!", "Ошибка");
            }
        }

        private void CreateDateBasePath()
        {
            var path = ViewWindowCreateSection.Tag.ToString();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private bool IsCorrectLoginField()
        {
            return LoginTextBox.Text.Length >= 2;
        }


        
    }
}
