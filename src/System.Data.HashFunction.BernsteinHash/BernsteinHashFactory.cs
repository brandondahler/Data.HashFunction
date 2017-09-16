using System;
using System.Collections.Generic;
using System.Data.HashFunction.BernsteinHash;
using System.Text;

namespace System.Data.HashFunction.BernsteinHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IBernsteinHash"/>.
    /// </summary>
    /// <seealso cref="IBernsteinHashFactory" />
    public class BernsteinHashFactory
        : IBernsteinHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IBernsteinHash" /> instance with the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="IBernsteinHash" /> instance.
        /// </returns>
        public IBernsteinHash Create()
        {
            return new BernsteinHash_Implementation();
        }
    }
}
