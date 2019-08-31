using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ISpookyHashV2"/>.
    /// </summary>
    public class SpookyHashV2Factory
        : ISpookyHashV2Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static ISpookyHashV2Factory Instance { get; } = new SpookyHashV2Factory();


        private SpookyHashV2Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="ISpookyHashV2"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="ISpookyHashV2"/> instance.</returns>
        public ISpookyHashV2 Create() =>
            Create(new SpookyHashConfig());

        /// <summary>
        /// Creates a new <see cref="ISpookyHashV2"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="ISpookyHashV2"/> instance.</returns>
        public ISpookyHashV2 Create(ISpookyHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new SpookyHashV2_Implementation(config);
        }
    }
}
