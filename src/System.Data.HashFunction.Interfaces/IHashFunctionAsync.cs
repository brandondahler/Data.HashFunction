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
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <remarks>
        /// All stream IO is done via ReadAsync.
        /// </remarks>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        Task<IHashValue> ComputeHashAsync(Stream inputStream);

        /// <summary>
        /// Computes hash value for given stream asynchronously.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <remarks>
        /// All stream IO is done via ReadAsync.
        /// </remarks>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        Task<IHashValue> ComputeHashAsync(Stream inputStream, CancellationToken cancellationToken);


        /// <summary>
        /// Computes hash value for given stream asynchronously.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <param name="outputStream">Stream to write the read data to.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <remarks>
        /// All stream IO is done via ReadAsync.
        /// All outputStream IO is done via WriteAsync.
        /// </remarks>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be writable.;<paramref name="outputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        Task<IHashValue> ComputeHashAsync(Stream inputStream, Stream outputStream);

        /// <summary>
        /// Computes hash value for given stream asynchronously while outputting the read bytes to the second stream asynchronously.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <param name="outputStream">Stream to write the read data to.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <remarks>
        /// All inputStream IO is done via ReadAsync.
        /// All outputStream IO is done via WriteAsync.
        /// </remarks>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be writable.;<paramref name="outputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        Task<IHashValue> ComputeHashAsync(Stream inputStream, Stream outputStream, CancellationToken cancellationToken);
    }
}
