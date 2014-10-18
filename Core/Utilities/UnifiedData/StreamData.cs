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
        public override void ForEachRead(Action<byte[]> action, int bufferSize)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize", "bufferSize must be greater than 0.");


            var buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = _Data.Read(buffer, 0, bufferSize)) > 0)
            {
                if (bytesRead == bufferSize)
                {
                    action(buffer);
                } else {
                    var partialBuffer = new byte[bytesRead];
                    Array.Copy(buffer, partialBuffer, bytesRead);

                    action(partialBuffer);
                }
            }
        }

        /// <inheritdoc />
        public override async Task ForEachReadAsync(Action<byte[]> action, int bufferSize)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize", "bufferSize must be greater than 0.");


            var buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = await _Data.ReadAsync(buffer, 0, bufferSize).ConfigureAwait(false)) > 0)
            {
                if (bytesRead == bufferSize)
                {
                    action(buffer);
                } else {
                    var partialBuffer = new byte[bytesRead];
                    Array.Copy(buffer, partialBuffer, bytesRead);

                    action(partialBuffer);
                }
            }

        }


        /// <inheritdoc />
        public override void ForEachGroup(int groupSize, Action<byte[]> action, Action<byte[]> remainderAction)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "bufferSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");


            byte[] group;

            while ((group = _Data.ReadBytes(groupSize)).Length == groupSize)
                action(group);


            if (remainderAction != null && group.Length > 0)
                remainderAction(group);
        }

        /// <inheritdoc />
        public override async Task ForEachGroupAsync(int groupSize, Action<byte[]> action, Action<byte[]> remainderAction)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "bufferSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");


            byte[] group;

            while ((group = await _Data.ReadBytesAsync(groupSize).ConfigureAwait(false)).Length == groupSize)
                action(group);


            if (remainderAction != null && group.Length > 0)
                remainderAction(group);
        }


        /// <inheritdoc />
        public override byte[] ToArray()
        {
            using (var ms = new MemoryStream())
            {
                _Data.CopyTo(ms);

                return ms.ToArray();
            }
        }

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
    }
}
