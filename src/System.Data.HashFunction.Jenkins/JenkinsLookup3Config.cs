using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public class JenkinsLookup3Config
        : IJenkinsLookup3Config
    {
        public int HashSizeInBits { get; set; } = 32;

        public UInt32 Seed { get; set; } = 0U;
        public UInt32 Seed2 { get; set; } = 0U;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IJenkinsLookup3Config Clone() =>
            new JenkinsLookup3Config() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed,
                Seed2 = Seed2
            };
    }
}
