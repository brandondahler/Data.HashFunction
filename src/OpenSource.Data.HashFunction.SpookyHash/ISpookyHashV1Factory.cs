using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 // Type or member is obsolete
namespace OpenSource.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ISpookyHashV1"/>.
    /// </summary>
    public interface ISpookyHashV1Factory
    {
        /// <summary>
        /// Creates a new <see cref="ISpookyHashV1"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="ISpookyHashV1"/> instance.</returns>
        ISpookyHashV1 Create();

        /// <summary>
        /// Creates a new <see cref="ISpookyHashV1"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="ISpookyHashV1"/> instance.</returns>
        ISpookyHashV1 Create(ISpookyHashConfig config);
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
