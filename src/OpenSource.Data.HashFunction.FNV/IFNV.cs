using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    /// <summary>
    /// Implementation of Fowler–Noll–Vo hash function (FNV-1 or FNV-1a) as specified at http://www.isthe.com/chongo/tech/comp/fnv/index.html. 
    /// </summary>
    public interface IFNV
        : IHashFunctionAsync
    {
        
        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IFNVConfig Config { get; }

    }
}
