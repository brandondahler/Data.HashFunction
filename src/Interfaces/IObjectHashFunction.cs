using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Common interface to non-cryptographic hash function of an object.
    /// </summary>
    public interface IObjectHashFunction
    {
        /// <summary>
        /// Size of produced hash, in bits.
        /// </summary>
        /// <value>
        /// The size of the hash, in bits.
        /// </value>
        int HashSize { get; }

        /// <summary>
        /// Computes hash value for given object.
        /// </summary>
        /// <param name="object">Object to hash.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        byte[] CalculateHash(object @object);
    }
}
