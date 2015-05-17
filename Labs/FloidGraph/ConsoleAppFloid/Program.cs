using FloidGraph;

namespace ConsoleAppFloid
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new Floid().GetShortPathMatrix(@"E:\1.txt");
        }
    }
}
