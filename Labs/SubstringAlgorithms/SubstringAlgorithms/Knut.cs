using System;
using System.Collections.Generic;


namespace SubstringAlgorithms
{
    public class Knut
    {
        public static int[] GetPrefix(string sample)
        {
            var result = new int[sample.Length];
            result[0] = -1;
            int stepIndex = -1;

            for (int i = 1; i < sample.Length; i++)
            {
                if (sample[stepIndex + 1] == sample[i])
                {
                    result[i] = stepIndex + 1;
                    stepIndex++;
                }
                else
                {
                    while (stepIndex != -1)
                    {
                        stepIndex = result[stepIndex];
                        if (sample[stepIndex+1] == sample[i])
                        {
                            stepIndex++;
                            break;
                        }
                    }
                    result[i] = stepIndex;
                }
            }
            return result;
        }

        public static List<int> GetIndexes(string text, string substring)
        {
            int[] prefix = GetPrefix(substring);
            var amount = 0;
            var indexes = new List<int>();

            for (int i = 0; i < text.Length; i++)
            {
                while (amount >= 0 && amount+1 < substring.Length && substring[amount+1]!= text[i])
                {
                    amount = prefix[amount];
                }
                if (amount + 1 < substring.Length && substring[amount + 1] == text[i])
                    amount++;

                if (amount == substring.Length-1)
                {
                    indexes.Add(i - substring.Length+1);
                    amount = prefix[amount];
                }
            }
            return indexes;
        }
    }
}
