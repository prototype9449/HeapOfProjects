using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableDoubleHashing
{
    public interface ISizeTable
    {
        int Size { get; }
        void IncriseSize();
        int GetHashValue(int number);
    }
}
