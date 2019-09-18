using OpenSource.Data.HashFunction.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Core
{
    /// <summary>
    /// Base implementation for an internal state of an iteratively computable hash function value.
    /// 
    /// Provides buffering and cancellation handling features.
    /// </summary>
    /// <typeparam name="TSelf">The final derived type to use when cloning.</typeparam>
    public abstract partial class BlockTransformerBase<TSelf>
        : IBlockTransformer
        where TSelf : BlockTransformerBase<TSelf>, new()
    {
        /// <summary>
        /// The default number of bytes to process before re-checking the cancellation token.
        /// </summary>
        protected const int DefaultCancellationBatchSize = 4096;

        /// <summary>
        /// The default block size to pass to <see cref="TransformByteGroupsInternal(ArraySegment{byte})"/>.
        /// </summary>
        protected const int DefaultInputBlockSize = 1;

        /// <summary>
        /// The input buffer that should be fetched during the <see cref="FinalizeHashValueInternal(CancellationToken)" /> methods.
        /// </summary>
        protected byte[] FinalizeInputBuffer { get => _inputBuffer; }

        private readonly int _cancellationBatchSize;
        private readonly int _inputBlockSize;

        private byte[] _inputBuffer = null;
        private bool _isCorrupted = false;
        

        /// <summary>
        /// Construct <see cref="BlockTransformerBase{TSelf}"/> with optional parameters to configure features.
        /// </summary>
        /// <param name="cancellationBatchSize">Maximum number of bytes to process before re-checking the cancellation token.</param>
        /// <param name="inputBlockSize">Block size to pass to <see cref="TransformByteGroupsInternal(ArraySegment{byte})"/>.</param>
        protected BlockTransformerBase(int cancellationBatchSize = DefaultCancellationBatchSize, int inputBlockSize = DefaultInputBlockSize)
        {
            _cancellationBatchSize = cancellationBatchSize;
            _inputBlockSize = inputBlockSize;

            // Ensure _cancellationBatchSize is a multiple of _inputBlockSize, preferrably rounding down.
            {
                var blockSizeRemainder = _cancellationBatchSize % _inputBlockSize;

                if (blockSizeRemainder > 0)
                {
                    _cancellationBatchSize -= blockSizeRemainder;

                    if (_cancellationBatchSize <= 0)
                        _cancellationBatchSize += _inputBlockSize;
                }
            }
        }

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        public void TransformBytes(byte[] data) => TransformBytes(data, CancellationToken.None);

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease processing of the provided data.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        public void TransformBytes(byte[] data, CancellationToken cancellationToken)
        {
            ThrowIfCorrupted();

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            TransformBytes(data, 0, data.Length, CancellationToken.None);
        }

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
        public void TransformBytes(byte[] data, int offset, int count) =>
            TransformBytes(data, 0, data.Length, CancellationToken.None);

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
        public void TransformBytes(byte[] data, int offset, int count, CancellationToken cancellationToken)
        {
            ThrowIfCorrupted();

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Length == 0)
                throw new ArgumentException("data.Length must be greater than 0.", nameof(data));

            if (offset < 0 || offset >= data.Length - 1)
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be a value greater than or equal to zero and less than the length of the array minus one.");

            if (count <= 0 || count > data.Length - offset)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be a value greater than zero and less than the the remaining length of the array after the offset value.");


            TransformBytes(new ArraySegment<byte>(data, offset, count), cancellationToken);
        }

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <exception cref="ArgumentException">data must be an ArraySegment of Count > 0.;<paramref name="data"/></exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        public void TransformBytes(ArraySegment<byte> data) =>
            TransformBytes(data, CancellationToken.None);

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease processing of the provided data.</param>
        /// <exception cref="ArgumentException">data must be an ArraySegment of Count > 0.;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        public void TransformBytes(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            if (data.Count == 0)
                throw new ArgumentException("data must be an ArraySegment of Count > 0.", nameof(data));

            TransformBytesInternal(data, cancellationToken);
        }


        /// <summary>
        /// Completes any finalization processing and returns the resulting <see cref="IHashValue"/>.
        /// </summary>
        /// <remarks>Internal state will remain unmodified, therefore this method will not invalidate future calls to any other TransformBytes or FinalizeTransformation calls.</remarks>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        public IHashValue FinalizeHashValue() =>
            FinalizeHashValue(CancellationToken.None);

        /// <summary>
        /// Completes any finalization processing and returns the resulting <see cref="IHashValue"/>.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease any final processing.</param>
        /// <remarks>Internal state will remain unmodified, therefore this method will not invalidate future calls to any other TransformBytes or FinalizeTransformation calls.</remarks>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        public IHashValue FinalizeHashValue(CancellationToken cancellationToken)
        {
            ThrowIfCorrupted();

            return FinalizeHashValueInternal(cancellationToken);
        }


        /// <summary>
        /// Clones this transformer's internal state to a new, unassociated instance of this transformer.
        /// </summary>
        /// <returns>A new, unassociated instance of this transformer.</returns>
        public IBlockTransformer Clone()
        {
            ThrowIfCorrupted();


            var clone = new TSelf();

            CopyStateTo(clone);

            return clone;
        }

        /// <summary>
        /// Copies the internal state of the current instance to the provided instance.
        /// </summary>
        /// <param name="other">The instance to copy the internal state into.</param>
        /// <remarks>All overriders should ensure base.CopyStateTo(other) is called.</remarks>
        protected virtual void CopyStateTo(TSelf other)
        {
            other._inputBuffer = _inputBuffer;
        }


        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if this transformer is marked as being corrupted.
        /// </summary>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        protected void ThrowIfCorrupted()
        {
            if (_isCorrupted)
                throw new InvalidOperationException("A previous transformation cancellation has resulted in an undefined internal state.");
        }

        /// <summary>
        /// Marks this instance as corrupted and will prevent all calls on this instance from completing successfully.
        /// </summary>
        protected void MarkSelfCorrupted()
        {
            _isCorrupted = true;
        }

        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease processing of the provided data.</param>
        protected void TransformBytesInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataArray = data.Array;
            var dataCurrentOffset = data.Offset;
            var dataRemainingCount = data.Count;

            var totalBytesToProcess = (_inputBuffer?.Length).GetValueOrDefault() + dataRemainingCount;
            var processingRemainder = totalBytesToProcess % _inputBlockSize;

            // Determine how many bytes will inevitably be rolled over into the next input buffer and prepare that buffer.
            byte[] nextInputBuffer = null;
            {
                if (processingRemainder > 0)
                {
                    nextInputBuffer = new byte[processingRemainder];

                    if (dataRemainingCount >= processingRemainder)
                    {
                        // All of the data can be fetched from newest data
                        var copyFromOffset = dataCurrentOffset + dataRemainingCount - processingRemainder;

                        Array.Copy(dataArray, copyFromOffset, nextInputBuffer, 0, processingRemainder);

                        dataRemainingCount -= processingRemainder;

                    }
                    else
                    {
                        var bytesRolledOver = 0;

                        if (_inputBuffer != null)
                        {
                            Array.Copy(_inputBuffer, nextInputBuffer, _inputBuffer.Length);

                            bytesRolledOver = _inputBuffer.Length;
                        }

                        Array.Copy(dataArray, 0, nextInputBuffer, bytesRolledOver, processingRemainder - bytesRolledOver);

                        dataRemainingCount = 0;
                    }
                }
            }

            // Process the full groups available
            if (dataRemainingCount > 0)
            {
                try
                {
                    if (_inputBuffer != null)
                    {
                        var overrideInputBuffer = new byte[_inputBlockSize];
                        var dataBytesToRead = overrideInputBuffer.Length - _inputBuffer.Length;

                        Array.Copy(_inputBuffer, overrideInputBuffer, _inputBuffer.Length);
                        Array.Copy(dataArray, dataCurrentOffset, overrideInputBuffer, _inputBuffer.Length, dataBytesToRead);

                        dataCurrentOffset += dataBytesToRead;
                        dataRemainingCount -= dataBytesToRead;


                        TransformByteGroupsInternal(new ArraySegment<byte>(overrideInputBuffer, 0, overrideInputBuffer.Length));
                    }

                    while (dataRemainingCount > 0)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var bytesToProcess = Math.Min(dataRemainingCount, _cancellationBatchSize);

                        TransformByteGroupsInternal(new ArraySegment<byte>(data.Array, dataCurrentOffset, bytesToProcess));

                        dataCurrentOffset += bytesToProcess;
                        dataRemainingCount -= bytesToProcess;
                    }

                    Debug.Assert(dataRemainingCount == 0);
                }
                catch (TaskCanceledException)
                {
                    MarkSelfCorrupted();

                    throw;
                }
            }

            // Update input buffer
            _inputBuffer = nextInputBuffer;
        }


        /// <summary>
        /// Updates the internal state of this transformer with the given data.
        /// 
        /// The data's size will be a multiple of the provided inputBlockSize in the constructor.
        /// </summary>
        /// <param name="data">The data to process into this transformer's internal state.</param>
        protected abstract void TransformByteGroupsInternal(ArraySegment<byte> data);

        /// <summary>
        /// Completes any finalization processing and returns the resulting <see cref="IHashValue"/>.
        /// </summary>
        /// <remarks>Internal state will remain unmodified, therefore this method will not invalidate future calls to any other TransformBytes or FinalizeTransformation calls.</remarks>
        /// <exception cref="InvalidOperationException">A previous transformation cancellation has resulted in an undefined internal state.</exception>
        protected abstract IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken);


    }
}
