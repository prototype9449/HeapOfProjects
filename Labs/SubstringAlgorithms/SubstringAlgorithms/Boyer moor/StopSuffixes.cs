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
                    var subSuffix = suffix;
                    for (int i = 1; i < suffix.Length && (_sample.IndexOf(subSuffix) != 0 || subSuffix.Length == _sample.Length) && subSuffix.Length != 0; i++)
                    {
                        subSuffix = subSuffix.Substring(1, subSuffix.Length - 1);
                    }
                    //while (_sample.IndexOf(subSuffix) != 0 && subSuffix.Length != 0)
                    //{
                    //    subSuffix = subSuffix.Substring(0, subSuffix.Length - 1);
                    //}
                    if (subSuffix.Length != 0)
                    {
                        if (_stopSuffixes.ContainsKey(subSuffix))
                            return _stopSuffixes[subSuffix];
                    }
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
