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
        public abstract byte[] ComputeHash(byte[] data);
    }
}
