using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using educationProject.Simple_classes;
using LessonDB;
using LessonDB.LessonDals;
using LessonLibrary;
using Button = System.Windows.Controls.Button;
using Control = System.Windows.Controls.Control;
using MessageBox = System.Windows.MessageBox;
using RadioButton = System.Windows.Controls.RadioButton;

namespace educationProject
{
    public partial class MainWindow
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ToString();

        private void SaveLessonButton_Click(object sender, RoutedEventArgs e)
        {
            //if (_lesson.Title.Contains(".dat"))
            //_lesson.Title = _lesson.Title.Substring(0, _lesson.Title.Length - 4);

            TitleLessonTextBox.Text = _lesson.Title;
            AutorTextBox.Text = _lesson.Autor;

            ElementPanel.Opacity = 0.8;
            ElementPanel.Effect = new BlurEffect { Radius = 5 };
            SaveLessonPanel.Visibility = Visibility.Visible;
        }

        private void NameLessonForSaveButtonEscape_Click(object sender, RoutedEventArgs e)
        {
            ElementPanel.Effect = null;
            ElementPanel.Opacity = 1;
            SaveLessonPanel.Visibility = Visibility.Hidden;
        }

        private void NameLessonForSaveButtonOK_Click(object sender, RoutedEventArgs e)
        {
            SettingLesson();
            _lesson.Title = TitleLessonTextBox.Text;

            var path = ViewWindowCreateSection.Tag + "/" + TitleLessonTextBox.Text + ".dat";
            var formatter = new BinaryFormatter();

            using (var fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, _lesson);
            }

