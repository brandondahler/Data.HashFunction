using System;
using System.Collections.Generic;
using System.Data.HashFunction.BernsteinHash;
using System.Text;

namespace System.Data.HashFunction.Test.BernsteinHash
{
    public class ModifiedBernsteinHash_Implementation_Tests
    {
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IModifiedBernsteinHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(32, TestConstants.FooBar, 0xf030b397),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xfeceaf2a),
                };

            protected override IModifiedBernsteinHash CreateHashFunction(int hashSize)
            {
                return new ModifiedBernsteinHash_Implementation();
            }
        }
    }
}
