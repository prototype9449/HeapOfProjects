using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SubstringAlgorithms.Boyer_moor
{
    public class StopSuffixes
    {
        private Dictionary<string, int> _stopSuffixes;
        private string _sample;
        private StopSymbols _providerStopSymbols;
        public StopSuffixes(string sample)
        {
            _sample = sample;
            _stopSuffixes = new Dictionary<string, int>();
            _providerStopSymbols = new StopSymbols(sample);
            BuildStopSuffixes();
        }

        private void BuildStopSuffixes()
        {
            for (int i = _sample.Length-1; i >= 0 ; i--)
            {
                var suffix = _sample.Substring(i, _sample.Length - i);
                var other = _sample.Substring(0, i);
                var position = other.LastIndexOf(suffix);
                if (position == -1)
                {
                    Add(suffix, _sample.Length);
                }
                else
                {
                    Add(suffix,_sample.Length-position-suffix.Length);
                }
            }
        }

        private void Add(string suffix, int shift)
        {
            if (!_stopSuffixes.ContainsKey(suffix))
            {
                _stopSuffixes.Add(suffix, shift);
            }
            else
            {
                _stopSuffixes[suffix] = shift;
            }
        }

        public int this[string suffix]
        {
            get
            {
                if (_stopSuffixes.ContainsKey(suffix))
                {
                    return _stopSuffixes[suffix];
                }
                else if(suffix.Count()!=1)
                {
                    return _sample.Length;
                }
                else
                {
                    return _providerStopSymbols[suffix[0]];
                }
            }
        }
    }
}
