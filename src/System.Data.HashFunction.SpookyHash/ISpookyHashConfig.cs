using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    public interface ISpookyHashConfig
    {
        int HashSizeInBits { get; }

        UInt64 Seed { get; }
        UInt64 Seed2 { get; }


        
        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        ISpookyHashConfig Clone();
    }
}
