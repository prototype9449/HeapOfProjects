using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableDoubleHashing
{
    class HashValuePrime : IGettingHashCode
    {
        private readonly int _size;
        private long _step;
        private int _number;

        public HashValuePrime(int size)
        {
            _size = size;
        }

        public int GetHashValue(int number)
        {
            if (_number != number)
            {
                _number = number;
                _step = 0;
            }
            else
            {
                _step++;
            }
            long firstFunction = number % _size;
            long secondFunction = 1 + number % (_size - 1);
            long multiplyResultByStep = _step*secondFunction%_size;
            long result = (firstFunction + multiplyResultByStep) % _size;
            return Convert.ToInt32(result);

        }
    }
}
