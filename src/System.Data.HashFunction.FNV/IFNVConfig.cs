using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    public interface IFNVConfig
    {
        int HashSizeInBits { get; }

        BigInteger Prime { get; }
        BigInteger Offset { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IFNVConfig Clone();
    }
}
