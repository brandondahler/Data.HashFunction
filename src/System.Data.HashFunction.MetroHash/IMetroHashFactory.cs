using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    public interface IMetroHashFactory
    {
        IMetroHash Create();

        IMetroHash Create(IMetroHashConfig config);
    }
}
