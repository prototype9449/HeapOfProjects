using System;
using Emil.GMP;

namespace ProbablePrime
{
    public class TestPrimeNumber : IProbabilisticPrimeTest
    {
        Random _random = new Random();
        public bool isPrimeNumber(string number, int roundCount)
        {
            var bigNumber = new BigInt(number);
            if (bigNumber <= 1) return false;
            if (bigNumber == 2) return true;
            if (bigNumber % 2 == 0) return false;

            var degree = 0;
            var oddRest = bigNumber - 1;

            while (!oddRest.IsOdd)
            {
                oddRest /= 2;
                degree++;
            }

            for (var i = 0; i < roundCount; i++)
            {
                var randomNumber = GetRandomBigNumber(bigNumber - 1) + 1;
                var rest = randomNumber.PowerMod(oddRest, bigNumber);
                if (rest == 1 || rest == bigNumber - 1) continue;

                for (int j = 0; j < degree - 1; j++)
                {
                    rest = rest.PowerMod(2, bigNumber);

                    if (rest == 1) return false;
                    if (rest == bigNumber - 1) break;
                }

                if (rest != bigNumber - 1) return false;
            }
            return true;
        }

        public BigInt GetRandomBigNumber(BigInt number)
        {
            var stringNumber = number.ToString();
            var randomNumber = "";
            var randomLength = _random.Next(1, stringNumber.Length+1);
            if (randomLength == 1)
            {
                return new BigInt(_random.Next(0, int.Parse(stringNumber[0].ToString())));
            }
            

            if (randomLength == stringNumber.Length)
            {
                randomNumber += _random.Next(1, int.Parse(stringNumber[0].ToString()) +1);
                var flag = false;
                var i = 1;
                for (;i < randomLength && !flag; i++)
                {
                    var positionDigit = int.Parse(stringNumber[i].ToString());
                    var randomDigit = _random.Next(0, positionDigit);
                    if (randomDigit < positionDigit)
                    {
                        flag = true;
                    }
                    randomNumber += randomDigit;
                }
                for (; i < randomLength; i++)
                {
                    randomNumber += _random.Next(10);
                }
            }
            else
            {
                randomNumber += _random.Next(1,10);
                for (var i = 1; i < randomLength; i++)
                {
                    randomNumber += _random.Next(10);
                }
            }
            
            return new BigInt(randomNumber);
        }

        
    }

    public static class ExtendedStringClass
    {
        public static bool IsGreat(this string first, string second)
        {
            return first.CompareTo(second) != -1;
        }
    }
}
