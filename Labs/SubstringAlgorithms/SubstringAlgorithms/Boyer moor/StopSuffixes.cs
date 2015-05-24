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

            if (_sample.Length == 1)
            {
                _stopSuffixes.Add(_sample, _providerStopSymbols[_sample[_sample.Length - 1]]);
                return;
            }

            _stopSuffixes.Add(_sample[_sample.Length-1].ToString(), _providerStopSymbols[_sample[_sample.Length - 1]]);

            for (int i = _sample.Length - 2; i >= 0; i--)
            {
                var suffix = _sample.Substring(i, _sample.Length - i);
                var other = _sample.Substring(0, i);
                var position = other.LastIndexOf(suffix);
                if (position == -1)
                {
                    var subSuffix = suffix;
                    for (int j = 1; j < suffix.Length && (_sample.IndexOf(subSuffix) != 0 || subSuffix.Length == _sample.Length) && subSuffix.Length != 0; j++)
                    {
                        subSuffix = subSuffix.Substring(1, subSuffix.Length - 1);
                    }
                    if (subSuffix.Length != 0)
                    {
                        Add(suffix, _stopSuffixes[subSuffix]);
                    }
                    else
                    {
                        Add(suffix, _sample.Length);
                    }
                }
                else
                {
                    Add(suffix, _sample.Length - position - suffix.Length);
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
            get { return _stopSuffixes[suffix]; }
        }
    }
}
