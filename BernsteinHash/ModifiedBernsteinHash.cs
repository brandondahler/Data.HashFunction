using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class ModifiedBernsteinHash
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32 }; } }


        public ModifiedBernsteinHash()
            : base(32)
        { 
            
        }


        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize != 32)
                throw new ArgumentOutOfRangeException("HashSize");


            UInt32 h = 0;

            foreach (var b in data)
                h = (33 * h) ^ b;

            return BitConverter.GetBytes(h);
        }
    }
}
