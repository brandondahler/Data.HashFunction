using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash2_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests_MurmurHash2
            : IHashFunctionAsync_TestBase<IMurmurHash2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x6715a92e),
                    new KnownValue(64, TestConstants.FooBar, 0xd49f461720d7a196),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x384025b5),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(11), 0xa4d1d1c83f3125d2),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0xfa38fed50a3dc771),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x82d0eccc1172c984),
                };

            protected override IMurmurHash2 CreateHashFunction(int hashSize) =>
                new MurmurHash2_Implementation(
                    new MurmurHash2Config() {
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_MurmurHash2_DefaultConstructor
            : IHashFunctionAsync_TestBase<IMurmurHash2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, TestConstants.FooBar, 0xd49f461720d7a196),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0xfa38fed50a3dc771),
                };

            protected override IMurmurHash2 CreateHashFunction(int hashSize) =>
                new MurmurHash2_Implementation(
                    new MurmurHash2Config());
        }
    
    }
}
