using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.xxHash
{
    /// <summary>
    /// Implements xxHash as specified at https://github.com/Cyan4973/xxHash/blob/dev/xxhash.c and 
    ///   https://github.com/Cyan4973/xxHash.
    /// </summary>
    public interface IxxHash
        : IHashFunctionAsync
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IxxHashConfig Config { get; }

    }
}
