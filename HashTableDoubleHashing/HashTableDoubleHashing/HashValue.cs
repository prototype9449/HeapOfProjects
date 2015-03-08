namespace HashTableDoubleHashing
{
    class HashValueDegreeTwo : IGettingHashCode
    {
        private readonly int _size;
        private int _step;
        private int _number;

        public HashValueDegreeTwo(int size)
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
            return (number % _size + _step * (1 + number % (_size - 2)))%_size;
        }
    }
}