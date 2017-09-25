using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public class MurmurHash2Config
        : IMurmurHash2Config
    {
        public int HashSizeInBits { get; set; } = 64;

        public UInt64 Seed { get; set; } = 0UL;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IMurmurHash2Config Clone() => 
            new MurmurHash2Config() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed
            };
    }
}
