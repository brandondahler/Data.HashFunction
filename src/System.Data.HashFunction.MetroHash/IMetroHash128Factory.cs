using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    public interface IMetroHash128Factory
        : IMetroHashFactory
    {
        new IMetroHash128 Create();
        new IMetroHash128 Create(IMetroHashConfig config);
    }
}
