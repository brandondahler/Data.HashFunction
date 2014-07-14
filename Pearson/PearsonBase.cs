using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Abstract implementation of Pearson hashing as specified at http://en.wikipedia.org/wiki/Pearson_hashing and
    ///   http://cs.mwsu.edu/~griffin/courses/2133/downloads/Spring11/p677-pearson.pdf.
    /// </summary>
    public abstract class PearsonBase
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes
        {
            get 
            {
                // 8, 16, 24, 32, ..., 2016, 2024, 2032, 2040
                return Enumerable.Range(1, 255)
                    .Select(x => x * 8); 
            }
        }


        /// <summary>
        /// 256-item read only collection of bytes.  Must be a permutation of [0, 255].
        /// </summary>
        /// <remarks>
        /// It is strongly recommended to return a static read only byte array reference.
        /// </remarks>
        protected abstract IReadOnlyList<byte> T { get; }


        /// <summary>
        /// Constructs new <see cref="PearsonBase"/> instance.
        /// </summary>
        /// <param name="defaultHashSize">Default value for the <see cref="HashFunctionBase.HashSize"/> property.</param>
        public PearsonBase(int defaultHashSize = 8)
            : base(defaultHashSize)
        {

        }


        /// <inheritdoc/>
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
                    currentH = T[currentH ^ ((data[0] + x) & 0xff)];

                // Handle data's items 1 to data.Length
                for (int y = 1; y < data.Length; ++y)
                    currentH = T[currentH ^ data[y]];

                h[x] = currentH;
            }

            return h;
        }
    }
}
