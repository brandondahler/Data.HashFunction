using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Test._Mocks
{
    /// <summary>
    /// Forces async functions to actually run asynchronously by delaying the read.
    /// </summary>
    public class SlowAsyncStream
        : Stream
    {
        public override bool CanRead { get { return _underlyingStream.CanRead; } }
        public override bool CanSeek { get { return _underlyingStream.CanSeek; } }
        public override bool CanTimeout { get { return _underlyingStream.CanTimeout; } }
        public override bool CanWrite { get { return _underlyingStream.CanTimeout; } }

        public override long Length { get { return _underlyingStream.Length; } }

        public override long Position
        {
            get { return _underlyingStream.Position; }
            set { _underlyingStream.Position = value; }
        }

        public override int ReadTimeout
        {
            get { return _underlyingStream.ReadTimeout; }
            set { _underlyingStream.ReadTimeout = value; }
        }

        public override int WriteTimeout
        {
            get { return _underlyingStream.WriteTimeout; }
            set { _underlyingStream.WriteTimeout = value; }
        }


        private readonly Stream _underlyingStream;

        public SlowAsyncStream(Stream underlyingStream) 
        {
            _underlyingStream = underlyingStream;
        }

        public async override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            await Task.Yield();

            await _underlyingStream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _underlyingStream.Dispose();
        }

        public override void Flush()
        {
            _underlyingStream.Flush();
        }

        public async override Task FlushAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();

            await _underlyingStream.FlushAsync(cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _underlyingStream.Read(buffer, offset, count);
        }

        public async override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Yield();

            return await _underlyingStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            return _underlyingStream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _underlyingStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _underlyingStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _underlyingStream.Write(buffer, offset, count);
        }

        public async override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Yield();

            await _underlyingStream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            _underlyingStream.WriteByte(value);
        }
    }
}
