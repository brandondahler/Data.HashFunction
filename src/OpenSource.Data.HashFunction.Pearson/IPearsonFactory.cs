using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Pearson
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IPearson"/>.
    /// </summary>
    public interface IPearsonFactory
    {
        /// <summary>
        /// Creates a new <see cref="IPearson"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IPearson"/> instance.</returns>
        IPearson Create();

        /// <summary>
        /// Creates a new <see cref="IPearson"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IPearson"/> instance.</returns>
        IPearson Create(IPearsonConfig config);
    }
}
