using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FNV
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFNV1a"/>.
    /// </summary>
    public class FNV1aFactory
        : IFNV1aFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFNV1aFactory Instance { get; } = new FNV1aFactory();


        private FNV1aFactory()
        {

        }

        /// <summary>
        /// Creates a new <see cref="IFNV1a"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IFNV1a"/> instance.</returns>
        public IFNV1a Create()
        {
            return Create(
                FNVConfig.GetPredefinedConfig(64));
        }

        /// <summary>
        /// Creates a new <see cref="IFNV1a"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IFNV1a"/> instance.</returns>
        public IFNV1a Create(IFNVConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new FNV1a_Implementation(config);
        }
    }
}
