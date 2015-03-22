using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppListSkip
{
    public class ListSkip<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>, new()
    {
        private Node<TKey, TValue>[] _heads;
        private Random _random  = new Random();
        public int Count { get; private set; }
        public int Heigh { get; private set; }
        public double Probability { get; private set; }

        public ListSkip(int hight, double probability)
        {
            Heigh = hight;
            Probability = probability;
            ItitializeHeads();
        }

        public void Add(TKey key, TValue value)
        {
            Insert(new Node<TKey, TValue>(key,value));
        }

        public void Remove(TKey key)
        {
            var removingNode = new Node<TKey, TValue>(key, default(TValue));
            var previousNodes = new Stack<Node<TKey, TValue>>();
            var nextNodes = new Stack<Node<TKey, TValue>>();
            SetEdges(out previousNodes, out nextNodes, removingNode);

            if (!removingNode.Equals(nextNodes.Peek()))
            {
                for (int i = 0; i < Heigh; i++)
                {
                    previousNodes.Pop().Right = nextNodes.Pop();
                }
            }
            else
            {
                for (int i = 0; i < Heigh; i++)
                {
                    var next = nextNodes.Pop();
                    var previous = previousNodes.Pop();
                    if(previous.Right!= null)
                        previous.Right = next.Equals(removingNode) ? next.Right : next;
                }
            }
            Count--;
        }
        public bool Contains(TKey key, TValue value)
        {
            var currentNode = _heads[0].Right;

            while (currentNode != null && currentNode.Key.CompareTo(key) != 1)
            {
                if (currentNode.Key.Equals(key) && currentNode.Value.Equals(value)) return true;
                currentNode = currentNode.Right;
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get { return GetNodeByKey(key).Value; }
            set { GetNodeByKey(key).Value = value; }
        }






        private void SetEdges(out Stack<Node<TKey, TValue>> previous, out Stack<Node<TKey, TValue>> nexts, Node<TKey, TValue> checkNode)
        {
            previous = new Stack<Node<TKey, TValue>>();
            nexts = new Stack<Node<TKey, TValue>>();

            var curruntNode = _heads.Last();
            for (int i = 0; i < _heads.Length; i++)
            {
                while (curruntNode.Right != null && checkNode > curruntNode.Right)
                    curruntNode = curruntNode.Right;

                previous.Push(curruntNode);
                nexts.Push(curruntNode.Right);
                curruntNode = curruntNode.Down;
            }
        }

        private void Insert(Node<TKey,TValue> newNode)
        {
            Stack<Node<TKey, TValue>> nextNodes;
            Stack<Node<TKey, TValue>> previousNodes;
            SetEdges(out previousNodes, out nextNodes, newNode);

            if (newNode == nextNodes.Peek()) return;

            var heighNode = GetRandomHeigh();

            newNode.Right = nextNodes.Pop();
            previousNodes.Pop().Right = newNode;

            for (int i = 1; i < heighNode; i++)
            {
                var upNode = new Node<TKey, TValue>(newNode);
                upNode.Down = newNode;
                newNode.Up = upNode;
                newNode = newNode.Up;

                newNode.Right = nextNodes.Pop();
                previousNodes.Pop().Right = newNode;
            }
            Count++;
        }

       
        private void ItitializeHeads()
        {
            _heads = new Node<TKey, TValue>[Heigh];
            for (int i = 0; i < Heigh; i++)
            {
                _heads[i] = new Node<TKey, TValue>();
            }
            for (int i = 0; i < _heads.Length-1; i++)
            {
                _heads[i].Up = _heads[i + 1];
            }
            for (int i = 1; i < _heads.Length; i++)
            {
                _heads[i].Down = _heads[i - 1];
            }
        }

        private Node<TKey, TValue> GetNodeByKey(TKey key)
        {
            var currentNode = _heads[0].Right;
            while (currentNode != null && currentNode.Key.CompareTo(key) != 1)
            {
                if (currentNode.Key.Equals(key)) return currentNode;
                currentNode = currentNode.Right;
            }
            throw new KeyNotFoundException("key is not found");
        }

        private int GetRandomHeigh()
        {
            int defaultHeigh = 1;
            while (_random.NextDouble() < Probability && defaultHeigh < Heigh)
            {
                defaultHeigh++;
            }
            return defaultHeigh;
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var currentNode = _heads[0].Right;
            while (currentNode != null)
            {
                yield return new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);
                currentNode = currentNode.Right;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
