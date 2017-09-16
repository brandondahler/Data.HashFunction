using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IDefaultBuzHash"/>.
    /// </summary>
    /// <seealso cref="IDefaultBuzHashFactory" />
    public class DefaultBuzHashFactory
        : IDefaultBuzHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IDefaultBuzHash" /> instance with the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="IDefaultBuzHash" /> instance.
        /// </returns>
        public IDefaultBuzHash Create()
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
        public IDefaultBuzHash Create(IDefaultBuzHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new DefaultBuzHash_Implementation(config.ShiftDirection, config.HashSizeInBits);
        }
    }
}
