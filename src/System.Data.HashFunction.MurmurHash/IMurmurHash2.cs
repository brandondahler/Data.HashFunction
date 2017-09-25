using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public interface IMurmurHash2
        : IMurmurHash
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IMurmurHash2Config Config { get; }

    }
}
