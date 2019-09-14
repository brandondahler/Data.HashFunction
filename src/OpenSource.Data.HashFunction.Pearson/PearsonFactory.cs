using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Pearson
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IPearson"/>.
    /// </summary>
    public class PearsonFactory
        : IPearsonFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IPearsonFactory Instance { get; } = new PearsonFactory();


        private PearsonFactory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IPearson"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IPearson"/> instance.</returns>
        /// <remarks>
        /// Implementation uses a default instance of <see cref="WikipediaPearsonConfig"/>
        /// </remarks>
        public IPearson Create() =>
            Create(new WikipediaPearsonConfig());

        /// <summary>
        /// Creates a new <see cref="IPearson"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IPearson"/> instance.</returns>
        public IPearson Create(IPearsonConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new Pearson_Implementation(config);
        }
    }
}
