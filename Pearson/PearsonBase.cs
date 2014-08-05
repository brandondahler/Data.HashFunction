using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Abstract implementation of Pearson hashing as specified at http://en.wikipedia.org/wiki/Pearson_hashing and
    ///   http://cs.mwsu.edu/~griffin/courses/2133/downloads/Spring11/p677-pearson.pdf.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors", 
        Justification = "Constructor required to validate implementer's parameters.")]
    public abstract class PearsonBase
        : HashFunctionBase
    {
        /// <summary>
        /// 256-item read only collection of bytes.  Must be a permutation of [0, 255].
        /// </summary>
        /// <value>
        /// The 256-item read only collection of bytes.
        /// </value>
        public IReadOnlyList<byte> T { get { return _T; } }


        private readonly IReadOnlyList<byte> _T;



        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 8.
        /// </remarks>
        /// <inheritdoc cref="PearsonBase(IReadOnlyList{byte}, int)" />
        public PearsonBase(IReadOnlyList<byte> t)
            : this(t, 8)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PearsonBase"/> class.
        /// </summary>
        /// <param name="t"><inheritdoc cref="T" /></param>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" />.  hashSize is allowed to be any positive integer that is divisible by 8.</param>
        /// <exception cref="System.ArgumentNullException">t</exception>
        /// <exception cref="System.ArgumentException">t must be a permutation of [0, 255].;t</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be a positive integer that is divisible by 8.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public PearsonBase(IReadOnlyList<byte> t, int hashSize)
            : base(hashSize)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            if (t.Count != 256 || t.Distinct().Count() != 256)
                throw new ArgumentException("t must be a permutation of [0, 255].", "t");


            if (hashSize <= 0 || hashSize % 8 != 0)
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be a positive integer that is divisible by 8.");



            _T = t;
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (HashSize <= 0 || HashSize % 8 != 0)
                throw new InvalidOperationException("HashSize set to an invalid value.");


            var h = new byte[HashSize / 8];
            bool firstByte = true;

            foreach (var dataByte in data.AsEnumerable())
            {
                for (int x = 0; x < HashSize / 8; ++x)
                {
                    if (!firstByte)
                        h[x] = T[h[x] ^ dataByte];
                    else
                        h[x] = T[(dataByte + x) & 0xff];
                }

                firstByte = false;
            }

            return h;
        }
    }
}
