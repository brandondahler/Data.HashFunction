using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="data"/></exception>
        IHashValue ComputeHash(Stream data);

    }
}
