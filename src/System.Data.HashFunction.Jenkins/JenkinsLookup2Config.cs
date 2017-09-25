using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public class JenkinsLookup2Config
        : IJenkinsLookup2Config
    {
        public UInt32 Seed { get; set; } = 0U;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IJenkinsLookup2Config Clone() => 
            new JenkinsLookup2Config() {
                Seed = Seed
            };
    }
}
