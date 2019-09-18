using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Blake2
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IBlake2B"/>.
    /// </summary>
    public sealed class Blake2BFactory
        : IBlake2BFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IBlake2BFactory Instance { get; } = new Blake2BFactory();


        private Blake2BFactory()
        {

        }

        /// <summary>
        /// Creates a new <see cref="IBlake2B" /> instance with the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="IBlake2B" /> instance.
        /// </returns>
        public IBlake2B Create()
        {
            return Create(new Blake2BConfig());
        }

        /// <summary>
        /// Creates a new <see cref="IBlake2B" /> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IBlake2B" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        public IBlake2B Create(IBlake2BConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new Blake2B_Implementation(config);
        }
    }
}
