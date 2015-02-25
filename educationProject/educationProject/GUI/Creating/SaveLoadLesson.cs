﻿using System;
using System.Collections;
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
using LessonLibrary;
using Control = System.Windows.Controls.Control;
using MessageBox = System.Windows.MessageBox;
using RadioButton = System.Windows.Controls.RadioButton;

namespace educationProject
{
    public partial class MainWindow
    {
        private void SaveLessonButton_Click(object sender, RoutedEventArgs e)
        {
            if (_lesson.Title.Contains(".dat"))
                _lesson.Title = _lesson.Title.Substring(0, _lesson.Title.Length - 4);

            NameLessonForSaveTextBox.Text = _lesson.Title;
            ElementPanel.Opacity = 0.8;
            ElementPanel.Effect = new BlurEffect {Radius = 5};
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
            try
            {
                _lesson.ClearData();

                foreach (var grid in ElementPanel.Children)
                {
                    var testPanel = grid as TestStackPanel;
                    if (testPanel != null)
                    {
                        RefreshUpdatedRightAnswers(testPanel);
                    }

                    var gridControl = grid as IGettingData;
                    if (gridControl != null)
                    {
                        _lesson.AddDataItem(gridControl.GetData());
                    }
                }

                var path = ViewWindowCreateSection.Tag + "/" + NameLessonForSaveTextBox.Text + ".dat";
                var formatter = new BinaryFormatter();

                using (var fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fStream, _lesson);
                }

                ElementPanel.Effect = null;
                ElementPanel.Opacity = 1;
                SaveLessonPanel.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshUpdatedRightAnswers(TestStackPanel testPanel)
        {
            var testPanelChildren = testPanel.Children;
            var indexCurrentItem = 0;
            foreach (var panel in testPanelChildren)
            {
                if (!(panel is RadioButton)) continue;

                var isChecked = (bool)(panel as RadioButton).IsChecked;
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
                _lesson = (Lesson) formatter.Deserialize(fileStream);
            }

            string[] data = { _lesson.Password, pathOpenFile};

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

            if(userView) 
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
    }
}

