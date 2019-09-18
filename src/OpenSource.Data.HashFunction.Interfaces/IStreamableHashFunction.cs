using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction
{
    /// <summary>
    /// Common interface to non-cryptographic hash functions that can be computed over a stream of data without buffering.
    /// </summary>
    public interface IStreamableHashFunction
        : IHashFunction
    {
        /// <summary>
        /// Creates a new transformer that will process data and hold the internal state using this hash function's algorithm.
        /// </summary>
        /// <returns>A new instance of <see cref="IHashFunctionBlockTransformer"/> that can process input iteratively and produce a final <see cref="IHashValue"/> for that input.</returns>
        IHashFunctionBlockTransformer CreateBlockTransformer();

        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="data"/></exception>
        IHashValue ComputeHash(Stream data);

        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        IHashValue ComputeHash(Stream data, CancellationToken cancellationToken);

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
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="data"/></exception>
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
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        Task<IHashValue> ComputeHashAsync(Stream data, CancellationToken cancellationToken);
    }
}
