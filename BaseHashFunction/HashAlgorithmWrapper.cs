using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class HashAlgorithmWrapper
        : IHashFunction, IDisposable
    {
        public int HashSize
        {
            get { return _hashAlgorithm.HashSize; }
            set { if (value != _hashAlgorithm.HashSize) throw new ArgumentOutOfRangeException(); }
        }

        public IEnumerable<int> ValidHashSizes { get { return new[] { _hashAlgorithm.HashSize }; } }



        protected readonly HashAlgorithm _hashAlgorithm;
        protected readonly bool _ownsInstance;

        protected readonly object SyncRoot = new object();

        private bool _disposed = false;

        public HashAlgorithmWrapper(HashAlgorithm hashAlgorithm, bool ownsInstance = true)
        {
            _hashAlgorithm = hashAlgorithm;
            _ownsInstance = ownsInstance;
        }

        ~HashAlgorithmWrapper()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (SyncRoot)
            {
                if (!_disposed && _ownsInstance)
                    _hashAlgorithm.Dispose();

                _disposed = true;
            }
        }


        public byte[] ComputeHash(byte[] data)
        {
            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(data);
        }

        public byte[] ComputeHash(Stream inputStream)
        {
            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(inputStream);
        }
    }


    public class HashAlgorithmWrapper<HashAlgorithmT>
        : IHashFunction, IDisposable
        where HashAlgorithmT : HashAlgorithm, new()
    {
        public int HashSize
        {
            get { return _hashAlgorithm.HashSize; }
            set { if (value != _hashAlgorithm.HashSize) throw new ArgumentOutOfRangeException(); }
        }

        public IEnumerable<int> ValidHashSizes { get { return new[] { _hashAlgorithm.HashSize }; } }


        protected readonly HashAlgorithmT _hashAlgorithm;
        protected readonly object SyncRoot = new object();
        
        
        private bool _disposed = false;


        public HashAlgorithmWrapper()
        {
            _hashAlgorithm = new HashAlgorithmT();
        }

        ~HashAlgorithmWrapper()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (SyncRoot)
            {
                if (!_disposed)
                    _hashAlgorithm.Dispose();

                _disposed = true;
            }
        }

        
        public byte[] ComputeHash(byte[] data)
        {
            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(data);
        }

        public byte[] ComputeHash(Stream inputStream)
        {
            lock (SyncRoot)
                return _hashAlgorithm.ComputeHash(inputStream);
        }
    }
}
