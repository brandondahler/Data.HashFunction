using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public interface IMurmurHash3Factory
    {
        IMurmurHash3 Create();

        IMurmurHash3 Create(IMurmurHash3Config config);
    }
}
