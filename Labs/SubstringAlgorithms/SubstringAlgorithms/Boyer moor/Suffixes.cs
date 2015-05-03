using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubstringAlgorithms.Boyer_moor
{
    public class Suffixes
    {
        private Dictionary<string, int> _suffixes;
        private string _sample;
        public Suffixes(string sample)
        {
            _sample = sample;
            _suffixes= new Dictionary<string, int>();
            BuildStopSuffixes();
        }

        private void BuildStopSuffixes()
        {
            for (int i = 1; i <= _sample.Length; i++)
            {
                Add(_sample.Substring(_sample.Length - i, i));
            }
        }

        private void Add(string suffix)
        {
            var subSample = _sample.Substring(0, _sample.Length - suffix.Length);
            if (subSample.LastIndexOf(suffix) != -1)
            {
                _suffixes.Add(suffix, _sample.Length - suffix.Length - subSample.LastIndexOf(suffix));
            }
            else
            {
                var shiftPreviousSuffix = _suffixes[suffix.Substring(1, suffix.Length-1)];
                _suffixes.Add(suffix,shiftPreviousSuffix);
            }
        }

        public int this[string suffix]
        {
            get
            {
                if (_suffixes.ContainsKey(suffix))
                {
                    return _suffixes[suffix];
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
