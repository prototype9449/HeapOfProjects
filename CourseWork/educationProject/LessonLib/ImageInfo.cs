using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace LessonLibrary
{
    [Serializable]
    public class ImageInfo : IData
    {
        [NonSerialized] private readonly Image ImageControl;
        
        public byte[] Bytes { get; private set; }

        public ImageInfo(string pathToImage)
        {
            ImageControl = new Image {Source = new BitmapImage(new Uri(pathToImage))};
            var imageDrawing = System.Drawing.Image.FromFile(pathToImage);

            var memoryStream = new MemoryStream();
            imageDrawing.Save(memoryStream, GetImageFormatByFileExtension(pathToImage));
            Bytes = memoryStream.ToArray();
        }

        public ImageInfo(byte[] bytes)
        {
            Bytes = bytes;
            var memoryStream = new MemoryStream(bytes);
            System.Drawing.Image imageSource = System.Drawing.Image.FromStream(memoryStream);

            var bitmap = new Bitmap(imageSource);
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);

                stream.Position = 0;
                var result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();

                //TODO take into account the original size of image
                ImageControl = new Image
                {
                    Source = result,
                    //MaxHeight = 350,
                    //MaxWidth = 600,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }
        }

        private ImageFormat GetImageFormatByFileExtension(string pathToFile)
        {
            var fileExtension = Path.GetExtension(pathToFile);
            if (fileExtension == null) throw new ArgumentException();

            switch (fileExtension.ToUpper())
            {
                case ".JPEG":
                case ".JPG" :
                    return ImageFormat.Jpeg;
                case ".BMP":
                    return ImageFormat.Bmp;
                case ".PNG":
                    return ImageFormat.Png;
                default:
                    throw new ArgumentException();
            }
        }

        public Image GetImageControl()
        {
            var imageInfo = new ImageInfo(Bytes);
            return imageInfo.ImageControl;
        }
    }
}
