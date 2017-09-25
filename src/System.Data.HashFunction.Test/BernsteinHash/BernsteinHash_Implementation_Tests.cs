using System;
using System.Collections.Generic;
using System.Data.HashFunction.BernsteinHash;
using System.Text;

namespace System.Data.HashFunction.Test.BernsteinHash
{
    public class BernsteinHash_Implementation_Tests
    {
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IBernsteinHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(32, TestConstants.FooBar, 0xf6055bf9),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x24bdc248),
                };

            protected override IBernsteinHash CreateHashFunction(int hashSize)
            {
                return new BernsteinHash_Implementation();
            }
        }

    }
}
