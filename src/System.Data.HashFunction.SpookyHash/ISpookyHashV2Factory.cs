using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ISpookyHashV2"/>.
    /// </summary>
    public interface ISpookyHashV2Factory
    {
        /// <summary>
        /// Creates a new <see cref="ISpookyHashV2"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="ISpookyHashV2"/> instance.</returns>
        ISpookyHashV2 Create();

        /// <summary>
        /// Creates a new <see cref="ISpookyHashV2"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="ISpookyHashV2"/> instance.</returns>
        ISpookyHashV2 Create(ISpookyHashConfig config);
    }
}
