using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.HashAlgorithm
{
    using HashAlgorithm = Security.Cryptography.HashAlgorithm;

    /// <summary>
    /// Defines a configuration for a <see cref="IHashAlgorithmWrapper"/> implementation.
    /// </summary>
    public class HashAlgorithmWrapperConfig
        : IHashAlgorithmWrapperConfig
    {
        /// <summary>
        /// A delegate that produces <see cref="HashAlgorithm"/> instances.
        /// </summary>
        /// <value>
        /// The delegate.
        /// </value>
        public Func<HashAlgorithm> InstanceFactory { get; set; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IHashAlgorithmWrapperConfig Clone() => 
            new HashAlgorithmWrapperConfig() {
                InstanceFactory = InstanceFactory
            };
    }
}
