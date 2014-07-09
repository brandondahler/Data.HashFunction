using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class ELF64
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32 }; } }


        public ELF64()
            : base(32)
        {

        }


        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize != 32)
                throw new ArgumentOutOfRangeException("HashSize");

            UInt32 hash = 0;

            foreach (var b in data)
            {
                hash <<= 4;
                hash += b;

                var tmp = hash & 0xF0000000;

		        if (tmp != 0)
		            hash ^= tmp >> 24;
		        
                hash &= 0x0FFFFFFF;
            }

            return BitConverter.GetBytes(hash);
        }
    }
}
