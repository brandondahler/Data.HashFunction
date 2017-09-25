using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public class MurmurHash3Config
        : IMurmurHash3Config
    {
        public int HashSizeInBits { get; set; } = 32;

        public UInt32 Seed { get; set; } = 0U;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IMurmurHash3Config Clone() =>
            new MurmurHash3Config() { 
                HashSizeInBits = HashSizeInBits,
                Seed = Seed
            };
    }
}
