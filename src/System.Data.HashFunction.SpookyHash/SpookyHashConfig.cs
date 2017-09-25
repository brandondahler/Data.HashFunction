using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    public class SpookyHashConfig
        : ISpookyHashConfig
    {
        public int HashSizeInBits { get; set; } = 128;

        public UInt64 Seed { get; set; } = 0UL;
        public UInt64 Seed2 { get; set; } = 0UL;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public ISpookyHashConfig Clone() =>
            new SpookyHashConfig() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed,
                Seed2 = Seed2
            };
    }
}
