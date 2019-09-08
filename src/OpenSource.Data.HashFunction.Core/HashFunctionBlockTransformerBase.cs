using OpenSource.Data.HashFunction.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Core
{
    public abstract partial class HashFunctionBlockTransformerBase<TSelf>
        : IHashFunctionBlockTransformer
        where TSelf : HashFunctionBlockTransformerBase<TSelf>, new()
    {
        protected const int _defaultCancellationBatchSize = 4096;
        protected const int _defaultInputBlockSize = 1;

        protected byte[] FinalizeInputBuffer { get => _inputBuffer; }

        private readonly int _cancellationBatchSize;
        private readonly int _inputBlockSize;

        private byte[] _inputBuffer = null;
        private bool _isCorrupted = false;
        

        protected HashFunctionBlockTransformerBase(int cancellationBatchSize = _defaultCancellationBatchSize, int inputBlockSize = _defaultInputBlockSize)
        {
            _cancellationBatchSize = cancellationBatchSize;
            _inputBlockSize = inputBlockSize;
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
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cease processing of the provided data.</param>
        /// <exception cref="ArgumentException">data must be an ArraySegment of Count > 0.;<paramref name="data"/></exception>
        /// <exception cref="TaskCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
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
        public IHashFunctionBlockTransformer Clone()
        {
            ThrowIfCorrupted();


            var clone = new TSelf();

            CopyStateTo(clone);

            return clone;
        }


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

        protected virtual void TransformBytesInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
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


        protected abstract void TransformByteGroupsInternal(ArraySegment<byte> data);
        protected abstract IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken);


    }
}
