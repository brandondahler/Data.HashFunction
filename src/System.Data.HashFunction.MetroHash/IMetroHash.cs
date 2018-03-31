using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Implementation of MetroHash as specified at https://github.com/jandrewrogers/MetroHash.
    /// 
    /// "
    /// MetroHash is a set of state-of-the-art hash functions for non-cryptographic use cases. 
    /// They are notable for being algorithmically generated in addition to their exceptional performance. 
    /// The set of published hash functions may be expanded in the future, 
    /// having been selected from a very large set of hash functions that have been constructed this way.
    /// "
    /// </summary>
    /// <seealso cref="IHashFunctionAsync" />
    public interface IMetroHash
        : IHashFunctionAsync
    {
        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IMetroHashConfig Config { get; }
    }
}
