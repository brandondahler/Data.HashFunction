using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Core
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
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        public IHashValue ComputeHash(byte[] data) => 
            ComputeHash(data, CancellationToken.None);


        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public IHashValue ComputeHash(byte[] data, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            return ComputeHash(data, 0, data.Length, cancellationToken);
        }


        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <param name="offset">The offset from which to begin using the data.</param>
        /// <param name="count">The number of bytes to use as data.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/>;Offset must be a value greater than or equal to zero and less than or equal to the length of the array.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>;Count must be a value greater than zero and less than the the remaining length of the array after the offset value.</exception>
        public IHashValue ComputeHash(byte[] data, int offset, int count) =>
            ComputeHash(data, offset, count, CancellationToken.None);

        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <param name="offset">The offset from which to begin using the data.</param>
        /// <param name="count">The number of bytes to use as data.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/>;Offset must be a value greater than or equal to zero and less than or equal to the length of the array.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>;Count must be a value greater than or equal to zero and less than the the remaining length of the array after the offset value.</exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public IHashValue ComputeHash(byte[] data, int offset, int count, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (offset < 0 || offset > data.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be a value greater than or equal to zero and less than or equal to the length of the array.");

            if (count < 0 || count > data.Length - offset)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be a value greater than or equal to zero and less than the the remaining length of the array after the offset value.");

            return ComputeHash(new ArraySegment<byte>(data, offset, count), cancellationToken);
        }

        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        public IHashValue ComputeHash(ArraySegment<byte> data) =>
            ComputeHash(data, CancellationToken.None);

        /// <summary>
        /// Computes hash value for given array segment.
        /// </summary>
        /// <param name="data">Array segment of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public IHashValue ComputeHash(ArraySegment<byte> data, CancellationToken cancellationToken) =>
            ComputeHashInternal(data, cancellationToken);

        /// <summary>
        /// Computes hash value for given array segment.
        /// </summary>
        /// <param name="data">Array segment of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        protected abstract IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken);

    }
}
