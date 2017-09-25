using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public interface IMurmurHash2Factory
    {
        IMurmurHash2 Create();

        IMurmurHash2 Create(IMurmurHash2Config config);
    }
}
