using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryHeapLib
{
    public class BinaryHeap<T> :IEnumerable<T> where T : IComparable<T>
    {
        public int Count
        {
            get { return _elements.Count; }
        }

        private List<T> _elements;

        public BinaryHeap()
        {
            _elements = new List<T>();
        }

        public BinaryHeap(IEnumerable<T> collection)
            : this()
        {
            _elements = new List<T>(collection);
            for (int i = Count / 2; i >= 0; i--)
            {
                Rebuilt(i);
            }
        }
        private void Rebuilt(int index)
        {
            do
            {
                int indexLeft = 2*index + 1;
                int indexRight = 2*index + 2;
                int indexMost = index;

                if (indexLeft < Count && _elements[indexLeft].CompareTo(_elements[indexMost])==1)
                {
                    indexMost = indexLeft;
                }

                if (indexRight < Count && _elements[indexRight].CompareTo(_elements[indexMost]) == 1)
                {
                    indexMost = indexRight;
                }

                if (indexMost == index)
                {
                    break;
                }

                _elements.Swap(index, indexMost);
                index = indexMost;

            } while (true);
        }

        public void RemoveMaxElement()
        {
            _elements[0] = _elements[Count - 1];
            _elements.RemoveAt(Count-1);
            Rebuilt(0);
        }

        public T GetMaxElement()
        {
            return _elements[0];
        }

        private void Add(T value)
        {
            _elements.Add(value);
            var index = Count - 1;
            if (index == 0) return;

            var indexRoot = (index - 1) / 2;

            while (_elements[index].CompareTo(_elements[indexRoot]) == 1  && index > 0)
            {
                _elements.Swap(index, indexRoot);

                index = indexRoot;
                indexRoot = (index - 1) / 2;
            }
        }

        public T[] GetSortedArray()
        {
            var copy = new List<T>(_elements);
            var result = new List<T>();
            while (Count!= 0)
            {
                var max = GetMaxElement();
                RemoveMaxElement();
                result.Add(max);
            }
            _elements = new List<T>(copy);
            return result.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
