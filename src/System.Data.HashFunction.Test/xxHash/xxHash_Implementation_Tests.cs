using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test._Utilities;
using System.Data.HashFunction.xxHash;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Test.xxHash
{
    public class xxHash_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests_xxHash
            : IHashFunctionAsync_TestBase<IxxHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x02cc5d05),
                    new KnownValue(32, TestConstants.FooBar, 0xeda34aaf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x92ea46ac),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(4), 0x0df3e9ea),
                    new KnownValue(64, TestConstants.Empty, 0xef46db3751d8e999),
                    new KnownValue(64, TestConstants.FooBar, 0xa2aa05ed9085aaf9),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xaf35642971419cbe),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(4), 0x103460bb4a599cab),
                };

            protected override IxxHash CreateHashFunction(int hashSize) =>
                new xxHash_Implementation(
                    new xxHashConfig() { 
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_xxHash_DefaultConstructor
            : IHashFunctionAsync_TestBase<IxxHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x02cc5d05),
                    new KnownValue(32, TestConstants.FooBar, 0xeda34aaf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x92ea46ac),
                };

            protected override IxxHash CreateHashFunction(int hashSize) =>
                new xxHash_Implementation(
                    new xxHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_xxHash_WithInitVal
            : IHashFunctionAsync_TestBase<IxxHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xff52b36b),
                    new KnownValue(32, TestConstants.FooBar, 0x294f6b05),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x01f950ab),
                    new KnownValue(64, TestConstants.Empty, 0x985e09f666271418),
                    new KnownValue(64, TestConstants.FooBar, 0x947ebc3ef380d35d),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xf6b6e74f681d3e5b),
                };

            protected override IxxHash CreateHashFunction(int hashSize) =>
                new xxHash_Implementation(
                    new xxHashConfig() { 
                        HashSizeInBits = hashSize,
                        Seed = 0x78fef705b7c769faU
                    });
        }
    

    }
}
