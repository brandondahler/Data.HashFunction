using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    public sealed class MetroHash64Factory
        : MetroHashFactoryBase<IMetroHash64>,
            IMetroHash64Factory
    {
        public static IMetroHash64Factory Instance { get; } = new MetroHash64Factory();


        private MetroHash64Factory()
        {

        }


        public override IMetroHash64 Create() => Create(new MetroHashConfig());

        public override IMetroHash64 Create(IMetroHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MetroHash64_Implementation(config);
        }
    }
}
