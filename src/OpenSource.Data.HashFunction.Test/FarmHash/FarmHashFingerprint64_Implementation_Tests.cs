using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint64_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void FarmHashFingerprint64_Implementation_Constructor_Works()
        {
            GC.KeepAlive(
                new FarmHashFingerprint64_Implementation());
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void FarmHashFingerprint64_Implementation_HashSizeInBits_Is64()
        {
            var farmHash = new FarmHashFingerprint64_Implementation();

            Assert.Equal(64, farmHash.HashSizeInBits);
        }

        #endregion



        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IFarmHashFingerprint64>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0x9ae16a3b2f90404f),
                    new KnownValue(64, TestConstants.FooBar.Take(3), 0x555c6f602f9383e3),
                    new KnownValue(64, TestConstants.FooBar, 0xc43fb29ab5effcfe),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(13), 0x54145170e3383fcc),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(17), 0xbb25bd7ca089d86),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(50), 0x5462bf74ef4729b1),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x7975a177275d65bf),
                };

            protected override IFarmHashFingerprint64 CreateHashFunction(int hashSize) =>
                new FarmHashFingerprint64_Implementation();
        }
    }
}
