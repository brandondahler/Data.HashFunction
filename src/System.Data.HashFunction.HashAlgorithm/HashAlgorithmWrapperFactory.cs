using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.HashAlgorithm
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IHashAlgorithmWrapper"/>.
    /// </summary>
    public class HashAlgorithmWrapperFactory
        : IHashAlgorithmWrapperFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IHashAlgorithmWrapperFactory Instance { get; } = new HashAlgorithmWrapperFactory();


        private HashAlgorithmWrapperFactory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IHashAlgorithmWrapper"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>A <see cref="IHashAlgorithmWrapper"/> instance.</returns>
        public IHashAlgorithmWrapper Create(IHashAlgorithmWrapperConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new HashAlgorithmWrapper_Implementation(config);
        }
    }
}
