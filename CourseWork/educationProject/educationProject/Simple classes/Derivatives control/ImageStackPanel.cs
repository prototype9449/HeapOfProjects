using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LessonLibrary;

namespace educationProject
{
    public class ImageStackPanel : BaseStackPanel, IGettingData
    {
        private byte[] _bytes;
        public ImageStackPanel(string pathToImage, StackPanel placeDeleting, bool userView)
        {
            _bytes = new ImageInfo(pathToImage).Bytes;
            IsModifiable = !userView;

            var uri = new Uri(pathToImage);
            var bitmap = new BitmapImage(uri);
            var image = new Image { Source = bitmap };

            // TODO!!!
            // фиксит проблему с неуказанными значениями ширины и высоты
            System.Drawing.Image file = System.Drawing.Image.FromFile(pathToImage);
            image.Width = placeDeleting.ActualWidth;
            image.Height = file.Height*placeDeleting.ActualWidth/file.Width;
            //image.Width = 150;
            //image.Height = 150;

            if (IsModifiable)
            {
                Children.Add(FactoryGrid.GetDeleteButton(this, placeDeleting));
            }
            Children.Add(image);
            InitializeImage();

        }

        public ImageStackPanel(ImageInfo imageInfo, StackPanel placeDeleting, bool userView)
        {
            _bytes = imageInfo.Bytes;
            if (!userView)
            {
                Children.Add(FactoryGrid.GetDeleteButton(this, placeDeleting));
            }
            Children.Add(imageInfo.GetImageControl());
            InitializeImage();
        }

        private void InitializeImage()
        {
            Margin = new Thickness(10);
            DragEnter += ElementPanel_OnDragEnter;
            MouseDown += This_MouseDown;
            SetDiagonalLinesBackground();
        }

        private void SetDiagonalLinesBackground()
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(PictureResource.WhiteDiagonalLines.GetHbitmap(),
                 IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Background = new ImageBrush(bitmapSource);
        }

        public IData GetData()
        {
            return new ImageInfo(_bytes);
        }
    }
}
