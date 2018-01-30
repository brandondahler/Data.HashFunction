using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.HashAlgorithm
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IHashAlgorithmWrapper"/>.
    /// </summary>
    public interface IHashAlgorithmWrapperFactory
    {

        /// <summary>
        /// Creates a new <see cref="IHashAlgorithmWrapper"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>A <see cref="IHashAlgorithmWrapper"/> instance.</returns>
        IHashAlgorithmWrapper Create(IHashAlgorithmWrapperConfig config);
    }
}
