using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Blake2
{
    /// <summary>
    /// Implementation of BLAKE2b as specified at https://blake2.net/.  Implementations are expected to support
    /// hash output sizes of 8 through 512 bits in 8-bit increments and allowing it to be seeded with a key, salt,
    /// and/or personalization sequence.
    /// </summary>
    /// <seealso cref="IHashFunctionAsync" />
    public interface IBlake2B
        : IHashFunctionAsync
    {
        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IBlake2BConfig Config { get; }
    }
}
