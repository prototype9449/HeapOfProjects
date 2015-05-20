using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubstringAlgorithms.Boyer_moor;

namespace SubstringAlgorithms
{
    public static class BoyerMoor
    {
        public static List<int> GetIndexes(string text, string substring)
        {
            if (text.Length < substring.Length)
                throw new InvalidOperationException("It is impossible. The length of text is less than the length of substring");

            var indexes = new List<int>();
            var stopSymbolsProvider = new StopSymbols(substring);
            var stopSuffixesProvider = new StopSuffixes(substring);

            int i = substring.Length - 1;
            int countMatchedSymbols = 0;
            while (i < text.Length)
            {
                int secondCh = i;
                for (int j = substring.Length - 1; j >= 0; j--, secondCh--)
                {
                    if (substring[j] == text[secondCh])
                    {
                        countMatchedSymbols++;
                    }
                    else
                    {
                        var suffix = text.Substring(secondCh, substring.Length - j);
                        var suffixShift = stopSuffixesProvider[suffix];
                        var symbolShift = stopSymbolsProvider[text[secondCh]];
                        if (symbolShift == substring.Length)
                        {
                            symbolShift = substring.Length-countMatchedSymbols;
                        }
                        var maxShift = Math.Max(suffixShift, symbolShift);

                        i += symbolShift;
                        countMatchedSymbols = 0;
                        break;
                    }
                }
                if (countMatchedSymbols == substring.Length)
                {
                    indexes.Add(secondCh+1);
                    i += substring.Length;
                    countMatchedSymbols = 0;
                }
            }
            return indexes;
        }
    }
}
