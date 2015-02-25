using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Image = System.Windows.Controls.Image;

namespace educationProject
{
    public class LessonPreview : StackPanel
    {

        public Image IconPreview { get; set; }

        public Label TextPreview { get; set; }

        //public Image LoupeIcon { get; set; }

        public Image EditIcon { get; set; }


        public LessonPreview(string title)
        {
            var editIconBitmapSource = GetBitmapSource(PictureResource.EditIcon);
            EditIcon = new Image { Source = editIconBitmapSource, Width = 20, Height = 20, HorizontalAlignment = HorizontalAlignment.Left, Cursor = Cursors.Hand };
            Children.Add(EditIcon);

            var fileIconBitmapSource = GetBitmapSource(PictureResource.FileIcon);
            IconPreview = new Image { Source = fileIconBitmapSource, Width = 60, Height = 50, Cursor = Cursors.Hand };

            TextPreview = new Label { Content = title.Split('.').First(), HorizontalAlignment = HorizontalAlignment.Center, Cursor = Cursors.Hand, Foreground = Brushes.Azure, FontWeight = FontWeights.Bold};
            Children.Add(TextPreview);
            Children.Add(IconPreview);
            Background = GetComplexVisualBrush(PictureResource.BackgroundLessonPreview);
        }

        private VisualBrush GetComplexVisualBrush(Bitmap bitmap)
        {
            var myBrush = new VisualBrush();
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            var visualBrushFromImage = new Image() { Source = bitmapSource };
            myBrush.Visual = visualBrushFromImage;
            return myBrush;
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
