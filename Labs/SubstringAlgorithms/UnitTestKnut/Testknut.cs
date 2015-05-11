using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubstringAlgorithms;

namespace UnitTestKnut
{
    [TestClass]
    public class TestKnut
    {
        [TestMethod]
        public void PrefixMustMatch()
        {
            var realPrefix = Knut.GetPrefix("abcdabcabcdabcdab");
            var expectedPrefix = new int[] { -1, -1, -1, -1, 0, 1, 2, 0, 1, 2, 3, 4, 5, 6, 3, 4, 5 };
            Assert.IsTrue(realPrefix.SequenceEqual(expectedPrefix));
        }

        [TestMethod]
        public void PrefixMustMutch2()
        {
            var realPrefix = Knut.GetPrefix("abcdabscabcdabia");
            var expectedPrefix = new[] { -1, -1, -1, -1, 0, 1, -1, -1, 0, 1, 2, 3, 4, 5, -1, 0 };
            Assert.IsTrue(realPrefix.SequenceEqual(expectedPrefix));
        }


        [TestMethod]
        public void PrefixMustMutch3()
        {
            var realPrefix = Knut.GetPrefix("abababab");
            var expectedPrefix = new[] { -1, -1, 0, 1, 2, 3, 4, 5 };
            Assert.IsTrue(realPrefix.SequenceEqual(expectedPrefix));
        }

        [TestMethod]
        public void PrefixMustMutch4()
        {
            var realPrefix = Knut.GetPrefix("aaaab");
            var expectedPrefix = new[] { -1, 0, 1, 2, -1 };
            Assert.IsTrue(realPrefix.SequenceEqual(expectedPrefix));
        }

        [TestMethod]
        public void ShiftsMustMutch1()
        {
            var text = "abcabcadb";
            var substring = "abcad";
            var expectedShifts = new List<int> {3};
            var realShifts = Knut.GetIndexes(text, substring);
            Assert.IsTrue(realShifts.SequenceEqual(expectedShifts));
        }

        [TestMethod]
        public void ShiftsMustMutch2()
        {
            var text = "abcabcabcab";
            var substring = "abcab";
            var expectedShifts = new List<int> { 0,3,6 };
            var realShifts = Knut.GetIndexes(text, substring);
            Assert.IsTrue(realShifts.SequenceEqual(expectedShifts));
        }

        [TestMethod]
        public void ShiftsMustMutch3()
        {
            var text = "aabaabbaabababaab";
            var substring = "aba";
            var expectedShifts = new List<int> {1,8, 10, 12};
            var realShifts = Knut.GetIndexes(text, substring);
            Assert.IsTrue(realShifts.SequenceEqual(expectedShifts));
        }

        [TestMethod]
        public void ShiftsMustMutch4()
        {
            var text = "aaaaaaa";
            var substring = "a";
            var expectedShifts = new List<int> { 0,1,2,3,4,5,6 };
            var realShifts = Knut.GetIndexes(text, substring);
            Assert.IsTrue(realShifts.SequenceEqual(expectedShifts));
        }
        
    }
}
