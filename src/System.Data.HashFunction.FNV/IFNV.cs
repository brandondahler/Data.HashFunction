using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FNV
{
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
