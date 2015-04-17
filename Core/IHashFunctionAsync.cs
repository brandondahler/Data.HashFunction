#if !NET40
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Common interface to non-cryptographic hash functions.
    /// </summary>
    public interface IHashFunctionAsync
        : IHashFunction
    {
        /// <summary>
        /// Computes hash value for given stream asynchronously.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>
        /// Hash value of data as byte array.
        /// </returns>
        /// <remarks>
        /// All stream IO is done via ReadAsync.
        /// </remarks>
        Task<byte[]> ComputeHashAsync(Stream data);
    }
}
#endif