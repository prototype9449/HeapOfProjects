using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace educationProject
{
    class ErrorPanel
    {
        public string Header { get; set; }
        public string MessageText { get; set; }

        public ErrorPanel(string _header, string messageText)
        {
            Header = _header;
            MessageText = messageText;
        }

        public WrapPanel GetErrorPanel()
        {
            var uri = new Uri("pack://application:,,,/Resource/RequestPanel.png");
            var image = new BitmapImage(uri);

            var wrapPanel = new WrapPanel
            {
                Name = "ErrorPanel",
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 270,
                Width = 400,
                Margin = new Thickness(0, 180, 0, 0),
                Background = new ImageBrush(image)
            };

            var textBlockHeader = new TextBlock
            {
                Name = "HeaderErrorPanel",
                Foreground = Brushes.Salmon,
                FontWeight = FontWeights.Bold,
                FontSize = 30,
                Margin = new Thickness(20),
                TextWrapping = TextWrapping.Wrap,
                Width = 360,
                TextAlignment = TextAlignment.Center,
                TextDecorations = TextDecorations.Underline,
                Text = Header
            };

            var textBlockText = new TextBlock
            {
                Name = "TextErrorPanel",
                Foreground = Brushes.DarkSalmon,
                FontSize = 23,
                Margin = new Thickness(20, 0, 20, 0),
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Text = MessageText
            };

            wrapPanel.Children.Add(textBlockHeader);
            wrapPanel.Children.Add(textBlockText);

            return wrapPanel;
        }
    }
}
