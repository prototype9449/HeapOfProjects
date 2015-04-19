using System;

namespace LessonLibrary
{
    [Serializable]
    public class TextInfo : IData
    {
        public string Text { get; private set; }
        private readonly byte[] _bytes;
        public TextInfo(string text)
        {
            Text = text;
            _bytes = ConvertData.ConvertTextToByteArray(text);
        }

        public TextInfo(byte[] bytes)
        {
            _bytes = bytes;
            Text = ConvertData.ConvertByteArrayToText(bytes);
        }
    }
}
