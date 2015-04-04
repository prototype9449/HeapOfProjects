using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LessonLibrary;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;

namespace educationProject
{
    public class TextStackPanel : BaseStackPanel, IGettingData
    {
        public TextStackPanel(StackPanel placeDeleting)
            : this("", "Введите текст", placeDeleting,true,false)
        {}

        public TextStackPanel(string text, string toolTip, StackPanel placeDeleting, bool haveDeleteButton, bool userView)
        {
            IsModifiable = !userView;

            AllowDrop = true;
            DragEnter += ElementPanel_OnDragEnter;
            MouseDown += This_MouseDown;

            PlaceDeleting = placeDeleting;

            if (haveDeleteButton && !userView)
            {
                Children.Add(FactoryGrid.GetDeleteButton(this, PlaceDeleting));
            }
            var textBoxDefault = FactoryTextBox.GetTextBoxDefault(text, !userView, toolTip);
            textBoxDefault.IsEnabled = !userView;
            Children.Add(textBoxDefault);
            var diagonalLinesImageBitmap = PictureResource.WhiteDiagonalLines;
            var color = new SolidColorBrush(Color.FromRgb(54, 171, 255));
            Background = GetComplexVisualBrush(color, diagonalLinesImageBitmap);
        }

        private VisualBrush GetComplexVisualBrush(SolidColorBrush color, Bitmap bitmap)
        {
            var myBrush = new VisualBrush();
            var brushGrid = new Grid();
            var colorGrid = new Grid
            {
                Background = color,
                MinWidth = 500,
                MinHeight = 300,
                Opacity = 0.3
            };

            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            var visualBrushFromImage = new Image() { Source = bitmapSource };
            brushGrid.Children.Add(visualBrushFromImage);
            brushGrid.Children.Add(colorGrid);
            myBrush.Visual = brushGrid;
            return myBrush;
        }

        public IData GetData()
        {
            var i = 0;
            while (!(Children[i] is TextBox))
            {
                i++;
            }
            var text = (Children[i] as TextBox).Text;
            return new TextInfo(text);
        }

        public string GetText()
        {
            const int i = 0;
            while (!(Children[i] is TextBox)) { }
            return (Children[i] as TextBox).Text;
        }
       
    }
}
