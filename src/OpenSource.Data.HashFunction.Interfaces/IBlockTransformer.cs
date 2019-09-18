using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction
{
    /// <summary>
    /// An internal state of an iteratively computable hash function value.
    /// </summary>
    public interface IBlockTransformer
    {
        /// <summary>
        /// Clones this transformer's internal state to a new, unassociated instance of this transformer.
        /// </summary>
        /// <returns>A new, unassociated instance of this transformer.</returns>
        IBlockTransformer Clone();


        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        void TransformBytes(byte[] data);

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease processing of the provided data.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        void TransformBytes(byte[] data, CancellationToken cancellationToken);


        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <param name="offset">The offset from which to begin using the data.</param>
        /// <param name="count">The number of bytes to use as data.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/>;Offset must be a value greater than or equal to zero and less than the length of the array minus one.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>;Count must be a value greater than zero and less than the the remaining length of the array after the offset value.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        void TransformBytes(byte[] data, int offset, int count);

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <param name="offset">The offset from which to begin using the data.</param>
        /// <param name="count">The number of bytes to use as data.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease processing of the provided data.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/>;Offset must be a value greater than or equal to zero and less than the length of the array minus one.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/>;Count must be a value greater than zero and less than the the remaining length of the array after the offset value.</exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        void TransformBytes(byte[] data, int offset, int count, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <exception cref="ArgumentException">data must be an ArraySegment of Count > 0.;<paramref name="data"/></exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        void TransformBytes(ArraySegment<byte> data);

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease processing of the provided data.</param>
        /// <exception cref="ArgumentException">data must be an ArraySegment of Count > 0.;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        void TransformBytes(ArraySegment<byte> data, CancellationToken cancellationToken);

        /// <summary>
        /// Completes any finalization processing and returns the resulting <see cref="IHashValue"/>.
        /// </summary>
        /// <remarks>Internal state will remain unmodified, therefore this method will not invalidate future calls to any TransformBytes or FinalizeHashValue.</remarks>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        IHashValue FinalizeHashValue();

        /// <summary>
        /// Completes any finalization processing and returns the resulting <see cref="IHashValue"/>.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease any final processing.</param>
        /// <remarks>Internal state will remain unmodified, therefore this method will not invalidate future calls to any other TransformBytes or FinalizeHashValue.</remarks>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        IHashValue FinalizeHashValue(CancellationToken cancellationToken);
    }
}
