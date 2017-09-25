using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public class MurmurHash2Factory
        : IMurmurHash2Factory
    {
        public static IMurmurHash2Factory Instance { get; } = new MurmurHash2Factory();


        private MurmurHash2Factory()
        {

        }


        public IMurmurHash2 Create() =>
            Create(new MurmurHash2Config());

        public IMurmurHash2 Create(IMurmurHash2Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MurmurHash2_Implementation(config);
        }

    }
}
