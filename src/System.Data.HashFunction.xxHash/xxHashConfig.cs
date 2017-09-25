using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.xxHash
{
    public class xxHashConfig
        : IxxHashConfig
    {
        public int HashSizeInBits { get; set; } = 32;

        public UInt64 Seed { get; set; } = 0UL;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IxxHashConfig Clone() =>
            new xxHashConfig() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed
            };
    }
}
