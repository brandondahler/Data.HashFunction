using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.SpookyHash;
using System.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.SpookyHash
{
    public class SpookyHashV2_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void SpookyHashV2_Implementation_Constructor_ValidInputs_Works()
        {
            var spookyHashConfigMock = new Mock<ISpookyHashConfig>();
            {
                spookyHashConfigMock.SetupGet(xhc => xhc.HashSizeInBits)
                    .Returns(32);

                spookyHashConfigMock.SetupGet(xhc => xhc.Seed)
                    .Returns(0UL);

                spookyHashConfigMock.SetupGet(xhc => xhc.Seed2)
                    .Returns(0UL);

                spookyHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => spookyHashConfigMock.Object);
            }

            GC.KeepAlive(
                new SpookyHashV2_Implementation(spookyHashConfigMock.Object));
        }


        #region Config

        [Fact]
        public void SpookyHashV2_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new SpookyHashV2_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void SpookyHashV2_Implementation_Constructor_Config_IsCloned()
        {
            var spookyHashConfigMock = new Mock<ISpookyHashConfig>();
            {
                spookyHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => new SpookyHashConfig() {
                        HashSizeInBits = 32,
                    });
            }

            GC.KeepAlive(
                new SpookyHashV2_Implementation(spookyHashConfigMock.Object));


            spookyHashConfigMock.Verify(xhc => xhc.Clone(), Times.Once);

            spookyHashConfigMock.VerifyGet(xhc => xhc.HashSizeInBits, Times.Never);
            spookyHashConfigMock.VerifyGet(xhc => xhc.Seed, Times.Never);
            spookyHashConfigMock.VerifyGet(xhc => xhc.Seed2, Times.Never);
        }

        #region HashSizeInBits

        [Fact]
        public void SpookyHashV2_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidHashSizes = new[] { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 31, 33, 63, 65, 127, 129, 65535 };

            foreach (var invalidHashSize in invalidHashSizes)
            {
                var spookyHashConfigMock = new Mock<ISpookyHashConfig>();
                {
                    spookyHashConfigMock.SetupGet(pc => pc.HashSizeInBits)
                        .Returns(invalidHashSize);

                    spookyHashConfigMock.Setup(pc => pc.Clone())
                        .Returns(() => spookyHashConfigMock.Object);
                }

                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new SpookyHashV2_Implementation(spookyHashConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void SpookyHashV2_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validHashSizes = new[] { 32, 64, 128 };

            foreach (var validHashSize in validHashSizes)
            {

                var spookyHashConfigMock = new Mock<ISpookyHashConfig>();
                {
                    spookyHashConfigMock.SetupGet(pc => pc.HashSizeInBits)
                        .Returns(validHashSize);

                    spookyHashConfigMock.Setup(pc => pc.Clone())
                        .Returns(() => spookyHashConfigMock.Object);
                }

                GC.KeepAlive(
                    new SpookyHashV2_Implementation(spookyHashConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion


        #region ComputeHash

        [Fact]
        public void SpookyHashV2_Implementation_ComputeHash_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var spookyHashConfigMock = new Mock<ISpookyHashConfig>();
            {
                var readCount = 0;

                spookyHashConfigMock.SetupGet(xhc => xhc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                spookyHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => spookyHashConfigMock.Object);
            }


            var spookyHashV2 = new SpookyHashV2_Implementation(spookyHashConfigMock.Object);

            Assert.Throws<NotImplementedException>(
                () => spookyHashV2.ComputeHash(new byte[1]));
        }

        #endregion

        #region ComputeHashAsync

        [Fact]
        public async Task SpookyHashV2_Implementation_ComputeHashAsync_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var spookyHashConfigMock = new Mock<ISpookyHashConfig>();
            {
                var readCount = 0;

                spookyHashConfigMock.SetupGet(xhc => xhc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                spookyHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => spookyHashConfigMock.Object);
            }


            var spookyHashV2 = new SpookyHashV2_Implementation(spookyHashConfigMock.Object);

            using (var memoryStream = new MemoryStream(new byte[1]))
            {
                await Assert.ThrowsAsync<NotImplementedException>(
                    () => spookyHashV2.ComputeHashAsync(memoryStream));
            }
        }

        #endregion


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
