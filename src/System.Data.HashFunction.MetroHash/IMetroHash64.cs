using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Implementation of MetroHash64 as specified at https://github.com/jandrewrogers/MetroHash.
    /// 
    /// "
    /// MetroHash is a set of state-of-the-art hash functions for non-cryptographic use cases. 
    /// They are notable for being algorithmically generated in addition to their exceptional performance. 
    /// The set of published hash functions may be expanded in the future, 
    /// having been selected from a very large set of hash functions that have been constructed this way.
    /// "
    /// </summary>
    /// <seealso cref="IMetroHash" />
    /// <seealso cref="IHashFunctionAsync" />
    public interface IMetroHash64
        : IMetroHash
    {

    }
}
