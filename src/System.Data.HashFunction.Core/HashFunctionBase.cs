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
    /// Abstract implementation of an <see cref="IHashFunction"/>.
    /// Provides convenience checks and ensures a default HashSize has been set at construction.
    /// </summary>
    public abstract class HashFunctionBase 
        : IHashFunction
    {

        /// <summary>
        /// Size of produced hash, in bits.
        /// </summary>
        /// <value>
        /// The size of the hash, in bits.
        /// </value>
        public abstract int HashSizeInBits { get; }


        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException">;<paramref name="data"/></exception>
        public IHashValue ComputeHash(byte[] data) => ComputeHash(data, CancellationToken.None);


        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException">;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public IHashValue ComputeHash(byte[] data, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            cancellationToken.ThrowIfCancellationRequested();


            return new HashValue(
                ComputeHashInternal(new ArrayData(data), cancellationToken),
                HashSizeInBits);
        }


        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        /// <inheritdoc />
        public IHashValue ComputeHash(Stream inputStream) => ComputeHash(inputStream, null, CancellationToken.None);


        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public IHashValue ComputeHash(Stream inputStream, CancellationToken cancellationToken) => ComputeHash(inputStream, null, cancellationToken);


        /// <summary>
        /// Computes hash value for given stream while outputting the read bytes to the second stream.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <param name="outputStream">Stream to write the read data to.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentNullException">;<paramref name="outputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be writable.;<paramref name="outputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        public IHashValue ComputeHash(Stream inputStream, Stream outputStream) => ComputeHash(inputStream, outputStream, CancellationToken.None);

        /// <summary>
        /// Computes hash value for given stream while outputting the read bytes to the second stream.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <param name="outputStream">Stream to write the read data to.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be writable.;<paramref name="outputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public IHashValue ComputeHash(Stream inputStream, Stream outputStream, CancellationToken cancellationToken)
        {
            if (inputStream == null)
                throw new ArgumentNullException(nameof(inputStream));

            if (!inputStream.CanRead)
                throw new ArgumentException("Stream must be readable.", nameof(inputStream));

            if (outputStream != null && !outputStream.CanWrite)
                throw new ArgumentException("Stream must be writable.", nameof(outputStream));



            cancellationToken.ThrowIfCancellationRequested();


            return new HashValue(
                ComputeHashInternal(
                    new StreamData(inputStream, outputStream),
                    cancellationToken),
                HashSizeInBits);
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
