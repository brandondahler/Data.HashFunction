using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public interface IMurmurHash2Config
    {
        int HashSizeInBits { get; }

        UInt64 Seed { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IMurmurHash2Config Clone();
    }
}
