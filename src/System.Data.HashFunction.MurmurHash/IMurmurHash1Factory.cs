using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public interface IMurmurHash1Factory
    {
        IMurmurHash1 Create();

        IMurmurHash1 Create(IMurmurHash1Config config);
    }
}
