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
    public class MurmurHash2_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void MurmurHash2_Implementation_Constructor_ValidInputs_Works()
        {
            var murmurHash2ConfigMock = new Mock<IMurmurHash2Config>();
            {
                murmurHash2ConfigMock.SetupGet(mmhc => mmhc.HashSizeInBits)
                    .Returns(32);

                murmurHash2ConfigMock.Setup(mmhc => mmhc.Clone())
                    .Returns(() => murmurHash2ConfigMock.Object);
            }

            GC.KeepAlive(
                new MurmurHash2_Implementation(murmurHash2ConfigMock.Object));
        }


        #region Config

        [Fact]
        public void MurmurHash2_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new MurmurHash2_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void MurmurHash2_Implementation_Constructor_Config_IsCloned()
        {
            var murmurHash2ConfigMock = new Mock<IMurmurHash2Config>();
            {
                murmurHash2ConfigMock.Setup(mmhc => mmhc.Clone())
                    .Returns(() => new MurmurHash2Config() {
                        HashSizeInBits = 32
                    });
            }

            GC.KeepAlive(
                new MurmurHash2_Implementation(murmurHash2ConfigMock.Object));


            murmurHash2ConfigMock.Verify(mmhc => mmhc.Clone(), Times.Once);

            murmurHash2ConfigMock.VerifyGet(mmhc => mmhc.HashSizeInBits, Times.Never);
            murmurHash2ConfigMock.VerifyGet(mmhc => mmhc.Seed, Times.Never);
        }

        #region HashSizeInBits

        [Fact]
        public void MurmurHash2_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidHashSizes = new[] { -1, 0, 1, 31, 33, 63, 65, 128, 256, 512 };

            foreach (var invalidHashSize in invalidHashSizes)
            {
                var murmurHash2ConfigMock = new Mock<IMurmurHash2Config>();
                {
                    murmurHash2ConfigMock.SetupGet(mmhc => mmhc.HashSizeInBits)
                        .Returns(invalidHashSize);

                    murmurHash2ConfigMock.Setup(mmhc => mmhc.Clone())
                        .Returns(() => murmurHash2ConfigMock.Object);
                }

                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new MurmurHash2_Implementation(murmurHash2ConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void MurmurHash2_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validHashSizes = new[] { 32, 64 };

            foreach (var validHashSize in validHashSizes)
            {
                var murmurHash2ConfigMock = new Mock<IMurmurHash2Config>();
                {
                    murmurHash2ConfigMock.SetupGet(mmhc => mmhc.HashSizeInBits)
                        .Returns(validHashSize);

                    murmurHash2ConfigMock.Setup(mmhc => mmhc.Clone())
                        .Returns(() => murmurHash2ConfigMock.Object);
                }

                GC.KeepAlive(
                    new MurmurHash2_Implementation(murmurHash2ConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion


        #region ComputeHash

        [Fact]
        public void MurmurHash2_Implementation_ComputeHash_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var murmurHash2ConfigMock = new Mock<IMurmurHash2Config>();
            {
                var readCount = 0;

                murmurHash2ConfigMock.SetupGet(jlc => jlc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                murmurHash2ConfigMock.Setup(jlc => jlc.Clone())
                    .Returns(() => murmurHash2ConfigMock.Object);
            }


            var murmurHash2 = new MurmurHash2_Implementation(murmurHash2ConfigMock.Object);

            Assert.Throws<NotImplementedException>(
                () => murmurHash2.ComputeHash(new byte[1]));
        }

        #endregion

        #region ComputeHashAsync

        [Fact]
        public async Task MurmurHash2_Implementation_ComputeHashAsync_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var murmurHash2ConfigMock = new Mock<IMurmurHash2Config>();
            {
                var readCount = 0;

                murmurHash2ConfigMock.SetupGet(jlc => jlc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                murmurHash2ConfigMock.Setup(jlc => jlc.Clone())
                    .Returns(() => murmurHash2ConfigMock.Object);
            }


            var murmurHash2 = new MurmurHash2_Implementation(murmurHash2ConfigMock.Object);

            using (var memoryStream = new MemoryStream(new byte[1]))
            {
                await Assert.ThrowsAsync<NotImplementedException>(
                    () => murmurHash2.ComputeHashAsync(memoryStream));
            }
        }

        #endregion


        public class IHashFunctionAsync_Tests_MurmurHash2
            : IHashFunctionAsync_TestBase<IMurmurHash2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x6715a92e),
                    new KnownValue(64, TestConstants.FooBar, 0xd49f461720d7a196),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x384025b5),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(11), 0xa4d1d1c83f3125d2),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0xfa38fed50a3dc771),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x82d0eccc1172c984),
                };

            protected override IMurmurHash2 CreateHashFunction(int hashSize) =>
                new MurmurHash2_Implementation(
                    new MurmurHash2Config() {
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_MurmurHash2_DefaultConstructor
            : IHashFunctionAsync_TestBase<IMurmurHash2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, TestConstants.FooBar, 0xd49f461720d7a196),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0xfa38fed50a3dc771),
                };

            protected override IMurmurHash2 CreateHashFunction(int hashSize) =>
                new MurmurHash2_Implementation(
                    new MurmurHash2Config());
        }
    
    }
}
