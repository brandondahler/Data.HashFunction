using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Implementation of MurmurHash3 as specified at https://github.com/aappleby/smhasher/blob/master/src/MurmurHash3.cpp 
    ///   and https://github.com/aappleby/smhasher/wiki/MurmurHash3.
    /// </summary>
    public interface IMurmurHash3
        : IMurmurHash
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IMurmurHash3Config Config { get; }

    }
}
