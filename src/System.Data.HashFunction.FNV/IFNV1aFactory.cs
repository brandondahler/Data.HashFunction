using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFNV1a"/>.
    /// </summary>
    public interface IFNV1aFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFNV1a"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IFNV1a"/> instance.</returns>
        IFNV1a Create();

        /// <summary>
        /// Creates a new <see cref="IFNV1a"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IFNV1a"/> instance.</returns>
        IFNV1a Create(IFNVConfig config);
    }
}
