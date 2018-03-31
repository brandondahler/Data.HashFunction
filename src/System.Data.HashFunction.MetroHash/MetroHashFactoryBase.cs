using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    public abstract class MetroHashFactoryBase<TMetroHash>
        : IMetroHashFactory
        where TMetroHash : IMetroHash
    {
        public abstract TMetroHash Create();
        public abstract TMetroHash Create(IMetroHashConfig config);


        IMetroHash IMetroHashFactory.Create() => Create();
        IMetroHash IMetroHashFactory.Create(IMetroHashConfig config) => Create(config);
    }
}
