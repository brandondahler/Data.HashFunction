using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    public class MurmurHash1Factory
        : IMurmurHash1Factory
    {
        public static IMurmurHash1Factory Instance { get; } = new MurmurHash1Factory();


        private MurmurHash1Factory()
        {

        }


        public IMurmurHash1 Create() =>
            Create(new MurmurHash1Config());

        public IMurmurHash1 Create(IMurmurHash1Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MurmurHash1_Implementation(config);
        }
    }
}
