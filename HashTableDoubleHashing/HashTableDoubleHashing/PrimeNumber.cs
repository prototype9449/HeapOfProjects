using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableDoubleHashing
{
    public static class PrimeNumber
    {
        public static int GetApproximately(int number)
        {
            while (!IsPrimeNumber(number))
            {
                number++;
            }
            return number;
        }

        private static bool IsPrimeNumber(int number)
        {
            if (number % 2 == 0) return false;
            for (int i = 3; i < Math.Sqrt(number) + 1; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
