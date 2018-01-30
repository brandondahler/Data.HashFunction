using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.xxHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IxxHash"/>.
    /// </summary>
    public class xxHashFactory
        : IxxHashFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IxxHashFactory Instance { get; } = new xxHashFactory();


        private xxHashFactory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IxxHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IxxHash"/> instance.</returns>
        public IxxHash Create() =>
            Create(new xxHashConfig());

        /// <summary>
        /// Creates a new <see cref="IxxHash"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IxxHash"/> instance.</returns>
        public IxxHash Create(IxxHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new xxHash_Implementation(config);
        }
    }
}
