using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public abstract class HashFunctionBase 
        : IHashFunction
    {
        public int HashSize 
        { 
            get { return _HashSize; }
            set
            {
                if (!ValidHashSizes.Contains(value))
                    throw new ArgumentOutOfRangeException();

                _HashSize = value;
            }
        }

        public abstract IEnumerable<int> ValidHashSizes { get; }


        private int _HashSize;

        protected HashFunctionBase(int defaultHashSize)
        {
            _HashSize = defaultHashSize;
        }

        public abstract byte[] ComputeHash(byte[] data);
    }
}
