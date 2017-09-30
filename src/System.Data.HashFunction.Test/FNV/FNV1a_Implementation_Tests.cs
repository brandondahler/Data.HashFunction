using System;
using System.Collections.Generic;
using System.Data.HashFunction.FNV;
using System.Data.HashFunction.Test._Utilities;
using System.Text;

namespace System.Data.HashFunction.Test.FNV
{
    public class FNV1a_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IFNV1a>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x811c9dc5),
                    new KnownValue(32, TestConstants.FooBar, 0xbf9cf968),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x15cc3fdb),
                    new KnownValue(64, TestConstants.Empty, 0xcbf29ce484222325),
                    new KnownValue(64, TestConstants.FooBar, 0x85944171f73967e8),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xd9daf73d6af17cfb),
                    new KnownValue(128, TestConstants.Empty, "8dc595627521b8624201bb072e27626c"),
                    new KnownValue(128, TestConstants.FooBar, "186f44ba97350d6fbf643c7962163e34"),
                    new KnownValue(128, TestConstants.LoremIpsum, "b3db4ee71f492ed1c2166a4bccdce8b6"),
                };

            protected override IFNV1a CreateHashFunction(int hashSize) =>
                new FNV1a_Implementation(
                    FNVConfig.GetPredefinedConfig(hashSize));
        }
    }
}
