using System;
using System.Collections.Generic;
using System.Data.HashFunction.CityHash;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Test.CityHash
{
    public class CityHash_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<ICityHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xc2ebd64e),
                    new KnownValue(32, TestConstants.RandomShort, 0x1fcea779),
                    new KnownValue(32, TestConstants.RandomLong, 0x9dba44d0),
                    new KnownValue(64, TestConstants.Empty, 0x9ae16a3b2f90404f),
                    new KnownValue(64, TestConstants.FooBar, 0xc43fb29ab5effcfe),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x764df1e17d92d1eb),
                    new KnownValue(64, TestConstants.RandomShort, 0x3ef8698eae651b16),
                    new KnownValue(64, TestConstants.RandomLong, 0x39e9fcdba69979b0),
                    new KnownValue(128, TestConstants.Empty, "2b9ac064fc9df03d291ee592c340b53c"),
                    new KnownValue(128, TestConstants.FooBar, "5064c017cf2c1672daa1f13a15b78c98"),
                    new KnownValue(128, TestConstants.LoremIpsum, "31dd5cb57a6c29dc0826565eeb0cf6a4"),
                    new KnownValue(128, TestConstants.RandomShort, "67cbf6f803487e7e09bffce371172c13"),
                    new KnownValue(128, TestConstants.RandomLong, "98999f077f446a5ee962148d86279ea0"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(3), 0xd83c2fa0),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(3), 0xfa10ac780bf932dd),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(3), "8b5529b80301a1c414cc313959fd5255"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa6480aae),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x3a03aa21105c4286),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(23), "658f52c24d66d71d844823de90c3d9ac"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(64), 0x8ace2a1a),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(64), 0x2167be8daa61f94d),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(64), "50e89788e0dbb5d6784fbcbdf57264d1"),
                };

            protected override ICityHash CreateHashFunction(int hashSize) =>
                new CityHash_Implementation(
                    new CityHashConfig() {
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_DefaultConstructor
            : IHashFunctionAsync_TestBase<ICityHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xc2ebd64e),
                    new KnownValue(32, TestConstants.RandomShort, 0x1fcea779),
                    new KnownValue(32, TestConstants.RandomLong, 0x9dba44d0),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(3), 0xd83c2fa0),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa6480aae),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(64), 0x8ace2a1a),
                };

            protected override ICityHash CreateHashFunction(int hashSize) =>
                new CityHash_Implementation(
                    new CityHashConfig());
        }
    

    }
}
