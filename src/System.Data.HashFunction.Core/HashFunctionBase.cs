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
    /// Abstract implementation of an IHashFunction.
    /// Provides convenience checks and ensures a default HashSize has been set at construction.
    /// </summary>
    public abstract class HashFunctionBase 
        : IHashFunction
    {

        /// <inheritdoc />
        public int HashSize { get; }

        /// <summary>
        /// Flag to determine if a hash function needs a seekable stream in order to calculate the hash.
        /// Override to true to make <see cref="ComputeHash(Stream)" /> pass a seekable stream to <see cref="ComputeHashInternal(IUnifiedData, CancellationToken)" />.
        /// </summary>
        /// <value>
        /// <c>true</c> if a seekable stream; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool RequiresSeekableStream { get; } = false;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="HashFunctionBase"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashSize" /></param>
        protected HashFunctionBase(int hashSize)
        {
           HashSize = hashSize;
        }


        /// <exception cref="ArgumentNullException">;<paramref name="data"/></exception>
        /// <inheritdoc />
        public IHashValue ComputeHash(byte[] data) => ComputeHash(data, CancellationToken.None);

        /// <exception cref="ArgumentNullException">;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <inheritdoc />
        public IHashValue ComputeHash(byte[] data, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            cancellationToken.ThrowIfCancellationRequested();


            return new HashValue(
                ComputeHashInternal(new ArrayData(data), cancellationToken),
                HashSize);
        }


        /// <exception cref="ArgumentNullException">;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="data"/></exception>
        /// <inheritdoc />
        public IHashValue ComputeHash(Stream data) => ComputeHash(data, CancellationToken.None);


        /// <exception cref="ArgumentNullException">;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="data"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <inheritdoc />
        public IHashValue ComputeHash(Stream data, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!data.CanRead)
                throw new ArgumentException("Stream must be readable.", nameof(data));

            if (!data.CanSeek && RequiresSeekableStream)
                throw new ArgumentException("Stream must be seekable for this type of hash function.", nameof(data));

            cancellationToken.ThrowIfCancellationRequested();


            return new HashValue(
                ComputeHashInternal(
                    new StreamData(data),
                    cancellationToken),
                HashSize);
        }


        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="data">Data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of data.
        /// </returns>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        protected abstract byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken);

    }
}
