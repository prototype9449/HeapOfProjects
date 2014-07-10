using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph   gr = new Graph(5);

            gr.AddVertex(2, new HashSet<int>());           
            gr.AddVertex(3, new HashSet<int>(new int[] { 2 }));
            gr.AddVertex(1, new HashSet<int>(new int[] { 2, 5 }));
            
            foreach (var vertex in gr.SetVertex)
            {
                Console.WriteLine();
                Console.Write(vertex.Key + " With ");
                foreach (var edge in vertex.Value)
                    Console.Write(edge + " ");
            }
            Console.ReadKey();
        }
    }
}
