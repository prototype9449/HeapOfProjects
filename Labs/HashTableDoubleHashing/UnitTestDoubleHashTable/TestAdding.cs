using System;
using HashTableDoubleHashing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDoubleHashTable
{

    public class MyInt
    {
        public int Number;
        public MyInt(int number)
        {
            Number = number;
        }

        public override int GetHashCode()
        {
            return Number;
        }

        public override bool Equals(object obj)
        {
            return (obj as MyInt).Number == Number;
        }
    }
    

    [TestClass]
    public class TestAdding
    {
        private DoubleHashTable<MyInt, int> GetTable()
        {
            return new DoubleHashTable<MyInt, int>(new SizeHashTable());
        }
        [TestMethod]
        public void TestMethod1()
        {
            var table = GetTable();
            var numbers = new int[]{31, 7, 20, 17, 12, 19, 25};
            foreach (var number in numbers)
            {
                table.Add(new MyInt(number), number);
            }
            Assert.AreEqual(table.Count, numbers.Length);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var table = GetTable();
            var numbers = new int[] { 31, 7, 20, 17, 12, 19, 24, 25 };
            foreach (var number in numbers)
            {
                table.Add(new MyInt(number), number);
            }
            Assert.AreEqual(table.Count, numbers.Length);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var table = GetTable();
            var numbers = new[] { 31, 7, 20, 17, 12, 19};
            foreach (var number in numbers)
            {
                table.Add(new MyInt(number), number);
            }
            table.Remove(new MyInt(31));
            Assert.AreEqual(table.Count, numbers.Length-1);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var table = GetTable();
            var numbers = new[] { 31, 7, 20, 17, 12, 19, 25 };
            foreach (var number in numbers)
            {
                table.Add(new MyInt(number), number);
            }
            Assert.AreEqual(table.SizeTable.Size, 7);
            table.Remove(new MyInt(31));
            table.Add(new MyInt(23), 23);
            Assert.AreEqual(table.SizeTable.Size, 7);
            table.Add(new MyInt(31),31);
            Assert.AreEqual(table.SizeTable.Size, 23);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var table = GetTable();
            var numbers = new[] { 31, 7, 20, 17, 12, 19, 25 };
            foreach (var number in numbers)
            {
                table.Add(new MyInt(number), number);
            }
            foreach (var number in numbers)
            {
                Assert.IsTrue(table.Contains(new MyInt(number)));
            }
        }
    }
}
