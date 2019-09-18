using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.BernsteinHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IModifiedBernsteinHash"/>.
    /// </summary>
    public class ModifiedBernsteinHashFactory
        : IModifiedBernsteinHashFactory
    {

        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IModifiedBernsteinHashFactory Instance { get; } = new ModifiedBernsteinHashFactory();


        private ModifiedBernsteinHashFactory()
        {

        }

        /// <summary>
        /// Creates a new <see cref="IModifiedBernsteinHash" /> instance with the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="IModifiedBernsteinHash" /> instance.
        /// </returns>
        public IModifiedBernsteinHash Create()
        {
            return new ModifiedBernsteinHash_Implementation();
        }
    }
}
