using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public class MurmurHash3Factory
        : IMurmurHash3Factory
    {
        public static IMurmurHash3Factory Instance { get; } = new MurmurHash3Factory();


        private MurmurHash3Factory()
        {

        }


        public IMurmurHash3 Create() =>
            Create(new MurmurHash3Config());

        public IMurmurHash3 Create(IMurmurHash3Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MurmurHash3_Implementation(config);
        }
    }
}
