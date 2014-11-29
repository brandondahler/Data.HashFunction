using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities.UnifiedData
{
    internal class StreamData
        : UnifiedData, IDisposable
    {
        /// <inheritdoc />
        public override long Length { get { return _Data.Length; } }

        protected readonly Stream _Data;



        /// <summary>
        /// Initializes a new instance of the <see cref="StreamData"/> class.
        /// </summary>
        /// <param name="data">The stream to represent.</param>
        public StreamData(Stream data)
        {
            _Data = data;
        }

        /// <summary>
        /// Disposes underlying stream.
        /// </summary>
        public void Dispose()
        {
            _Data.Dispose();
        }


        /// <inheritdoc />
        public override void ForEachRead(Action<byte[], int, int> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");


            var buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = _Data.Read(buffer, 0, buffer.Length)) > 0)
                action(buffer, 0, bytesRead);
        }

#if NET45
        /// <inheritdoc />
        public override async Task ForEachReadAsync(Action<byte[], int, int> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");


            var buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = await _Data.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                action(buffer, 0, bytesRead);
        }
#endif


        /// <inheritdoc />
        public override void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "groupSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");


            // Store bufferSize to keep it from changing under us
            var bufferSize = BufferSize;


            byte[] buffer = new byte[groupSize < bufferSize ? bufferSize : groupSize];
            int position = 0;
            int currentLength;
            

            while ((currentLength = _Data.Read(buffer, position, buffer.Length - position)) > 0)
            {
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
            }


            if (remainderAction != null && position > 0)
                remainderAction(buffer, 0, position);
        }

#if NET45
        /// <inheritdoc />
        public override async Task ForEachGroupAsync(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "groupSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");


            // Store bufferSize to keep it from changing under us
            var bufferSize = BufferSize;


            byte[] buffer = new byte[groupSize < bufferSize ? bufferSize : groupSize];
            int position = 0;
            int currentLength;


            while ((currentLength = await _Data.ReadAsync(buffer, position, buffer.Length - position).ConfigureAwait(false)) > 0)
            {
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
            }


            if (remainderAction != null && position > 0)
                remainderAction(buffer, 0, position);
        }
#endif


        /// <inheritdoc />
        public override byte[] ToArray()
        {
            using (var ms = new MemoryStream())
            {
                _Data.CopyTo(ms);

                return ms.ToArray();
            }
        }

#if NET45
        /// <inheritdoc />
        public override async Task<byte[]> ToArrayAsync()
        {
            using (var ms = new MemoryStream())
            {
                await _Data.CopyToAsync(ms)
                    .ConfigureAwait(false);

                return ms.ToArray();
            }
        }
#endif
    }
}
