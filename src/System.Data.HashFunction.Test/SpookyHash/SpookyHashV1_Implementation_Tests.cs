using System;
using System.Collections.Generic;
using System.Data.HashFunction.SpookyHash;
using System.Text;

namespace System.Data.HashFunction.Test.SpookyHash
{
    public class SpookyHashV1_Implementation_Tests
    {
        public class IHashFunctionAsync_Tests_SpookyHashV1
            : IHashFunctionAsync_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xd019a52d),
                    new KnownValue(64, TestConstants.FooBar, 0x52919208d019a52d),
                    new KnownValue(128, TestConstants.FooBar, "2da519d0089291529c22f24a80017a5e"),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xcc79cd7e),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x1c7efd4ccc79cd7e),
                    new KnownValue(128, TestConstants.LoremIpsum, "7ecd79cc4cfd7e1c5c15710c2d261311"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig() { 
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV1_DefaultConstructor
            : IHashFunctionAsync_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "2da519d0089291529c22f24a80017a5e"),
                    new KnownValue(128, TestConstants.LoremIpsum, "7ecd79cc4cfd7e1c5c15710c2d261311"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV1_WithInitVals
            : IHashFunctionAsync_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xddf2894b),
                    new KnownValue(64, TestConstants.FooBar, 0x35d04f6cddf2894b),
                    new KnownValue(128, TestConstants.FooBar, "2ffa3a68544614fc258f142b35dfb07a"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig() {
                        HashSizeInBits = hashSize,
                        Seed = 0x7da236b987930b75U,
                        Seed2 = 0x2eb994a3851d2f54U
                    });
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV1_WithInitVals_DefaultHashSize
            : IHashFunctionAsync_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "2ffa3a68544614fc258f142b35dfb07a"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig() {
                        Seed = 0x7da236b987930b75U,
                        Seed2 = 0x2eb994a3851d2f54U
                    });
        }
    
    }
}