            ElementPanel.Effect = null;
            ElementPanel.Opacity = 1;
            SaveLessonPanel.Visibility = Visibility.Hidden;
        }

        private void SettingLesson()
        {
            _lesson.ClearData();

            foreach (var grid in ElementPanel.Children)
            {
                RefreshUpdatedRightAnswers(grid);

                var gridControl = grid as IGettingData;
                if (gridControl != null)
                {
                    _lesson.AddDataItem(gridControl.GetData());
                }
            }
        }


        private void RefreshUpdatedRightAnswers(object grid)
        {
            var testPanel = grid as TestStackPanel;
            if (testPanel == null) return;

            var testPanelChildren = testPanel.Children;
            var indexCurrentItem = 0;
            foreach (var testPanelChild in testPanelChildren)
            {
                if (!(testPanelChild is RadioButton)) continue;

                var isChecked = (bool)(testPanelChild as RadioButton).IsChecked;
                if (isChecked)
                {
                    testPanel.SetEnableNumberAnswer(indexCurrentItem);
                    break;
                }
                indexCurrentItem++;
            }
        }

        private void OpenFileInFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = Environment.CurrentDirectory + "/" + ViewWindowCreateSection.Tag + "/" + _lesson.Title;
            filePath = filePath.Replace("/", "\\");
            OpenFolderAndSelectFile(filePath);
        }

        private void OpenFolderAndSelectFile(string file)
        {
            try
            {
                Process.Start("explorer.exe", @"/select, " + file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLessonButton_Click(object sender, RoutedEventArgs e)
        {
            string pathOpenFile;
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pathOpenFile = ofd.FileName;
            }
            else
            {
                return;
            }
            AddLessonToWrapPanel(pathOpenFile);
            LoadLessons(pathOpenFile.Substring(0, pathOpenFile.Length - 4), true);
        }

        private void LoadLessonInputPassword(string pathOpenFile)
        {
            var formatter = new BinaryFormatter();

            using (var fileStream = File.OpenRead(pathOpenFile + ".dat"))
            {
                _lesson = (Lesson)formatter.Deserialize(fileStream);
            }

            string[] data = { _lesson.Password, pathOpenFile };

            PasswordForEditLessonTextBox.Tag = data;
            PasswordForEditLessonTextBox.Text = "";
            PasswordForEditLessonGrid.Visibility = Visibility.Visible;

            SectionPanel.Opacity = 0.1;
        }

        private void PasswordForEditLessonButtonOK_Click(object sender, RoutedEventArgs e)
        {
            var data = (String[])PasswordForEditLessonTextBox.Tag;
            if (PasswordForEditLessonTextBox.Text == data[0])
            {
                PasswordForEditLessonGrid.Visibility = Visibility.Hidden;
                SectionPanel.Opacity = 1;
                LoadLessons(pathOpenFile: data[1], userView: false);
            }
            else
            {
                MessageBox.Show("Вы ввели неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PasswordForEditLessonButtonEscape_Click(object sender, RoutedEventArgs e)
        {
            PasswordForEditLessonGrid.Visibility = Visibility.Hidden;
            SectionPanel.Opacity = 1;
        }

        private void LoadLessons(string pathOpenFile, bool userView)
        {
            ElementPanel.Tag = userView;
            ElementPanel.Children.Clear();
            TimeOfBeginReadLesson = DateTime.Now;
            CreatingLesson.Visibility = Visibility.Visible;
            MenuLesson.Visibility = Visibility.Hidden;

            if (userView)
            {
                EditNavigationPanel.Visibility = Visibility.Hidden;
                CalculatingResult.Visibility = Visibility.Visible;
            }
            else
            {
                EditNavigationPanel.Visibility = Visibility.Visible;
                CalculatingResult.Visibility = Visibility.Hidden;
            }

            var formatter = new BinaryFormatter();
            using (var fileStream = File.OpenRead(pathOpenFile + ".dat"))
            {
                _lesson = (Lesson)formatter.Deserialize(fileStream);
                foreach (var data in _lesson.DataList)
                {
                    AddTextPanelOrImagePanel(data, ElementPanel);

                    var testInfo = data as TestInfo;

                    if (testInfo != null)
                    {
                        var testStackPanel = new TestStackPanel(testInfo.Question, testInfo.Answers,
                            testInfo.NumberRightAnswer, ElementPanel, userView: userView);

                        ElementPanel.Children.Add(testStackPanel);
                    }
                }

            }

            if (userView)
                TakeOffSelectionFromRadioButtons(ElementPanel);
        }

        //Fix the problem, when selection of radiobuttons is not disabled, after loading from file
        private void TakeOffSelectionFromRadioButtons(UIElement container)
        {
            var children = LogicalTreeHelper.GetChildren(container);
            foreach (var element in children)
            {
                var castedElementToUiElement = (element as UIElement);
                if (castedElementToUiElement != null)
                {
                    var childsCurrentElement = LogicalTreeHelper.GetChildren(castedElementToUiElement);
                    if (childsCurrentElement != null)
                        TakeOffSelectionFromRadioButtons((element as UIElement));
                }

                if (element is RadioButton)
                    (element as RadioButton).IsChecked = false;
            }
        }

        private void AddLessonToWrapPanel(string pathToFile)
        {
            string destFileName = ViewWindowCreateSection.Tag + "/" + pathToFile.Split('\\').Last();
            while (File.Exists(destFileName))
                destFileName = destFileName.Insert(destFileName.Length - 4, " - new");
            File.Copy(pathToFile, destFileName);
            RefreshDirectories();
        }

        internal void EditPreviewOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var lessonPreview = (sender as Image).Tag;
            var pathToFile = ViewWindowCreateSection.Tag + "/" + (lessonPreview as LessonPreview).TextPreview.Content;
            LoadLessonInputPassword(pathToFile);
        }

        private void LessonPreviewOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var lessonPreview = (sender as Image).Tag;
            string pathToFile = ViewWindowCreateSection.Tag + "/" + (lessonPreview as LessonPreview).TextPreview.Content;
            LoadLessons(pathToFile, true);
            GeneralWindow_setSizeForElementPanel(sender: null, e: null);
        }


        private void AddTextPanelOrImagePanel(IData data, StackPanel stackPanel)
        {
            var userView = (bool)ElementPanel.Tag;
            var textInfo = data as TextInfo;
            if (textInfo != null)
            {
                var textGrid = new TextStackPanel(textInfo.Text, "введите текст", ElementPanel,
                    haveDeleteButton: !userView, userView: userView);
                stackPanel.Children.Add(textGrid);
            }

            var imageInfo = data as ImageInfo;
            if (imageInfo != null)
            {
                var imageGrid = new ImageStackPanel(imageInfo, ElementPanel, userView);
                stackPanel.Children.Add(imageGrid);
            }
        }

        private void ButtonSearchLessons_OnClick(object sender, RoutedEventArgs e)
        {
            ButClearAllSerachingLessons_OnClick(null, null);

            var dal = new SimplyLessonsDal(_connectionString);

            var parameters = new List<KeyValuePair<TypeSearch, string>>();

            if (!string.IsNullOrEmpty(TextBoxTitle.Text))
            {
                parameters.Add(new KeyValuePair<TypeSearch, string>(TypeSearch.Title, TextBoxTitle.Text));
            }
            if (!string.IsNullOrEmpty(TextBoxAutor.Text))
            {
                parameters.Add(new KeyValuePair<TypeSearch, string>(TypeSearch.Autor, TextBoxAutor.Text));
            }

            if (parameters.Count == 0) return;
            List<LessonInfoId> lessons;

            try
            {
                if (ChooseExactlyCheckBox.IsChecked.Value)
                {
                    lessons = dal.GetLessonsByFieldsExactly(parameters);
                }
                else
                {
                    lessons = dal.GetLessonsByFieldsLikely(parameters);
                }

                foreach (var lesson in lessons)
                {
                    var button = FactoryButton.GetLessonButton(lesson);
                    button.Click += ButtonOnClick;
                    StackPanelSearchingLessons.Children.Add(button);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели что-то неправильно");

            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var button = sender as Button;
            var idlesson = (int)button.Tag;

            var dal = new SimplyLessonsDal(_connectionString);
            var lesson = dal.GetLessonById(idlesson);

            var path = ViewWindowCreateSection.Tag + "/" + lesson.Title + ".dat";
            var formatter = new BinaryFormatter();

            using (var fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, lesson);
            }
            RefreshDirectories();
        }

        private void ButClearAllSerachingLessons_OnClick(object sender, RoutedEventArgs e)
        {
            StackPanelSearchingLessons.Children.Clear();
        }

        private void SaveButtonDataBase_OnClick(object sender, RoutedEventArgs e)
        {
            SettingLesson();
            _lesson.Title = TitleLessonTextBox.Text;
            _lesson.Autor = AutorTextBox.Text;
            _lesson.DateCreate = DateTime.Now;

            var dal = new SimplyLessonsDal(_connectionString);

            dal.InsertLesson(_lesson);

            ElementPanel.Effect = null;
            ElementPanel.Opacity = 1;
            SaveLessonPanel.Visibility = Visibility.Hidden;
        }
    }
}

