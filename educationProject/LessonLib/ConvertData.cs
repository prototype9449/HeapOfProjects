using System;

namespace LessonLibrary
{
    public static class ConvertData
    {
        public static byte[] ConvertTextToByteArray(string sourceString)
        {
            var bytes = new byte[sourceString.Length * sizeof(char)];
            Buffer.BlockCopy(sourceString.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ConvertByteArrayToText(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
