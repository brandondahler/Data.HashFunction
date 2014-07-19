

using System.Collections.Generic;
using System.IO;
namespace System.Data.HashFunction
{
    /// <summary>
    /// Common interface to non-cryptographic hash functions.
    /// </summary>
    public interface IHashFunction
    {
        /// <summary>
        /// Size of produced hash, in bits.  Must be one of the values in <see cref="ValidHashSizes"/>.
        /// </summary>
        int HashSize { get; set; }

        /// <summary>
        /// Valid sizes of <see cref="HashSize"/>, in bits.
        /// </summary>
        IEnumerable<int> ValidHashSizes { get; }


        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <returns>Hash value of the data as byte array.</returns>
        byte[] ComputeHash(byte[] data);

        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>Hash value of data as byte array.</returns>
        byte[] ComputeHash(Stream data);
    }
}
