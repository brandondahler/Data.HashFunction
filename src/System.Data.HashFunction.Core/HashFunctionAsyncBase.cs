using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core
{
    /// <summary>
    /// Abstract implementation of an <see cref="IHashFunctionAsync"/>.
    /// Provides convenience checks and ensures a default HashSize has been set at construction.
    /// </summary>
    public abstract class HashFunctionAsyncBase 
        : HashFunctionBase, 
            IHashFunctionAsync
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
        public Task<IHashValue> ComputeHashAsync(Stream inputStream) => ComputeHashAsync(inputStream, null);

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
        public Task<IHashValue> ComputeHashAsync(Stream inputStream, CancellationToken cancellationToken) => ComputeHashAsync(inputStream, null, cancellationToken);

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
        public Task<IHashValue> ComputeHashAsync(Stream inputStream, Stream outputStream) => ComputeHashAsync(inputStream, outputStream, CancellationToken.None);

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
        public async Task<IHashValue> ComputeHashAsync(Stream inputStream, Stream outputStream, CancellationToken cancellationToken)
        {
            if (inputStream == null)
                throw new ArgumentNullException(nameof(inputStream));

            if (!inputStream.CanRead)
                throw new ArgumentException("Stream must be readable.", nameof(inputStream));

            if (outputStream != null && !outputStream.CanWrite)
                throw new ArgumentException("Stream must be writable.", nameof(outputStream));


            cancellationToken.ThrowIfCancellationRequested();


            return new HashValue(
                await ComputeHashAsyncInternal(new StreamData(inputStream, outputStream), cancellationToken)
                    .ConfigureAwait(false),
                HashSizeInBits);
        }



        /// <summary>
        /// Computes hash value for given stream asynchronously.
        /// </summary>
        /// <param name="data">Data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of data as byte array.
        /// </returns>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        protected abstract Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken);

    }
}
