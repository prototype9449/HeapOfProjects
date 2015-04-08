using System.Linq;
using BinaryHeapLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBinaryHeap
{
    [TestClass]
    public class TestAdding
    {
        private int[] GetArray()
        {

            var array = new[] { 1, 2, 3, 4, 5, 6, 7 };
            return array;

        }

        [TestMethod]
        public void TestMethod1()
        {
            var heap = new BinaryHeap<int>(GetArray()).GetSortedArray();
            var array = GetArray().Reverse().ToArray();
            for (int i = 0; i < heap.Length; i++)
            {
                Assert.AreEqual(heap[i], array[i]);
            }

        }
    }
}
