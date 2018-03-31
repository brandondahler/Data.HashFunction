using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    public interface IMetroHash64Factory
        : IMetroHashFactory
    {
        new IMetroHash64 Create();
        new IMetroHash64 Create(IMetroHashConfig config);
    }
}
