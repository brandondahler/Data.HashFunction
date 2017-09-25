using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public interface IJenkinsLookup2Config
    {
        UInt32 Seed { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IJenkinsLookup2Config Clone();
    }
}
