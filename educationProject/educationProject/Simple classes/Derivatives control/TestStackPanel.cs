using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LessonLibrary;

namespace educationProject
{
    public class TestStackPanel : BaseStackPanel, IGettingData
    {
        private int _numberRightAnswer;
        
        public TestStackPanel(StackPanel placeDeleting)
        {
            PlaceDeleting = placeDeleting;

            Children.Add(FactoryGrid.GetDeleteButton(this, PlaceDeleting));
            Children.Add(FactoryGrid.GetTextBoxGridWithoutDeleteButton("Введите вопрос", "Введите вопрос", PlaceDeleting,userView:false));
            Children.Add(new AnswersGrid());

            InitializeTest();
        }

        public TestStackPanel(string question, List<string> answers, int numberRightAnswer, StackPanel placeDeleting, bool userView)
        {
            IsModifiable = !userView;

            PlaceDeleting = placeDeleting;
            if (!userView)
            {
                Children.Add(FactoryGrid.GetDeleteButton(this, PlaceDeleting));
            }
            Children.Add(FactoryGrid.GetTextBoxGridWithoutDeleteButton(question, "", placeDeleting, userView));
            Children.Add(new AnswersGrid(answers, numberRightAnswer, userView));
            _numberRightAnswer = numberRightAnswer;

            InitializeTest();
        }

        internal void InitializeTest()
        {
            Margin = new Thickness(10);
            DragEnter += ElementPanel_OnDragEnter;
            MouseDown += This_MouseDown;
            SetDiagonalLinesBackground();
        }

        private void SetDiagonalLinesBackground()
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(PictureResource.WhiteDiagonalLinesWithBorder.GetHbitmap(),
                 IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Background = new ImageBrush(bitmapSource);
        }

        //public void AddTextBox(string text, bool userView, bool isHaveDeleteButton)
        //{
        //    var grid = new UIElement();

        //    //TODO!!!
        //    //_isModifiable = userView;

        //    if (isHaveDeleteButton)
        //    {
        //        grid = FactoryGrid.GetTextBoxGrid(text, "Введите текст", PlaceDeleting);
        //    }
        //    else
        //    {
        //        grid = FactoryGrid.GetTextBoxGridWithoutDeleteButton(text, "Введите текст", PlaceDeleting, userView);
        //    }

        //    if (Children[0] is Image)
        //    {
        //        Children.Insert(1, grid);
        //    }
        //    else
        //    {
        //        Children.Insert(0, grid);
        //    }
        //}

        //public void AddImage(string pathToFile)
        //{
        //    Children.Insert(1, FactoryGrid.GetImageGrid(pathToFile, PlaceDeleting,false));
        //}

        //public void AddImage(Image image)
        //{
        //    var imageGrid = new ImageStackPanel(image, PlaceDeleting, false);
        //    Children.Insert(1, imageGrid);
        //}

        public AnswersGrid GetAnswersGrid()
        {
            foreach (var child in Children)
            {
                if (child is AnswersGrid) 
                    return (child as AnswersGrid);
            }
            return null;
        }

        public IData GetData()
        {
            var answers = new List<string>();
            var numberRightAnswer = -1;
            var question = "";

            foreach (var grid in Children)
            {
                if (grid is TextStackPanel)
                {
                    question = (grid as TextStackPanel).GetText();
                }
                if (grid is AnswersGrid)
                {
                    answers = (grid as AnswersGrid).GetAnswers();
                    numberRightAnswer = (grid as AnswersGrid).GetEnableNumberAnswer();
                }
            }
            return new TestInfo(question, answers, numberRightAnswer);
        }

        public int GetEnableNumberAnswer()
        {
            foreach (var child in Children)
            {
                if (child is AnswersGrid)
                {
                    return (child as AnswersGrid).GetEnableNumberAnswer();
                }
            }
            return -1;
        }

        public void SetEnableNumberAnswer(int rightAnswer)
        {
            _numberRightAnswer = rightAnswer;
        }
    }
}
