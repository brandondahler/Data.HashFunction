using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IDefaultBuzHash"/>.
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
        /// Creates a new <see cref="IDefaultBuzHash" /> instance with the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="IDefaultBuzHash" /> instance.
        /// </returns>
        public IBuzHash Create()
        {
            return Create(new DefaultBuzHashConfig());
        }

        /// <summary>
        /// Creates a new <see cref="IDefaultBuzHash" /> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IDefaultBuzHash" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">config</exception>
        public IBuzHash Create(IBuzHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new BuzHash_Implementation(config);
        }
    }
}
