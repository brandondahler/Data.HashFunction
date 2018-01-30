using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Implementation of MurmurHash1 as specified at https://github.com/aappleby/smhasher/blob/master/src/MurmurHash1.cpp 
    ///   and https://github.com/aappleby/smhasher/wiki/MurmurHash1.
    /// 
    /// This hash function has been superseded by <seealso cref="IMurmurHash2">MurmurHash2</seealso> and <seealso cref="IMurmurHash3">MurmurHash3</seealso>.
    /// </summary>
    public interface IMurmurHash1
        : IMurmurHash
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IMurmurHash1Config Config { get; }

    }
}
