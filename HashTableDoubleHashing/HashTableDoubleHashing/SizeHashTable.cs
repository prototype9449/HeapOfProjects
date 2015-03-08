using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HashTableDoubleHashing
{
    public class SizeHashTable : ISizeTable
    {
        public int Size { get; private set; }
        private const int DefaultSize = 7;
        private const int FactorMultiply = 3;
        private IGettingHashCode _hashValue;
        
        public SizeHashTable()
        {
            Size = DefaultSize;
            _hashValue = new HashValuePrime(Size);
        }

        public SizeHashTable(int size)
        {
            Size = size;
            _hashValue = new HashValuePrime(Size);
        }

        public void IncriseSize()
        {
            var nextApproximatelyNumber = 0;
            checked
            {
                nextApproximatelyNumber = Size*FactorMultiply;
            }

            Size = PrimeNumber.GetApproximately(nextApproximatelyNumber);
            _hashValue = new HashValuePrime(Size);
        }
        
        public int GetHashValue(int number)
        {
            return _hashValue.GetHashValue(number);
        }
    }

    

    
}
