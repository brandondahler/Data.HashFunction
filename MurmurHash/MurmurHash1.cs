using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of MurmurHash1 as specified at https://code.google.com/p/smhasher/source/browse/trunk/MurmurHash1.cpp 
    ///   and https://code.google.com/p/smhasher/wiki/MurmurHash1.
    /// 
    /// This hash function has been superseded by MurmurHash2 and MurmurHash3.
    /// </summary>
    public class MurmurHash1
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 32 }; }
        }

        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        public UInt32 Seed { get; set; }


        /// <inheritdoc/>
        protected override bool RequiresSeekableStream { get { return true; } }

        /// <summary>
        /// Constant m as defined by MurmurHash1 specification.
        /// </summary>
        protected const UInt32 m = 0XC6A4A793;

        /// <summary>
        /// Constructs new <see cref="MurmurHash1"/> instance.
        /// </summary>
        public MurmurHash1()
            : base(32)
        {
            Seed = 0;
        }


        /// <inheritdoc/>
        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (HashSize != 32)
                throw new ArgumentOutOfRangeException("HashSize");

            UInt32 h = Seed ^ ((UInt32) data.Length * m);
            var dataGroups = data.AsGroupedStreamData(4);

            
            foreach (var dataGroup in dataGroups)
            {
                h += BitConverter.ToUInt32(dataGroup, 0);
                h *= m;
                h ^= h >> 16;
            }
            

            var remainder = dataGroups.Remainder;

            switch(remainder.Length)
            {
                case 3: h += (UInt32) remainder[2] << 16;  goto case 2;
                case 2: h += (UInt32) remainder[1] <<  8;  goto case 1;
                case 1:
                    h += (UInt32) remainder[0];
                    h *= m;
                    h ^= h >> 16;
                    break;
            };
 
            h *= m;
            h ^= h >> 10;
            h *= m;
            h ^= h >> 17;

            return BitConverter.GetBytes(h);
        }
    }
}
