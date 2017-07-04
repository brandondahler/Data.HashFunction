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
        : UnifiedDataAsync, 
            IDisposable
    {
        /// <inheritdoc />
        public override long Length { get { return _data.Length; } }


        private readonly Stream _data;

        private bool _disposed = false;


        /// <summary>
        /// Initializes a new instance of the <see cref="StreamData"/> class.
        /// </summary>
        /// <param name="data">The stream to represent.</param>
        public StreamData(Stream data)
        {
            _data = data;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="StreamData"/> class.
        /// </summary>
        ~StreamData()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Disposes underlying stream.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _data.Dispose();
            }
        }


        /// <inheritdoc />
        public override void ForEachRead(Action<byte[], int, int> action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            cancellationToken.ThrowIfCancellationRequested();


            var buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = _data.Read(buffer, 0, buffer.Length)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                action(buffer, 0, bytesRead);
            }
        }

        /// <inheritdoc />
        public override async Task ForEachReadAsync(Action<byte[], int, int> action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            cancellationToken.ThrowIfCancellationRequested();

            
            var buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = await _data.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                action(buffer, 0, bytesRead);
            }
        }


        /// <inheritdoc />
        public override void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "groupSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");

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

        /// <inheritdoc />
        public override async Task ForEachGroupAsync(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "groupSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");
            
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


        /// <inheritdoc />
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

        /// <inheritdoc />
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
