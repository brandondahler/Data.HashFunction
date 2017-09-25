using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.HashAlgorithm
{
    public interface IHashAlgorithmWrapperFactory
    {
        IHashAlgorithmWrapper Create(IHashAlgorithmWrapperConfig config);
    }
}
