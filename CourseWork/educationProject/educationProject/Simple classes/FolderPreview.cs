using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace educationProject.Simple_classes
{
    class FolderPreview : StackPanel
    {
        public Image IconPreview { get; set; }

        public Label TextPreview { get; set; }

        public FolderPreview(string textPreview)
        {
            var bitmap = PictureResource.FolderIcon;

            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            TextPreview = new Label { Content = textPreview, HorizontalAlignment = HorizontalAlignment.Center, Cursor = Cursors.Hand, Foreground = Brushes.Azure, FontWeight = FontWeights.Bold};
            IconPreview = new Image { Source = bitmapSource, Width = 60, Height = 60, Cursor = Cursors.Hand};
            Children.Add(TextPreview);
            Children.Add(IconPreview);

            var border = new Border { Width = IconPreview.Width + 20, BorderBrush = Brushes.DarkGray, BorderThickness = new Thickness { Top = 2, Bottom = 2, Left = 2, Right = 2 } };
            Children.Add(border);
        }
    }
}
