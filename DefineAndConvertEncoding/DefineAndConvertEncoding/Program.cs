using System;
using System.IO;
using System.Text;
using Ude;

namespace DefineAndConvertEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertAllFiles(@"E:/Portal", Encoding.UTF8);
        }

        static void ConvertAllFiles(string pathDirectory, Encoding encoding)
        {
            var dir = new DirectoryInfo(pathDirectory);
            var fiels = dir.GetFiles("*.cs", SearchOption.AllDirectories);
            foreach (var fileInfo in fiels)
            {
                Convert(fileInfo.FullName, encoding);
            }
        }

        static void Convert(string path, Encoding encoding)
        {
            var detecetor = new CharsetDetector();
            byte[] bufferBefore;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                bufferBefore = new byte[(int)stream.Length];
                stream.Read(bufferBefore, 0, (int)stream.Length);
            }
            detecetor.Feed(bufferBefore, 0, bufferBefore.Length);
            detecetor.DataEnd();
            var stringEncode = detecetor.Charset;
            var factor = detecetor.Confidence;

            byte[] bufferAfter;
            if (Math.Abs(factor - 1) < 0.00001)
            {
                bufferAfter = Encoding.Convert(Encoding.GetEncoding(stringEncode), encoding, bufferBefore);
            }
            else
            {
                bufferAfter = Encoding.Convert(Encoding.Default, encoding, bufferBefore);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                stream.Write(bufferAfter, 0, bufferAfter.Length);
            }
        }
    }
}
