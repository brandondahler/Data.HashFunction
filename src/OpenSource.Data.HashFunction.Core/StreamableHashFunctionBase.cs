using OpenSource.Data.HashFunction.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Core
{
    /// <summary>
    /// Common interface to non-cryptographic hash functions that can be computed over a stream of data without buffering.
    /// </summary>
    public abstract class StreamableHashFunctionBase
        : HashFunctionBase,
            IStreamableHashFunction
    {
        /// <summary>
        /// Creates a new transformer that will process data and hold the internal state using this hash function's algorithm.
        /// </summary>
        /// <returns>A new instance of <see cref="IBlockTransformer"/> that can process input iteratively and produce a final <see cref="IHashValue"/> for that input.</returns>
        public abstract IBlockTransformer CreateBlockTransformer();

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
        /// <inheritdoc />
        public IHashValue ComputeHash(Stream data) => ComputeHash(data, CancellationToken.None);


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
        public IHashValue ComputeHash(Stream data, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!data.CanRead)
                throw new ArgumentException("Stream must be readable.", nameof(data));


            return ComputeHashInternal(data, cancellationToken);
        }


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
        public Task<IHashValue> ComputeHashAsync(Stream data) => ComputeHashAsync(data, CancellationToken.None);

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
        public Task<IHashValue> ComputeHashAsync(Stream data, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!data.CanRead)
                throw new ArgumentException("Stream must be readable.", nameof(data));


            return ComputeHashAsyncInternal(data, cancellationToken);
        }

        /// <summary>
        /// Computes hash value for given array segment.
        /// </summary>
        /// <param name="data">Array segment of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var memoryStream = new MemoryStream(data.Array, data.Offset, data.Count, false))
                return ComputeHashInternal(memoryStream, cancellationToken);
        }

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
        protected IHashValue ComputeHashInternal(Stream data, CancellationToken cancellationToken)
        {
            var blockTransformer = CreateBlockTransformer();
            var buffer = new byte[4096];

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var bytesRead = data.Read(buffer, 0, 4096);

                if (bytesRead == 0)
                    break;

                blockTransformer.TransformBytes(buffer, 0, bytesRead, cancellationToken);
            }

            return blockTransformer.FinalizeHashValue(cancellationToken);
        }

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
        protected async Task<IHashValue> ComputeHashAsyncInternal(Stream data, CancellationToken cancellationToken)
        {
            var blockTransformer = CreateBlockTransformer();
            var buffer = new byte[4096];

            while (true)
            {
                var bytesRead = await data.ReadAsync(buffer, 0, 4096, cancellationToken)
                    .ConfigureAwait(false);

                if (bytesRead == 0)
                    break;

                blockTransformer.TransformBytes(buffer, 0, bytesRead, cancellationToken);
            }

            return blockTransformer.FinalizeHashValue(cancellationToken);
        }


    }
}
