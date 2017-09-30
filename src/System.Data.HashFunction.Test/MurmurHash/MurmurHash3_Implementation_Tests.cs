using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash3_Implementation_Tests
    {
        public class IHashFunctionAsync_Tests_MurmurHash3
            : IHashFunctionAsync_TestBase<IMurmurHash3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xa4c4d4bd),
                    new KnownValue(128, TestConstants.FooBar, "455ac81671aed2bdafd6f8bae055a274"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0x5a1d7378),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(7), "1689190f13f3290b3c5ead34c751ea8a"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(31), 0x8d50f530),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(31), "5c769a439b78878e8640e16335e4313f"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(33), "7fdc48ea7913cb074565f43f4c1e1356"),
                };

            protected override IMurmurHash3 CreateHashFunction(int hashSize) =>
                new MurmurHash3_Implementation(
                    new MurmurHash3Config() {
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_MurmurHash3_DefaultConstructor
            : IHashFunctionAsync_TestBase<IMurmurHash3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xa4c4d4bd),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0x5a1d7378),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(31), 0x8d50f530),
                };

            protected override IMurmurHash3 CreateHashFunction(int hashSize) =>
                new MurmurHash3_Implementation(
                    new MurmurHash3Config());
        }
    
    }
}
