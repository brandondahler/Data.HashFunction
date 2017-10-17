using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash3_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void MurmurHash3_Implementation_Constructor_ValidInputs_Works()
        {
            var murmurHash3ConfigMock = new Mock<IMurmurHash3Config>();
            {
                murmurHash3ConfigMock.SetupGet(mmhc => mmhc.HashSizeInBits)
                    .Returns(32);

                murmurHash3ConfigMock.Setup(mmhc => mmhc.Clone())
                    .Returns(() => murmurHash3ConfigMock.Object);
            }

            GC.KeepAlive(
                new MurmurHash3_Implementation(murmurHash3ConfigMock.Object));
        }


        #region Config

        [Fact]
        public void MurmurHash3_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new MurmurHash3_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void MurmurHash3_Implementation_Constructor_Config_IsCloned()
        {
            var murmurHash3ConfigMock = new Mock<IMurmurHash3Config>();
            {
                murmurHash3ConfigMock.Setup(mmhc => mmhc.Clone())
                    .Returns(() => new MurmurHash3Config() {
                        HashSizeInBits = 32
                    });
            }

            GC.KeepAlive(
                new MurmurHash3_Implementation(murmurHash3ConfigMock.Object));


            murmurHash3ConfigMock.Verify(mmhc => mmhc.Clone(), Times.Once);

            murmurHash3ConfigMock.VerifyGet(mmhc => mmhc.HashSizeInBits, Times.Never);
            murmurHash3ConfigMock.VerifyGet(mmhc => mmhc.Seed, Times.Never);
        }

        #region HashSizeInBits

        [Fact]
        public void MurmurHash3_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidHashSizes = new[] { -1, 0, 1, 31, 33, 64, 127, 129, 256, 512 };

            foreach (var invalidHashSize in invalidHashSizes)
            {
                var murmurHash3ConfigMock = new Mock<IMurmurHash3Config>();
                {
                    murmurHash3ConfigMock.SetupGet(mmhc => mmhc.HashSizeInBits)
                        .Returns(invalidHashSize);

                    murmurHash3ConfigMock.Setup(mmhc => mmhc.Clone())
                        .Returns(() => murmurHash3ConfigMock.Object);
                }

                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new MurmurHash3_Implementation(murmurHash3ConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void MurmurHash3_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validHashSizes = new[] { 32, 128 };

            foreach (var validHashSize in validHashSizes)
            {
                var murmurHash3ConfigMock = new Mock<IMurmurHash3Config>();
                {
                    murmurHash3ConfigMock.SetupGet(mmhc => mmhc.HashSizeInBits)
                        .Returns(validHashSize);

                    murmurHash3ConfigMock.Setup(mmhc => mmhc.Clone())
                        .Returns(() => murmurHash3ConfigMock.Object);
                }

                GC.KeepAlive(
                    new MurmurHash3_Implementation(murmurHash3ConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion


        #region ComputeHash

        [Fact]
        public void MurmurHash3_Implementation_ComputeHash_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var murmurHash3ConfigMock = new Mock<IMurmurHash3Config>();
            {
                var readCount = 0;

                murmurHash3ConfigMock.SetupGet(jlc => jlc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                murmurHash3ConfigMock.Setup(jlc => jlc.Clone())
                    .Returns(() => murmurHash3ConfigMock.Object);
            }


            var murmurHash3 = new MurmurHash3_Implementation(murmurHash3ConfigMock.Object);

            Assert.Throws<NotImplementedException>(
                () => murmurHash3.ComputeHash(new byte[1]));
        }

        #endregion

        #region ComputeHashAsync

        [Fact]
        public async Task MurmurHash3_Implementation_ComputeHashAsync_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var murmurHash3ConfigMock = new Mock<IMurmurHash3Config>();
            {
                var readCount = 0;

                murmurHash3ConfigMock.SetupGet(jlc => jlc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                murmurHash3ConfigMock.Setup(jlc => jlc.Clone())
                    .Returns(() => murmurHash3ConfigMock.Object);
            }


            var murmurHash3 = new MurmurHash3_Implementation(murmurHash3ConfigMock.Object);

            using (var memoryStream = new MemoryStream(new byte[1]))
            {
                await Assert.ThrowsAsync<NotImplementedException>(
                    () => murmurHash3.ComputeHashAsync(memoryStream));
            }
        }

        #endregion


        public class IHashFunctionAsync_Tests_MurmurHash3
            : IHashFunctionAsync_TestBase<IMurmurHash3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xa4c4d4bd),
                    new KnownValue(128, TestConstants.FooBar, "455ac81671aed2bdafd6f8bae055a274"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0x5a1d7378),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(7), "1689190f13f3290b3c5ead34c751ea8a"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(31), 0x8d50f530),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(31), "5c769a439b78878e8640e16335e4313f"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(33), "7fdc48ea7913cb074565f43f4c1e1356"),
                };

            protected override IMurmurHash3 CreateHashFunction(int hashSize) =>
                new MurmurHash3_Implementation(
                    new MurmurHash3Config() {
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_MurmurHash3_DefaultConstructor
            : IHashFunctionAsync_TestBase<IMurmurHash3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xa4c4d4bd),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0x5a1d7378),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(31), 0x8d50f530),
                };

            protected override IMurmurHash3 CreateHashFunction(int hashSize) =>
                new MurmurHash3_Implementation(
                    new MurmurHash3Config());
        }
    
    }
}
