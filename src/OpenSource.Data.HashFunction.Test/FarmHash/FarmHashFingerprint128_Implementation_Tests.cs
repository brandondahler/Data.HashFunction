using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint128_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void FarmHashFingerprint128_Implementation_Constructor_Works()
        {
            GC.KeepAlive(
                new FarmHashFingerprint128_Implementation());
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void FarmHashFingerprint128_Implementation_HashSizeInBits_Is128()
        {
            var farmHash = new FarmHashFingerprint128_Implementation();

            Assert.Equal(128, farmHash.HashSizeInBits);
        }

        #endregion



        public class IHashFunctionAsync_Tests
            : IHashFunction_TestBase<IFarmHashFingerprint128>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.Empty, "0x3cb540c392e51e293df09dfc64c09a2b"),
                    new KnownValue(128, TestConstants.FooBar.Take(3), "0xb39b3f148460e4ac14be06e50b80b82e"),
                    new KnownValue(128, TestConstants.FooBar, "0x988cb7153af1a1da72162ccf17c06450"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(13), "0x2e8ba24e20ecfd83a0adc6c41584ca33"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(17), "0x5f74542e89cb32d3fb12ca78d96817cb"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(31), "0x4a98ff6acfe7dc9ec455b7f9b98865b7"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(50), "0xbfbb5a9fa793e2b857fff2a86ba83dfd"),
                    new KnownValue(128, TestConstants.LoremIpsum, "0xa4f60ceb5e562608dc296c7ab55cdd31"),
                };

            protected override IFarmHashFingerprint128 CreateHashFunction(int hashSize) =>
                new FarmHashFingerprint128_Implementation();
        }
    }
}
