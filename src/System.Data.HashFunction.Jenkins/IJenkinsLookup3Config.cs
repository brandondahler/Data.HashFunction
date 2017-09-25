using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public interface IJenkinsLookup3Config
    {
        int HashSizeInBits { get; }

        UInt32 Seed { get; }

        UInt32 Seed2 { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IJenkinsLookup3Config Clone();
    }
}
