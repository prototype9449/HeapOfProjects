using System;
using System.IO;
using System.Linq;
using educationProject.Simple_classes;
using allCursors = System.Windows.Input.Cursors;

namespace educationProject
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Object _locker = new Object();
        public DateTime TimeOfBeginReadLesson = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RefreshDirectories()
        {
            var path = ViewWindowCreateSection.Tag.ToString();
            var dirs = Directory.GetDirectories(path);

            SectionPanel.Children.Clear();
            FolderIsEmptyErrorPanel.Children.Clear();

            TextBoxPath.Text = path.Replace("DateBaseEducation", "БазаДанных:") + "/";

            foreach (var dir in dirs)
            {
                var imagePreview = new FolderPreview(dir.Split('\\').Last());
                imagePreview.MouseDown += SectionButtonClick;
                SectionPanel.Children.Add(imagePreview);
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (file.Split('.').Last() != "dat")
                    continue;

                var lessonPreview = new LessonPreview(file.Split('\\').Last());
                lessonPreview.IconPreview.MouseLeftButtonDown += LessonPreviewOnMouseDown;
                lessonPreview.IconPreview.Tag = lessonPreview;
                lessonPreview.EditIcon.MouseLeftButtonDown += EditPreviewOnMouseDown;
                lessonPreview.EditIcon.Tag = lessonPreview;
                SectionPanel.Children.Add(lessonPreview);
            }

            if (!dirs.Any() && !files.Any())
            {
                var errorPanel = new ErrorPanel("Раздел пуст!", "Нажмите на кнопку слева, чтобы создать урок!");
                FolderIsEmptyErrorPanel.Children.Add(errorPanel.getErrorPanel());
            }
        }

        private void GeneralWindow_setSizeForElementPanel(object sender, EventArgs e)
        {
            ElementPanel.MinHeight = GeneralWindow.ActualHeight - 38;
        }
    }
}