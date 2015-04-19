using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LessonLibrary;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;

namespace educationProject
{
    public partial class MainWindow
    {

        private List<User> userResults;
        private int directionSort = 1;

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            ElementPanel.Children.Add(FactoryGrid.GetTestStackPanel(ElementPanel));
            ScrollToBottom();
        }

        private void AddTextButton_Click(object sender, RoutedEventArgs e)
        {
            ElementPanel.Children.Add(new TextStackPanel("Введите текст", "Введите текст", ElementPanel,true,false));
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            CustomScrollViewer.UpdateLayout();
            const double defaultOffset = 10.0;
            if (CustomScrollViewer.ScrollableHeight > defaultOffset)
                CustomScrollViewer.ScrollToBottom();
        }

        private void CalculatingResult_Click(object sender, RoutedEventArgs e)
        {
            CalculatingResult.Visibility = Visibility.Hidden;

            var rightAnswerList = _lesson.GetRightAnswers();

            int rightUserAnswer = 0;

            for (int i = 0; i < ElementPanel.Children.Count; i++)
            {
                var childsElemPanel = ElementPanel.Children[i];
                var testStackPanel = childsElemPanel as TestStackPanel;
                if (testStackPanel != null)
                {
                    var diagonalLinesImageBitmap = PictureResource.WhiteDiagonalLines;
                    var color = new SolidColorBrush(Color.FromRgb(255, 60, 60));
                    if (testStackPanel.GetEnableNumberAnswer() == rightAnswerList[i])
                    {
                        rightUserAnswer++;
                        color = new SolidColorBrush(Color.FromRgb(86, 255, 86));
                        diagonalLinesImageBitmap = PictureResource.BlackDiagonalLines;
                    }
                    else
                    {
                        var answerGrid = testStackPanel.GetAnswersGrid();
                        var answers = answerGrid.GetAnswers();

                        testStackPanel.Children.Add(new Label
                        {
                            Content =
                                "Правильный ответ №" + (rightAnswerList[i] + 1) + " [" + answers[rightAnswerList[i]] +
                                "]",
                            FontSize = 25,
                            FontWeight = FontWeights.Bold,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Foreground = Brushes.Azure,
                            Margin = new Thickness(0, 10, 0, 5)
                        });
                    }

                    var myBrush = GetComplexVisualBrush(color, diagonalLinesImageBitmap);
                    testStackPanel.Background = myBrush;
                }
                else
                {
                    ElementPanel.Children.RemoveAt(i);
                    i--;
                }
            }

            ElementPanel.Children.Insert(0, (new Label
            {
                Content = "Сделано правильно " + rightUserAnswer + " из " + rightAnswerList.Count + " заданий",
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = Brushes.Azure,
                Margin = new Thickness(0, 20, 0, 20)
            }));

            SaveUserResult(LoginTextBox.Text, _lesson.Title, rightUserAnswer, rightAnswerList.Count);
        }

        private VisualBrush GetComplexVisualBrush(SolidColorBrush color, Bitmap bitmap)
        {
            var myBrush = new VisualBrush();
            var brushGrid = new Grid();
            var colorGrid = new Grid
            {
                Background = color,
                MinWidth = 200,
                MinHeight = 200,
                Opacity = 0.8
            };
            brushGrid.Children.Add(colorGrid);

            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            var visualBrushFromImage = new Image() {Source = bitmapSource};
            brushGrid.Children.Add(visualBrushFromImage);
            myBrush.Visual = brushGrid;
            return myBrush;
        }

