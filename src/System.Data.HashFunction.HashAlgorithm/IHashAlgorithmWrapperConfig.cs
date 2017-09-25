using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.HashAlgorithm
{
    using HashAlgorithm = System.Security.Cryptography.HashAlgorithm;

    public interface IHashAlgorithmWrapperConfig
    {
        Func<HashAlgorithm> InstanceFactory { get; }

        

        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IHashAlgorithmWrapperConfig Clone();
    }
}