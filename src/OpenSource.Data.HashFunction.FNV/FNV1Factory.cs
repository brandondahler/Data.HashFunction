using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FNV
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFNV1"/>.
    /// </summary>
    public class FNV1Factory
        : IFNV1Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFNV1Factory Instance { get; } = new FNV1Factory();


        private FNV1Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFNV1"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IFNV1"/> instance.</returns>
        public IFNV1 Create()
        {
            return Create(
                FNVConfig.GetPredefinedConfig(64));
        }

        /// <summary>
        /// Creates a new <see cref="IFNV1"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IFNV1"/> instance.</returns>
        public IFNV1 Create(IFNVConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new FNV1_Implementation(config);
        }
    }
}
