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
            int k = -1;

            for (int i = 1; i < sample.Length; i++)
            {
                if (sample[k + 1] == sample[i])
                {
                    result[i] = k + 1;
                    k++;
                }
                else
                {
                    while (k != -1)
                    {
                        k = result[k];
                        if (sample[k+1] == sample[i])
                        {
                            k++;
                            break;
                        }
                    }
                    result[i] = k;
                }
            }
            return result;
        }

        public static List<int> GetIndexes(string text, string substring)
        {
            var prefix = GetPrefix(substring);
            var amount = 0;
            var shifts = new List<int>();

            for (int i = 0; i < text.Length; i++)
            {
                while (amount >= 0 && substring[amount+1]!= text[i])
                {
                    amount = prefix[amount];
                }
                if (substring[amount + 1] == text[i])
                    amount++;

                if (amount == substring.Length-1)
                {
                    shifts.Add(i - substring.Length+1);
                    amount = prefix[amount];
                }
            }
            return shifts;
        }
    }
}
