using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.HashAlgorithm
{
    /// <summary>
    /// Implementation of <see cref="IHashFunction" /> that wraps cryptographic hash functions known as <see cref="System.Security.Cryptography.HashAlgorithm" />.
    /// </summary>
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
