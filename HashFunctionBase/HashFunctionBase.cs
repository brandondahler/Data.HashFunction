using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Abstract implementation of an IHashFunction.
    /// Provides convenience checks and ensures a default HashSize has been set at construction.
    /// </summary>
    public abstract class HashFunctionBase 
        : IHashFunction
    {
        /// <inheritdoc/>
        public virtual int HashSize 
        { 
            get { return _HashSize; }
            set
            {
                if (!ValidHashSizes.Contains(value))
                    throw new ArgumentOutOfRangeException("value");

                _HashSize = value;
            }
        }

        /// <summary>
        /// Flag to determine if a hash function needs a seekable stream in order to calculate the hash.
        /// 
        /// Override to true to make <see cref="ComputeHash(Stream)"/> pass a seekable stream to <see cref="ComputeHashInternal(Stream)"/>.
        /// </summary>
        protected virtual bool RequiresSeekableStream { get { return false; } }

        /// <inheritdoc/>
        public abstract IEnumerable<int> ValidHashSizes { get; }


        private int _HashSize;

        /// <summary>
        /// Constructs new <see cref="HashFunctionBase"/> instance.
        /// </summary>
        /// <param name="defaultHashSize">Default value for the <see cref="HashSize"/> property.</param>
        protected HashFunctionBase(int defaultHashSize)
        {
            _HashSize = defaultHashSize;
        }

        /// <inheritdoc/>
        public virtual byte[] ComputeHash(byte[] data)
        {
            return ComputeHash(new MemoryStream(data));
        }

        /// <inheritdoc/>
        public byte[] ComputeHash(Stream data)
        {
            if (!data.CanRead)
                throw new ArgumentException("Stream \"data\" must be readable.", "data");

            if (!RequiresSeekableStream || data.CanSeek)
                return ComputeHashInternal(data);

            using (var ms = new MemoryStream())
            {
                data.CopyTo(ms);

                ms.Seek(0, SeekOrigin.Begin);

                return ComputeHashInternal(ms);
            }
        }

        /// <inheritdoc/>
        protected abstract byte[] ComputeHashInternal(Stream data);
    }
}
