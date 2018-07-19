using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction
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
        /// <exception cref="ArgumentNullException">;<paramref name="data"/></exception>
        IHashValue ComputeHash(byte[] data);

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
        IHashValue ComputeHash(byte[] data, CancellationToken cancellationToken);


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
        IHashValue ComputeHash(Stream inputStream);

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
        IHashValue ComputeHash(Stream inputStream, CancellationToken cancellationToken);


        /// <summary>
        /// Computes hash value for given stream while outputting the read bytes to the second stream.
        /// </summary>
        /// <param name="inputStream">Stream of data to hash.</param>
        /// <param name="outputStream">Stream to write the read data to.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        /// <exception cref="ArgumentNullException">;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be readable.;<paramref name="inputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be writable.;<paramref name="outputStream"/></exception>
        /// <exception cref="ArgumentException">Stream must be seekable for this type of hash function.;<paramref name="inputStream"/></exception>
        IHashValue ComputeHash(Stream inputStream, Stream outputStream);

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
        IHashValue ComputeHash(Stream inputStream, Stream outputStream, CancellationToken cancellationToken);
    }
}
