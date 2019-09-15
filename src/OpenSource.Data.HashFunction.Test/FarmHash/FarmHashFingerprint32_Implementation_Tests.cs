using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint32_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void FarmHashFingerprint32_Implementation_Constructor_Works()
        {
            GC.KeepAlive(
                new FarmHashFingerprint32_Implementation());
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void FarmHashFingerprint32_Implementation_HashSizeInBits_Is32()
        {
            var farmHash = new FarmHashFingerprint32_Implementation();

            Assert.Equal(32, farmHash.HashSizeInBits);
        }

        #endregion



        public class IHashFunctionAsync_Tests
            : IHashFunction_TestBase<IFarmHashFingerprint32>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar.Take(3), 0x6b5025e3),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(17), 0xe3e27892),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x6482ed0d),
                };

            protected override IFarmHashFingerprint32 CreateHashFunction(int hashSize) =>
                new FarmHashFingerprint32_Implementation();
        }
    }
}