        private void SaveUserResult(string UserName, string NameLesson, int countRightUserAnswer, int countAllAnswer)
        {
            try
            {
                var path = Environment.CurrentDirectory + "\\" + "testResults.dat";
                var formatter = new BinaryFormatter();

                var performTime = DateTime.Now - TimeOfBeginReadLesson;
                var user = new User(UserName, NameLesson, countRightUserAnswer, countAllAnswer, DateTime.Now, new TimeSpan(performTime.Hours, performTime.Minutes, performTime.Seconds));

                using (var fStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fStream, user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private List<User> GetUserResults()
        {
            var path = Environment.CurrentDirectory + "\\" + "testResults.dat";
            var results = new List<User>();

            if (!File.Exists(path))
            {
                return results;
            }

            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var bFormatter = new BinaryFormatter();
                while (fileStream.Position != fileStream.Length)
                {
                    results.Insert(0, (User)bFormatter.Deserialize(fileStream));
                }
            }

            return results;
        }

        private void LoadUserResultsButton_Click(object sender, RoutedEventArgs e)
        {
            CreatingLesson.Visibility = Visibility.Visible;
            MenuLesson.Visibility = Visibility.Hidden;
            EditNavigationPanel.Visibility = Visibility.Hidden;
            CalculatingResult.Visibility = Visibility.Hidden;

            userResults = GetUserResults();

            if (!userResults.Any())
            {
                var errorPanel = new ErrorPanel("Раздел пуст!", "Результатов пройденных тестов не найдено!");
                ElementPanel.Children.Add(errorPanel.getErrorPanel());
                return;
            }

            DrawResultGrid();
        }

        private void DrawResultGrid()
        {
            ElementPanel.Children.Clear();
            string[][] header =
            {
                new [] {"Имя", "Урок", "Результат", "Дата", "Затрачено"},
                new [] {"Ваше имя", "Название урока", "Результат", "Дата прохождения", "Затрачено времени на прохождение теста"}
            };
            ElementPanel.Children.Add(GetResultHeaderGrid(header, Brushes.Bisque));

            var indexResult = 0;
            foreach (var result in userResults)
            {
                string[][] args =
                {
                    new []
                    {
                        result.Name, result.LessonName, (result.CountRightAnswer + "/" + result.CountAllAnswer),
                        result.EndTime.ToShortDateString(), result.PerformTime.ToString()
                    },
                    new[]
                    {
                        "Ваше имя", "Название урока", "Результат", "Время: " + result.EndTime.ToLongTimeString(),
                        "Затрачено времени на прохождение теста"
                    }
                };

                var backGround = indexResult % 2 == 0 ? Brushes.Beige : Brushes.Azure;
                indexResult++;

                ElementPanel.Children.Add(GetResultRowGrid(args, backGround));
            }
        }

        private Grid GetResultRowGrid(string[][] columnText, Brush backGround)
        {
            var rowGrid = new Grid()
            {
                Width = ElementPanel.Width - 20,
                Background = backGround,
                ShowGridLines = true
            };

            for (var i = 0; i < columnText[0].Count(); i++)
            {
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition());

                var text = new TextBlock
                {
                    Text = columnText[0][i],
                    ToolTip = columnText[1][i],
                    FontSize = 15,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold
                };
                Grid.SetColumn(text, i);
                rowGrid.Children.Add(text);
            }

            return rowGrid;
        }

        private Grid GetResultHeaderGrid(string[][] columnText, Brush backGround)
        {
            var headerGrid = GetResultRowGrid(columnText, backGround);
            headerGrid.Margin = new Thickness(0, 20, 0, 0);
            foreach (var col in headerGrid.Children)
            {
                var columnTextBlock  = (TextBlock) col;
                columnTextBlock.Cursor = Cursors.Hand;
                columnTextBlock.MouseLeftButtonDown += ResultHeader_Click;
            }
        
            return headerGrid;
        }

        private void ResultHeader_Click(object sender, MouseButtonEventArgs e)
        {
            var column = (TextBlock) sender;
            int colIndex = Grid.GetColumn(column);

            directionSort++;

            switch (colIndex)
            {
                case 0:
                    if (directionSort % 2 == 0)
                        userResults.Sort(CompareNameDown);
                    else
                        userResults.Sort(CompareNameUp);
                    break;
                case 1:
                    if (directionSort % 2 == 0)
                        userResults.Sort(CompareLessonNameDown);
                    else
                        userResults.Sort(CompareLessonNameUp);
                    break;
                case 3:
                    if (directionSort % 2 == 0)
                        userResults.Sort(CompareDateUp);
                    else
                        userResults.Sort(CompareDateDown);
                    break;
            }    
            DrawResultGrid();
        }

        private int CompareNameUp(User a, User b)
        {
            return String.Compare(a.Name, b.Name);
        }

        private int CompareNameDown(User a, User b)
        {
            return String.Compare(b.Name, a.Name);
        }

        private int CompareLessonNameUp(User a, User b)
        {
            return String.Compare(a.LessonName, b.LessonName);
        }

        private int CompareLessonNameDown(User a, User b)
        {
            return String.Compare(b.LessonName, a.LessonName);
        }

        private int CompareDateUp(User a, User b)
        {
            return a.EndTime.CompareTo(b.EndTime);
        }

        private int CompareDateDown(User a, User b)
        {
            return b.EndTime.CompareTo(a.EndTime);
        }

        private BitmapSource GetBitmapSource(Bitmap resource)
        {
            var fileIconBitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                resource.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            return fileIconBitmapSource;
        }
    }
}
