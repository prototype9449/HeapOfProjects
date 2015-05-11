using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubstringAlgorithms.Boyer_moor
{
    public class StopSymbols
    {
        private Dictionary<char, int> _stopSymbols;
        private string _sample;
        public StopSymbols(string sample)
        {
            _sample = sample;
            _stopSymbols= new Dictionary<char, int>();
            BuildStopSymblos();
        }

        private void BuildStopSymblos()
        {
            for (int i = 0; i < _sample.Length-1; i++)
            {
                Add(_sample[i], _sample.Length - i-1);
            }
        }

        private void Add(char symbol, int shift)
        {
            if (!_stopSymbols.ContainsKey(symbol))
            {
                _stopSymbols.Add(symbol, shift);
            }
            else
            {
                _stopSymbols[symbol] = shift;
            }
        }

        public int this[char symbol]
        {
            get
            {
                int a = Convert.ToInt16("2");
                if (_stopSymbols.ContainsKey(symbol))
                {
                    return _stopSymbols[symbol];
                }
                else
                {
                    return _sample.Length;
                }
            }
        }
    }
}
