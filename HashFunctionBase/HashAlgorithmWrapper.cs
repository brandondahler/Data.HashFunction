using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of <see cref="IHashFunction"/> that wraps cryptographic hash functions known as <see cref="HashAlgorithm" />.
    /// </summary>
    public class HashAlgorithmWrapper
        : IHashFunction, IDisposable
    {
        /// <inheritdoc/>
        public virtual int HashSize
        {
            get { return _hashAlgorithm.HashSize; }
            set { if (value != _hashAlgorithm.HashSize) throw new ArgumentOutOfRangeException("value"); }
        }

        /// <inheritdoc/>
        public IEnumerable<int> ValidHashSizes { get { return new[] { _hashAlgorithm.HashSize }; } }



        private readonly HashAlgorithm _hashAlgorithm;
        private readonly bool _ownsInstance;

        private readonly object SyncRoot = new object();

        private bool _disposed = false;

        /// <summary>
        /// Constructs new <see cref="HashAlgorithmWrapper"/> instance.
        /// </summary>
        /// <param name="hashAlgorithm">Instance of <see cref="HashAlgorithm"/> to use for hashing.</param>
        /// <param name="ownsInstance">
        /// If true, the instance of <see cref="HashAlgorithm"/> passed will be disposed when the HashAlgorithmWrapper instance is disposed.
        /// </param>
        public HashAlgorithmWrapper(HashAlgorithm hashAlgorithm, bool ownsInstance = true)
        {
            _hashAlgorithm = hashAlgorithm;
            _ownsInstance = ownsInstance;
        }

        /// <summary>
        /// Disposes instance, specifying that this is not a direct dispose call.
        /// </summary>
        ~HashAlgorithmWrapper()
        {
            Dispose(false);
        }


        /// <summary>
        /// Disposes <see cref="HashAlgorithm"/> passed in constructor if it is owned by this <see cref="HashAlgorithmWrapper"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }



        /// <summary>
        /// Disposes <see cref="HashAlgorithm"/> passed in constructor if it is owned by this <see cref="HashAlgorithmWrapper"/>.
        /// </summary>
        /// <param name="disposing">If false, call is assumed to be from the destructor.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (SyncRoot)
            {
                if (!_disposed && _ownsInstance)
                    _hashAlgorithm.Dispose();

                _disposed = true;
            }
        }


        /// <inheritdoc/>
        public byte[] ComputeHash(byte[] data)
        {
            if (HashSize != _hashAlgorithm.HashSize)
                throw new ArgumentOutOfRangeException("HashSize");

            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(data.ToArray());
        }

        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="inputStream">Readable stream of data to hash.</param>
        /// <returns>Hash value of the data that was read from the stream.</returns>
        public byte[] ComputeHash(Stream inputStream)
        {
            if (HashSize != _hashAlgorithm.HashSize)
                throw new ArgumentOutOfRangeException("HashSize");

            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(inputStream);
        }
    }


    /// <summary>
    /// Generic implementation of <see cref="IHashFunction"/> that wraps cryptographic hash functions known as <see cref="HashAlgorithm" />s.
    /// </summary>
    public class HashAlgorithmWrapper<HashAlgorithmT>
        : IHashFunction, IDisposable
        where HashAlgorithmT : HashAlgorithm, new()
    {
        /// <inheritdoc/>
        public virtual int HashSize
        {
            get { return _hashAlgorithm.HashSize; }
            set { if (value != _hashAlgorithm.HashSize) throw new ArgumentOutOfRangeException("value"); }
        }

        /// <inheritdoc/>
        public IEnumerable<int> ValidHashSizes { get { return new[] { _hashAlgorithm.HashSize }; } }


        private readonly HashAlgorithmT _hashAlgorithm;
        private readonly object SyncRoot = new object();
        
        
        private bool _disposed = false;


        /// <summary>
        /// Constructs new <see cref="HashAlgorithmWrapper{HashAlgorithmT}"/> instance.
        /// </summary>
        public HashAlgorithmWrapper()
        {
            _hashAlgorithm = new HashAlgorithmT();
        }

        /// <summary>
        /// Disposes instance, specifying that this is not a direct dispose call.
        /// </summary>
        ~HashAlgorithmWrapper()
        {
            Dispose(false);
        }


        /// <summary>
        /// Disposes <see cref="HashAlgorithm"/> created in constructor.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes <see cref="HashAlgorithm"/> created in constructor.
        /// </summary>
        /// <param name="disposing">If false, call is assumed to be from the destructor.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (SyncRoot)
            {
                if (!_disposed)
                    _hashAlgorithm.Dispose();

                _disposed = true;
            }
        }

        
        /// <inheritdoc/>
        public byte[] ComputeHash(byte[] data)
        {
            if (HashSize != _hashAlgorithm.HashSize)
                throw new ArgumentOutOfRangeException("HashSize");

            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(data.ToArray());
        }

        /// <summary>
        /// Computes hash value for given stream.
        /// </summary>
        /// <param name="inputStream">Readable stream of data to hash.</param>
        /// <returns>Hash value of the data that was read from the stream.</returns>
        public byte[] ComputeHash(Stream inputStream)
        {
            if (HashSize != _hashAlgorithm.HashSize)
                throw new ArgumentOutOfRangeException("HashSize");

            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(inputStream);
        }
    }
}
