using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    public sealed class MetroHash128Factory
        : MetroHashFactoryBase<IMetroHash128>,
            IMetroHash128Factory
    {
        public static IMetroHash128Factory Instance { get; } = new MetroHash128Factory();


        private MetroHash128Factory()
        {

        }


        public override IMetroHash128 Create() => Create(new MetroHashConfig());

        public override IMetroHash128 Create(IMetroHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MetroHash128_Implementation(config);
        }
    }
}
