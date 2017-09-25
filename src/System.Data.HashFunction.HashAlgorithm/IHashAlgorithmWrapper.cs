using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.HashAlgorithm
{
    public interface IHashAlgorithmWrapper
        : IHashFunction
    {
        
        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IHashAlgorithmWrapperConfig Config { get; }

    }
}
