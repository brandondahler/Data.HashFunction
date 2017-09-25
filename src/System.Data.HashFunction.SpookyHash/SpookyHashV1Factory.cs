using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    public class SpookyHashV1Factory
        : ISpookyHashV1Factory
    {
        public static ISpookyHashV1Factory Instance { get; } = new SpookyHashV1Factory();


        private SpookyHashV1Factory()
        {

        }


        public ISpookyHashV1 Create() =>
            Create(new SpookyHashConfig());

        public ISpookyHashV1 Create(ISpookyHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new SpookyHashV1_Implementation(config);
        }
    }
}
