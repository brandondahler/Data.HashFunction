using System.Collections.Generic;
using System.IO;
using System.Threading;
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
        /// Hash value of the data.
        /// </returns>
        /// <remarks>
        /// All stream IO is done via ReadAsync.
        /// </remarks>
        Task<IHashValue> ComputeHashAsync(Stream data);

        /// <summary>
        /// Computes hash value for given stream asynchronously.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <remarks>
        /// All stream IO is done via ReadAsync.
        /// </remarks>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        Task<IHashValue> ComputeHashAsync(Stream data, CancellationToken cancellationToken);
    }
}
