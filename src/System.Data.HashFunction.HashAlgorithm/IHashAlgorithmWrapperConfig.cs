using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.HashAlgorithm
{
    using HashAlgorithm = System.Security.Cryptography.HashAlgorithm;

    /// <summary>
    /// Defines a configuration for a <see cref="IHashAlgorithmWrapper"/> implementation.
    /// </summary>
    public interface IHashAlgorithmWrapperConfig
    {
        /// <summary>
        /// A delegate that produces <see cref="HashAlgorithm"/> instances.
        /// </summary>
        /// <value>
        /// The delegate.
        /// </value>
        Func<HashAlgorithm> InstanceFactory { get; }

        

        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IHashAlgorithmWrapperConfig Clone();
    }
}