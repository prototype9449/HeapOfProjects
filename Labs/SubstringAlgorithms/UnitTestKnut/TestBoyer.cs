using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubstringAlgorithms;
using SubstringAlgorithms.Boyer_moor;

namespace UnitTestKnut
{
    [TestClass]
    public class TestBoyer
    {
        [TestMethod]
        public void StopSymbolsForWordMustMatch()
        {
            var sample = "abcdadcd";
            var stopSymbols = new StopSymbols(sample);
            Assert.AreEqual(stopSymbols['a'], 3);
            Assert.AreEqual(stopSymbols['b'], 6);
            Assert.AreEqual(stopSymbols['c'], 1);
            Assert.AreEqual(stopSymbols['d'], 2);
            Assert.AreEqual(stopSymbols['e'], 8);
            
        }

        [TestMethod]
        public void StopSuffixForWordMustMatch1()
        {
            var sample = "abcdadcd";
            var stopSuffixes = new StopSuffixes(sample);
            Assert.AreEqual(stopSuffixes["d"], 2);
            Assert.AreEqual(stopSuffixes["cd"], 4);
            Assert.AreEqual(stopSuffixes["dcd"], 8);
            Assert.AreEqual(stopSuffixes["adcd"], 8);
            Assert.AreEqual(stopSuffixes["dadcd"], 8);
            Assert.AreEqual(stopSuffixes["cdadcd"], 8);
            Assert.AreEqual(stopSuffixes["bcdadcd"], 8);
            Assert.AreEqual(stopSuffixes["abcdadcd"], 8);
        }

        [TestMethod]
        public void StopSuffixForWordMustMatch2()
        {
            var sample = "aaaaaaa";
            var stopSuffixes = new StopSuffixes(sample);
            Assert.AreEqual(stopSuffixes["a"], 1);
            Assert.AreEqual(stopSuffixes["aa"], 2);
            Assert.AreEqual(stopSuffixes["aaa"], 3);
            Assert.AreEqual(stopSuffixes["aaaa"], 7);
            Assert.AreEqual(stopSuffixes["aaaaa"], 7);
            Assert.AreEqual(stopSuffixes["aaaaaa"], 7);
            Assert.AreEqual(stopSuffixes["aaaaaaa"], 7);
            Assert.AreEqual(stopSuffixes["dasda"], 7);
        }

        [TestMethod]
        public void IndexesForWordMustMatch1()
        {
            var text = "abeccacbadbabbad";
            var subsrting = "abbad";
            var exceptedIndexes = new[] {11};
            var realIndexes = BoyerMoor.GetIndexes(text, subsrting);
            Assert.IsTrue(exceptedIndexes.SequenceEqual(realIndexes));
        }
        [TestMethod]
        public void IndexesForWordMustMatch2()
        {
            var text = "aabbadcbadbabbad";
            var subsrting = "abbad";
            var exceptedIndexes = new[] { 1,11 };
            var realIndexes = BoyerMoor.GetIndexes(text, subsrting);
            Assert.IsTrue(exceptedIndexes.SequenceEqual(realIndexes));
        }

        [TestMethod]
        public void IndexesForWordMustMatch3()
        {
            var text = "aaaaaaa";
            var subsrting = "a";
            var exceptedIndexes = new[] { 0,1,2,3,4,5,6 };
            var realIndexes = BoyerMoor.GetIndexes(text, subsrting);
            Assert.IsTrue(exceptedIndexes.SequenceEqual(realIndexes));
        }
    }
}
