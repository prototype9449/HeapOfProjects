using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace educationProject
{
    public static class FactoryGrid
    {
        public static TextStackPanel GetTextBoxGrid(string text , string toolTip, StackPanel placeDeleting)
        {
            return new TextStackPanel(text, toolTip, placeDeleting, haveDeleteButton: true, userView: false) { HorizontalAlignment = HorizontalAlignment.Stretch };
        }

        public static TextStackPanel GetTextBoxGridWithoutDeleteButton(string text, string toolTip, StackPanel placeDeleting, bool userView)
        {
            return new TextStackPanel(text, toolTip, placeDeleting, haveDeleteButton: false, userView : userView) { HorizontalAlignment = HorizontalAlignment.Stretch };
        }

        public static ImageStackPanel GetImageGrid(string pathToImage, StackPanel placeDeleting, bool userView)
        {
            return new ImageStackPanel(pathToImage, placeDeleting, userView);
        }

        public static TestStackPanel GetTestStackPanel(StackPanel placeDeleting)
        {
            return new TestStackPanel(placeDeleting);
        }


        public static Image GetDragAndDropField(StackPanel stackPanel)
        {
            var dragAndDropField = new Image();
            var bitmap = PictureResource.DragAndDropLogo;
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            dragAndDropField.Source = bitmapSource;
            dragAndDropField.Width = 400;
            dragAndDropField.Height = 50;
            dragAndDropField.AllowDrop = true;
            return dragAndDropField;
        }

        public static Image GetDeleteButton(UIElement deleteElement, StackPanel placeDeleting)
        {
            var deleteButton = new Image();
            var bitmap = PictureResource.DeleteButtonMin2;
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            deleteButton.Source = bitmapSource;
            deleteButton.Width = 129;
            deleteButton.Height = 30;
            deleteButton.HorizontalAlignment = HorizontalAlignment.Right;
            deleteButton.Cursor = Cursors.Hand;

            deleteButton.MouseDown += (sender, args) =>
            {
                var result = MessageBox.Show("Вы действительно хотите удалить панель?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    placeDeleting.Children.Remove(deleteElement);
            };

            return deleteButton;
        }
    }
}
