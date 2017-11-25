using System;
using System.Collections.Generic;
using System.Data.HashFunction.SpookyHash;
using System.Data.HashFunction.Test._Utilities;
using System.Text;

namespace System.Data.HashFunction.Test.SpookyHash
{
    public class SpookyHashV2_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests_SpookyHashV2
            : IHashFunctionAsync_TestBase<ISpookyHashV2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x03edde99),
                    new KnownValue(64, TestConstants.FooBar, 0x86c057a503edde99),
                    new KnownValue(128, TestConstants.FooBar, "65178fe24e37629a86c057a503edde99"),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xded54c84U),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xbb6b988bded54c84),
                    new KnownValue(128, TestConstants.LoremIpsum, "844cd5de8b986bbb1062913785ea1fa2"),
                };

            protected override ISpookyHashV2 CreateHashFunction(int hashSize) =>
                new SpookyHashV2_Implementation(
                    new SpookyHashConfig() { 
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV2_DefaultConstructor
            : IHashFunctionAsync_TestBase<ISpookyHashV2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "7040f0ce799f9b1f22020c7b2be86797"),
                    new KnownValue(128, TestConstants.LoremIpsum, "844cd5de8b986bbb1062913785ea1fa2"),
                };

            protected override ISpookyHashV2 CreateHashFunction(int hashSize) =>
                new SpookyHashV2_Implementation(
                    new SpookyHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV2_WithInitVals
            : IHashFunctionAsync_TestBase<ISpookyHashV2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x7acaff2f),
                    new KnownValue(64, TestConstants.FooBar, 0x080cdf197acaff2f),
                    new KnownValue(128, TestConstants.FooBar, "275e9e8cb0cc53c1d604509a253730a9"),
                };

            protected override ISpookyHashV2 CreateHashFunction(int hashSize) =>
                new SpookyHashV2_Implementation(
                    new SpookyHashConfig() { 
                        HashSizeInBits = hashSize,
                        Seed = 0x7da236b987930b75U,
                        Seed2 = 0x2eb994a3851d2f54U
                    });
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV2_WithInitVals_DefaultHashSize
            : IHashFunctionAsync_TestBase<ISpookyHashV2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "275e9e8cb0cc53c1d604509a253730a9"),
                };

            protected override ISpookyHashV2 CreateHashFunction(int hashSize) =>
                new SpookyHashV2_Implementation(
                    new SpookyHashConfig() { 
                        Seed = 0x7da236b987930b75U,
                        Seed2 = 0x2eb994a3851d2f54U
                    });
        }
    
    }
}
