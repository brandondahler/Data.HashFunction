using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.xxHash
{
    public interface IxxHashConfig
    {
        int HashSizeInBits { get; }

        UInt64 Seed { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IxxHashConfig Clone();
    }
}
