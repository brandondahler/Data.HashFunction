using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Pearson
{
    public interface IPearsonConfig
    {
        IReadOnlyList<byte> Table { get; }

        int HashSizeInBits { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IPearsonConfig Clone();
    }
}
