using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public abstract class PearsonBase
        : HashFunctionBase
    {
        /// <summary>
        /// 8, 16, 24, 32, ..., 2016, 2024, 2032, 2040
        /// </summary>
        public override IEnumerable<int> ValidHashSizes
        {
            get 
            { 
                return Enumerable.Range(1, 255)
                    .Select(x => x * 8); 
            }
        }

        /// <summary>
        /// 256-item array of bytes.  Must be a permutation of [1, 256].
        /// </summary>
        protected abstract byte[] T { get; }

        private readonly byte[] ValueTables = null;

        public PearsonBase()
            : base(8)
        {
            ValueTables = T;
        }


        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize < 8 || HashSize > 2040 || HashSize % 8 != 0)
                throw new ArgumentOutOfRangeException("HashSize");

            var h = new byte[HashSize / 8];

            for (int x = 0; x < HashSize / 8; ++x)
            {
                byte currentH = 0;

                // Handle data's item 0
                if (data.Length > 0)
                    currentH = ValueTables[currentH ^ ((data[0] + x) & 0xff)];

                // Handle data's items 1 to data.Length
                for (int y = 1; y < data.Length; ++y)
                    currentH = ValueTables[currentH ^ data[y]];

                h[x] = currentH;
            }

            return h;
        }
    }
}
