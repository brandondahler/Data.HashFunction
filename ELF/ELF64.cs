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
            : base(64)
        {

        }


        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize != 64)
                throw new ArgumentOutOfRangeException("HashSize is set to an invalid value.");

            var hash = 0UL;

            foreach (var b in data)
            {
                var tmp = hash & 0xF0000000;

		        if (tmp != 0)
		            hash ^= tmp >> 24;
		        
                hash &= ~tmp;
            }

            return BitConverter.GetBytes(hash);
        }
    }
}
