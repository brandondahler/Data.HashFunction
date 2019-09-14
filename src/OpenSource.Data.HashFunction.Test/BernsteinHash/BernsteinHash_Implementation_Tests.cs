using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.BernsteinHash;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.BernsteinHash
{
    public class BernsteinHash_Implementation_Tests
    {

        [Fact]
        public void BernsteinHash_Implementation_HashSizeInBits_IsSet()
        {
            var bernsteinHash = new BernsteinHash_Implementation();

            Assert.Equal(32, bernsteinHash.HashSizeInBits);
        }


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
