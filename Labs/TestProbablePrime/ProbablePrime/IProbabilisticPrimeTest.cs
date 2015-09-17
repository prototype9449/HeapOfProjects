using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbablePrime
{
    public interface IProbabilisticPrimeTest
    {
        bool isPrimeNumber(string number, int rounds);
    }
}
