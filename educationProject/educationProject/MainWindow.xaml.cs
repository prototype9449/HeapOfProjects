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
            var directories = Directory.GetDirectories(path);

            SectionPanel.Children.Clear();
            FolderIsEmptyErrorPanel.Children.Clear();

            TextBoxPath.Text = path.Replace("DateBaseEducation", "БазаДанных:") + "/";

            foreach (var directory in directories)
            {
                var imagePreview = new FolderPreview(directory.Split('\\').Last());
                imagePreview.MouseDown += SectionButtonClick;
                SectionPanel.Children.Add(imagePreview);
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var extension = file.Split('.').Last();
                if (extension != "dat")
                    continue;

                var titleFile = file.Split('\\').Last();
                var lessonPreview = new LessonPreview(titleFile);
                lessonPreview.IconPreview.MouseLeftButtonDown += LessonPreviewOnMouseDown;
                lessonPreview.IconPreview.Tag = lessonPreview;
                lessonPreview.EditIcon.MouseLeftButtonDown += EditPreviewOnMouseDown;
                lessonPreview.EditIcon.Tag = lessonPreview;

                SectionPanel.Children.Add(lessonPreview);
            }

            bool IsNothing = !directories.Any() && !files.Any();
            if (IsNothing)
            {
                var errorPanel = new ErrorPanel("Раздел пуст!", "Нажмите на кнопку слева, чтобы создать урок!");
                FolderIsEmptyErrorPanel.Children.Add(errorPanel.GetErrorPanel());
            }
        }

        private void GeneralWindow_setSizeForElementPanel(object sender, EventArgs e)
        {
            ElementPanel.MinHeight = GeneralWindow.ActualHeight - 38;
        }
    }
}