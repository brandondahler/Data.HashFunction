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

        /// <inheritdoc />
        public virtual int HashSize 
        { 
            get { return _HashSize; }
        }

        /// <summary>
        /// Flag to determine if a hash function needs a seekable stream in order to calculate the hash.
        /// Override to true to make <see cref="ComputeHash(Stream)" /> pass a seekable stream to <see cref="ComputeHashInternal(Stream)" />.
        /// </summary>
        /// <value>
        /// <c>true</c> if a seekable stream; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool RequiresSeekableStream { get { return false; } }


        private readonly int _HashSize;


        /// <summary>
        /// Initializes a new instance of the <see cref="HashFunctionBase"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashSize" /></param>
        protected HashFunctionBase(int hashSize)
        {
            _HashSize = hashSize;
        }


        /// <inheritdoc />
        public virtual byte[] ComputeHash(byte[] data)
        {
            return ComputeHash(new MemoryStream(data));
        }

        /// <exception cref="System.ArgumentException">Stream \data\ must be readable.;data</exception>
        /// <inheritdoc />
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

        /// <inheritdoc cref="ComputeHash(Stream)" />
        protected abstract byte[] ComputeHashInternal(Stream data);
    }
}
