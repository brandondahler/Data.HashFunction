using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Test.Mocks
{
    /// <summary>
    /// Forces underlying MemoryStream to report as being non-seekable.
    /// </summary>
    public class NonSeekableMemoryStream
        : MemoryStream
    {

        public virtual long Real_Length { get { return base.Length; } }


        public override bool CanSeek { get { return false; } }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", 
            Justification = "Mock purposefully throws to indicate a major issue.")]
        public override long Length
        {
            get { throw new NotImplementedException("Attempted to read length of a non-seekable stream."); }
        }



        public NonSeekableMemoryStream(byte[] buffer) : base(buffer) { }
        public NonSeekableMemoryStream(byte[] buffer, bool writable) : base(buffer, writable) { }
        public NonSeekableMemoryStream(byte[] buffer, int index, int count) : base(buffer, index, count) { }
        public NonSeekableMemoryStream(byte[] buffer, int index, int count, bool writable) : base(buffer, index, count, writable) { }
        public NonSeekableMemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible) : base(buffer, index, count, writable, publiclyVisible) { }


        
        public virtual long Real_Seek(long offset, SeekOrigin loc)
        {
            return base.Seek(offset, loc);
        }

        public virtual void Real_SetLength(long value)
        {
            base.SetLength(value);
        }



        public override long Seek(long offset, SeekOrigin loc)
        {
            throw new NotImplementedException("Attempted to seek an non-seekable stream.");
        }


        public override void SetLength(long value)
        {
            throw new NotImplementedException("Attempted to set length of a non-seekable stream.");
        }
    }
}
