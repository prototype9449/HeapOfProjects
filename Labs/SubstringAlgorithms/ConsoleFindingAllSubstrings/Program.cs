using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SubstringAlgorithms;

namespace ConsoleFindingAllSubstrings
{
    class Program
    {
        static void Main(string[] args)
        {
            var indexesKnut = new List<int>();
            var indexesBoyer = new List<int>();
            using (var reader = new StreamReader(@"E:\anna.txt", Encoding.Default))
            {
                var text = reader.ReadToEnd();
                indexesKnut = Knut.GetIndexes(text, "ак");
                indexesBoyer = BoyerMoor.GetIndexes(text, "ак");
            }
            Console.WriteLine(indexesBoyer.Count);
            Console.WriteLine(indexesKnut.Count);


        }
    }
}
