using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FNV
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFNV1"/>.
    /// </summary>
    public interface IFNV1Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFNV1"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IFNV1"/> instance.</returns>
        IFNV1 Create();

        /// <summary>
        /// Creates a new <see cref="IFNV1"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IFNV1"/> instance.</returns>
        IFNV1 Create(IFNVConfig config);
    }
}
