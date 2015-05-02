using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableDoubleHashing
{
    public class PairHashValue<T>
    {
        public T Value { get; set; }
        public bool IsDeleted { get; set; }
        public PairHashValue(T value)
        {
            Value = value;
            IsDeleted = false;
        }
    }
}
