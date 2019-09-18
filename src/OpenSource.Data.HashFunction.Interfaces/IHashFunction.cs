using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction
{
    /// <summary>
    /// Common interface to non-cryptographic hash functions.
    /// </summary>
    public interface IHashFunction
    {
        /// <summary>
        /// Size of produced hash, in bits.
        /// </summary>
        /// <value>
        /// The size of the hash, in bits.
        /// </value>
        int HashSizeInBits { get; }


        /// <summary>
        /// Computes hash value for given byte array.
        /// </summary>
        /// <param name="data">Array of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        IHashValue ComputeHash(byte[] data);

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
        IHashValue ComputeHash(byte[] data, CancellationToken cancellationToken);

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
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>;Count must be a value greater than or equal to zero and less than the the remaining length of the array after the offset value.</exception>
        IHashValue ComputeHash(byte[] data, int offset, int count);

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
        IHashValue ComputeHash(byte[] data, int offset, int count, CancellationToken cancellationToken);

        /// <summary>
        /// Computes hash value for given array segment.
        /// </summary>
        /// <param name="data">Array segment of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        IHashValue ComputeHash(ArraySegment<byte> data);

        /// <summary>
        /// Computes hash value for given array segment.
        /// </summary>
        /// <param name="data">Array segment of data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        IHashValue ComputeHash(ArraySegment<byte> data, CancellationToken cancellationToken);

    }
}
