using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core.Utilities.UnifiedData
{
    internal sealed class StreamData
        : UnifiedDataAsyncBase, 
            IDisposable
    {
        /// <summary>
        /// Length of data provided.
        /// </summary>
        /// <remarks>
        /// Implementors are allowed throw an exception if it is not possible to resolve the length of the data.
        /// </remarks>
        /// <exception cref="NotSupportedException" />
        public override long Length { get { return _data.Length; } }


        private readonly Stream _data;

        private bool _disposed = false;


        /// <summary>
        /// Initializes a new instance of the <see cref="StreamData"/> class.
        /// </summary>
        /// <param name="data">The stream to represent.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        public StreamData(Stream data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }


        /// <summary>
        /// Disposes underlying stream.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _data.Dispose();
            }
        }


        /// <summary>
        /// Executes an action each time a chunk is read.
        /// </summary>
        /// <param name="action">Function to execute.</param>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        public override void ForEachRead(Action<byte[], int, int> action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            cancellationToken.ThrowIfCancellationRequested();


            var buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = _data.Read(buffer, 0, buffer.Length)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                action(buffer, 0, bytesRead);
            }
        }

        /// <summary>
        /// Executes an action each time a chunk is read.
        /// </summary>
        /// <param name="action">Function to execute.</param>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">action</exception>
        public override async Task ForEachReadAsync(Action<byte[], int, int> action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            cancellationToken.ThrowIfCancellationRequested();

            
            var buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = await _data.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                action(buffer, 0, bytesRead);
            }
        }


        /// <summary>
        /// Executes an action one or more times, providing the data read as an array whose length is a multiple of groupSize.  
        /// Optionally runs an action on the final remainder group.
        /// </summary>
        /// <param name="groupSize">Length of the groups passed to the action.</param>
        /// <param name="action">Action to execute for each full group read.</param>
        /// <param name="remainderAction">Action to execute if the final group is less than groupSize.  Null values are allowed.</param>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <remarks>remainderAction will not be run if the length of the data is a multiple of groupSize.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">groupSize;groupSize must be greater than 0.</exception>
        /// <exception cref="ArgumentNullException">action</exception>
        public override void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(groupSize), $"{nameof(groupSize)} must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            cancellationToken.ThrowIfCancellationRequested();


            // Store bufferSize to keep it from changing under us
            var bufferSize = BufferSize;


            byte[] buffer = new byte[groupSize < bufferSize ? bufferSize : groupSize];
            int position = 0;
            int currentLength;

            
            while ((currentLength = _data.Read(buffer, position, buffer.Length - position)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();


                position += currentLength;

                // If we can fulfill a group
                if (position >= groupSize)
                {
                    var extraBytesLength = position % groupSize;


                    // Fulfill the group
                    action(buffer, 0, position - extraBytesLength);


                    // Move extra bytes to beginning of array or reset position
                    if (extraBytesLength > 0)
                    {
                        Array.Copy(buffer, position - extraBytesLength, buffer, 0, extraBytesLength);
                        position %= groupSize;

                    } else {
                        position = 0;
                    }
                }


                cancellationToken.ThrowIfCancellationRequested();
            }


            if (remainderAction != null && position > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                remainderAction(buffer, 0, position);
            }
        }


        /// <summary>
        /// Executes an action one or more times, providing the data read as an array whose length is a multiple of groupSize.  
        /// Optionally runs an action on the final remainder group.
        /// </summary>
        /// <param name="groupSize">Length of the groups passed to the action.</param>
        /// <param name="action">Action to execute for each full group read.</param>
        /// <param name="remainderAction">Action to execute if the final group is less than groupSize.  Null values are allowed.</param>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <remarks>remainderAction will not be run if the length of the data is a multiple of groupSize.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">groupSize;groupSize must be greater than 0.</exception>
        /// <exception cref="ArgumentNullException">action</exception>
        public override async Task ForEachGroupAsync(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(groupSize), $"{nameof(groupSize)} must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException(nameof(action));
            
            cancellationToken.ThrowIfCancellationRequested();


            // Store bufferSize to keep it from changing under us
            var bufferSize = BufferSize;


            byte[] buffer = new byte[groupSize < bufferSize ? bufferSize : groupSize];
            int position = 0;
            int currentLength;


            while ((currentLength = await _data.ReadAsync(buffer, position, buffer.Length - position, cancellationToken).ConfigureAwait(false)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();


                position += currentLength;

                // If we can fulfill a group
                if (position >= groupSize)
                {
                    var extraBytesLength = position % groupSize;


                    // Fulfill the group
                    action(buffer, 0, position - extraBytesLength);


                    // Move extra bytes to beginning of array or reset position
                    if (extraBytesLength > 0)
                    {
                        Array.Copy(buffer, position - extraBytesLength, buffer, 0, extraBytesLength);
                        position %= groupSize;

                    } else {
                        position = 0;
                    }
                }


                cancellationToken.ThrowIfCancellationRequested();
            }


            if (remainderAction != null && position > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                remainderAction(buffer, 0, position);
            }
        }



        /// <summary>
        /// Reads all data and converts it to an in-memory array.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <returns>Array of bytes read from the data provider.</returns>
        public override byte[] ToArray(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var ms = new MemoryStream())
            {
                var buffer = new byte[BufferSize];
                int bytesRead;

                while ((bytesRead = _data.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    ms.Write(buffer, 0, bytesRead);

                    cancellationToken.ThrowIfCancellationRequested();
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Reads all data and converts it to an in-memory array.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <returns>Array of bytes read from the data provider.</returns>
        public override async Task<byte[]> ToArrayAsync(CancellationToken cancellationToken)
        {
            using (var ms = new MemoryStream())
            {
                await _data.CopyToAsync(ms, BufferSize, cancellationToken)
                    .ConfigureAwait(false);

                return ms.ToArray();
            }
        }
    }
}
