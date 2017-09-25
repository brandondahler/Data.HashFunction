using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public class MurmurHash1Config
        : IMurmurHash1Config
    {
        public UInt32 Seed { get; set; } = 0U;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IMurmurHash1Config Clone() => 
            new MurmurHash1Config() {
                Seed = Seed
            };
    }
}
