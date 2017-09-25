using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.xxHash
{
    public interface IxxHashFactory
    {
        IxxHash Create();

        IxxHash Create(IxxHashConfig config);
    }
}
