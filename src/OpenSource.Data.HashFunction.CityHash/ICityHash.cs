using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.CityHash
{

    /// <summary>
    /// Implementation of CityHash as specified at https://code.google.com/p/cityhash/.
    /// 
    /// "
    /// CityHash provides hash functions for strings. The functions mix the 
    ///   input bits thoroughly but are not suitable for cryptography. 
    ///  
    /// [Hash size of 128-bits is] tuned for strings of at least a few hundred bytes. 
    /// Depending on your compiler and hardware, it's likely faster than [the hash size of 64-bits] on 
    ///   sufficiently long strings. 
    /// It's slower than necessary on shorter strings, but we expect that case to be relatively unimportant.
    /// "
    /// </summary>
    public interface ICityHash
        : IHashFunction
    {
        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        ICityHashConfig Config { get; }

    }
}
