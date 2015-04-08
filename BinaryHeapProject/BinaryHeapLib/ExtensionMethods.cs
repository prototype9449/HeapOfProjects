using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BinaryHeapLib
{
    public static class ExtensionMethods
    {
        public static void Swap<T>(this T[] elements, int i, int j)
        {
            var temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }
        public static void Swap<T>(this List<T> elements, int i, int j)
        {
            var temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }
    }
}
