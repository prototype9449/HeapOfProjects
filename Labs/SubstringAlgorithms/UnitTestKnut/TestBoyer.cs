using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.AreEqual(stopSymbols['a'], 4);
            Assert.AreEqual(stopSymbols['b'], 1);
            Assert.AreEqual(stopSymbols['c'], 6);
            Assert.AreEqual(stopSymbols['d'], 5);
            Assert.AreEqual(stopSymbols['e'], -1);
            
        }

        [TestMethod]
        public void SuffixesForWordMustMatch()
        {
            var sample = "колокол";
            var suffixes = new Suffixes(sample);
            Assert.AreEqual(suffixes["л"], 4);
            Assert.AreEqual(suffixes["ол"], 4);
            Assert.AreEqual(suffixes["кол"], 4);
            Assert.AreEqual(suffixes["окол"], 4);
            Assert.AreEqual(suffixes["локол"], 4);
            Assert.AreEqual(suffixes["олокол"], 4);
            Assert.AreEqual(suffixes["колокол"], 4);

        }

        [TestMethod]
        public void SuffixesForWordMustMatch2()
        {
            var sample = "abcdadcd";
            var suffixes = new Suffixes(sample);
            Assert.AreEqual(suffixes["d"], 2);
            Assert.AreEqual(suffixes["cd"], 4);
            Assert.AreEqual(suffixes["dcd"], 8);
            Assert.AreEqual(suffixes["adcd"], 8);
            Assert.AreEqual(suffixes["dadcd"], 8);
            Assert.AreEqual(suffixes["cdadcd"], 8);
            Assert.AreEqual(suffixes["bcdadcd"], 8);
            Assert.AreEqual(suffixes["abcdadcd"], 8);

        }
    }
}
