using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.BuzHash;
using System.Text;

namespace System.Data.HashFunction.Test.BuzHash
{
    public class BuzHash_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IBuzHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0xd3),
                    new KnownValue(8, TestConstants.FooBar, 0xb2),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x83),
                    new KnownValue(16, TestConstants.Empty, 0x37d3),
                    new KnownValue(16, TestConstants.FooBar, 0xd088),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x284a),
                    new KnownValue(32, TestConstants.Empty, 0xfd0337d3),
                    new KnownValue(32, TestConstants.FooBar, 0xe3d9d0b6),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x2f16d6a7),
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0xe8ebaa27e3d9d09f),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xfad1d8bf5108c8dd),
                };

            protected override IBuzHash CreateHashFunction(int hashSize) =>
                new BuzHash_Implementation(
                    new DefaultBuzHashConfig() { 
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_DefaultConstructor
            : IHashFunctionAsync_TestBase<IBuzHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0xe8ebaa27e3d9d09f),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xfad1d8bf5108c8dd),
                };

            protected override IBuzHash CreateHashFunction(int hashSize) =>
                new BuzHash_Implementation(
                    new DefaultBuzHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_RightShift
            : IHashFunctionAsync_TestBase<IBuzHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0xd3),
                    new KnownValue(8, TestConstants.FooBar, 0x8b),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x97),
                    new KnownValue(16, TestConstants.Empty, 0x37d3),
                    new KnownValue(16, TestConstants.FooBar, 0xe16b),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x076f),
                    new KnownValue(32, TestConstants.Empty, 0xfd0337d3),
                    new KnownValue(32, TestConstants.FooBar, 0xd2d7f16b),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x36d306ab),
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0x7f44cd6ecad7f16b),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xb826c048cb5399a3),
                };

            protected override IBuzHash CreateHashFunction(int hashSize) =>
                new BuzHash_Implementation(
                    new DefaultBuzHashConfig() {
                        HashSizeInBits = hashSize,
                        ShiftDirection = CircularShiftDirection.Right
                    });
        }
    

        public class IHashFunctionAsync_Tests_RightShift_DefaultConstructor
            : IHashFunctionAsync_TestBase<IBuzHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0x7f44cd6ecad7f16b),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xb826c048cb5399a3),
                };

            protected override IBuzHash CreateHashFunction(int hashSize) =>
                new BuzHash_Implementation(
                    new DefaultBuzHashConfig() {
                        ShiftDirection = CircularShiftDirection.Right
                    });
        }
    }
}
