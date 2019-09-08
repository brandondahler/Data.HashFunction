using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IBuzHash"/>.
    /// </summary>
    /// <seealso cref="IBuzHashFactory" />
    public sealed class BuzHashFactory
        : IBuzHashFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IBuzHashFactory Instance { get; } = new BuzHashFactory();


        private BuzHashFactory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IBuzHash" /> instance with the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="IBuzHash" /> instance.
        /// </returns>
        public IBuzHash Create()
        {
            return Create(new DefaultBuzHashConfig());
        }

        /// <summary>
        /// Creates a new <see cref="IBuzHash" /> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IBuzHash" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        public IBuzHash Create(IBuzHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new BuzHash_Implementation(config);
        }
    }
}
