using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash1_Implementation_Tests
    {
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IMurmurHash1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x5c45f49b),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0xff3a37aa),
                };

            protected override IMurmurHash1 CreateHashFunction(int hashSize) =>
                new MurmurHash1_Implementation(
                    new MurmurHash1Config());
        }


    }
}
