using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    public class SpookyHashV2Factory
        : ISpookyHashV2Factory
    {
        public static ISpookyHashV2Factory Instance { get; } = new SpookyHashV2Factory();


        private SpookyHashV2Factory()
        {

        }


        public ISpookyHashV2 Create() =>
            Create(new SpookyHashConfig());

        public ISpookyHashV2 Create(ISpookyHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new SpookyHashV2_Implementation(config);
        }
    }
}
