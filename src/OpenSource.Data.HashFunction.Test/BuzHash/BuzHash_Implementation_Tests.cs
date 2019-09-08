using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.BuzHash;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.BuzHash
{
    public class BuzHash_Implementation_Tests
    {

        #region Constructor
        
        [Fact]
        public void BuzHash_Implementation_Constructor_ValidInputs_Work()
        {
            var buzHashConfigMock = new Mock<IBuzHashConfig>();
            {
                buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                    .Returns(new UInt64[256]);

                buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                    .Returns(64);

                buzHashConfigMock.SetupGet(bhc => bhc.Seed)
                    .Returns(0);

                buzHashConfigMock.SetupGet(bhc => bhc.ShiftDirection)
                    .Returns(CircularShiftDirection.Left);

                buzHashConfigMock.Setup(bhc => bhc.Clone())
                    .Returns(() => buzHashConfigMock.Object);
            }


            GC.KeepAlive(
                new BuzHash_Implementation(buzHashConfigMock.Object));
        }


        #region Config

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new BuzHash_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_IsCloned()
        {
            var buzHashConfigMock = new Mock<IBuzHashConfig>();
            {
                buzHashConfigMock.Setup(bhc => bhc.Clone())
                    .Returns(new DefaultBuzHashConfig());
            }

            GC.KeepAlive(
                new BuzHash_Implementation(buzHashConfigMock.Object));


            buzHashConfigMock.Verify(bhc => bhc.Clone(), Times.Once);

            buzHashConfigMock.VerifyGet(bhc => bhc.Rtab, Times.Never);
            buzHashConfigMock.VerifyGet(bhc => bhc.HashSizeInBits, Times.Never);
            buzHashConfigMock.VerifyGet(bhc => bhc.Seed, Times.Never);
            buzHashConfigMock.VerifyGet(bhc => bhc.ShiftDirection, Times.Never);
        }

        #region Rtab

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_Rtab_IsNull_Throws()
        {
            var buzHashConfigMock = new Mock<IBuzHashConfig>();
            {
                buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                    .Returns((IReadOnlyList<UInt64>) null);

                buzHashConfigMock.Setup(bhc => bhc.Clone())
                    .Returns(() => buzHashConfigMock.Object);
            }


            Assert.Equal(
                "config.Rtab",
                Assert.Throws<ArgumentException>(
                        () => new BuzHash_Implementation(buzHashConfigMock.Object))
                    .ParamName);
        }


        [Fact]
        public void BuzHash_Implementation_Constructor_Config_Rtab_IsInvalidLength_Throws()
        {
            var invalidLengths = new[] { 0, 1, 16, 32, 64, 128, 255, 257, 512 };

            foreach (var length in invalidLengths)
            {
                var buzHashConfigMock = new Mock<IBuzHashConfig>();
                {
                    buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                        .Returns(new UInt64[length]);

                    buzHashConfigMock.Setup(bhc => bhc.Clone())
                        .Returns(() => buzHashConfigMock.Object);
                }


                Assert.Equal(
                    "config.Rtab",
                    Assert.Throws<ArgumentException>(
                            () => new BuzHash_Implementation(buzHashConfigMock.Object))
                        .ParamName);
            }
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidLengths = new[] { 0, 1, 7, 9, 31, 33, 63, 65, 127, 128, 129 };

            foreach (var length in invalidLengths)
            {
                var buzHashConfigMock = new Mock<IBuzHashConfig>();
                {
                    buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                        .Returns(new UInt64[256]);

                    buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                        .Returns(length);

                    buzHashConfigMock.Setup(bhc => bhc.Clone())
                        .Returns(() => buzHashConfigMock.Object);
                }


                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new BuzHash_Implementation(buzHashConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validLengths = new[] { 8, 16, 32, 64 };

            foreach (var length in validLengths)
            {
                var buzHashConfigMock = new Mock<IBuzHashConfig>();
                {
                    buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                        .Returns(new UInt64[256]);

                    buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                        .Returns(length);

                    buzHashConfigMock.SetupGet(bhc => bhc.ShiftDirection)
                        .Returns(CircularShiftDirection.Left);

                    buzHashConfigMock.Setup(bhc => bhc.Clone())
                        .Returns(() => buzHashConfigMock.Object);
                }


                GC.KeepAlive(
                    new BuzHash_Implementation(buzHashConfigMock.Object));
            }
        }

        #endregion

        #region Seed

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_Seed_IsValid_Works()
        {
            var validValues = new[] { 0UL, 1UL, 65536UL, 0xFFFFFFFFFFFFFFFFUL };

            foreach (var value in validValues)
            {
                var buzHashConfigMock = new Mock<IBuzHashConfig>();
                {
                    buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                        .Returns(new UInt64[256]);

                    buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                        .Returns(64);

                    buzHashConfigMock.SetupGet(bhc => bhc.Seed)
                        .Returns(value);

                    buzHashConfigMock.SetupGet(bhc => bhc.ShiftDirection)
                        .Returns(CircularShiftDirection.Left);

                    buzHashConfigMock.Setup(bhc => bhc.Clone())
                        .Returns(() => buzHashConfigMock.Object);
                }


                GC.KeepAlive(
                    new BuzHash_Implementation(buzHashConfigMock.Object));
            }
        }

        #endregion

        #region ShiftDirection

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_ShiftDirection_IsInvalid_Throws()
        {
            var buzHashConfigMock = new Mock<IBuzHashConfig>();
            {
                buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                    .Returns(new UInt64[256]);

                buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                    .Returns(64);

                buzHashConfigMock.SetupGet(bhc => bhc.ShiftDirection)
                    .Returns((CircularShiftDirection) (-1));

                buzHashConfigMock.Setup(bhc => bhc.Clone())
                    .Returns(() => buzHashConfigMock.Object);
            }


            Assert.Equal(
                "config.ShiftDirection",
                Assert.Throws<ArgumentOutOfRangeException>(
                        () => new BuzHash_Implementation(buzHashConfigMock.Object))
                    .ParamName);
        }

        [Fact]
        public void BuzHash_Implementation_Constructor_Config_ShiftDirection_IsValid_Works()
        {
            var validValues = new[] { CircularShiftDirection.Left, CircularShiftDirection.Right };

            foreach (var value in validValues)
            {
                var buzHashConfigMock = new Mock<IBuzHashConfig>();
                {
                    buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                        .Returns(new UInt64[256]);

                    buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                        .Returns(64);

                    buzHashConfigMock.SetupGet(bhc => bhc.ShiftDirection)
                        .Returns(value);

                    buzHashConfigMock.Setup(bhc => bhc.Clone())
                        .Returns(() => buzHashConfigMock.Object);
                }


                GC.KeepAlive(
                    new BuzHash_Implementation(buzHashConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion

        #region Config

        [Fact]
        public void BuzHash_Implementation_Config_IsCloneOfClone()
        {
            var buzHashConfig3 = Mock.Of<IBuzHashConfig>();
            var buzHashConfig2 = Mock.Of<IBuzHashConfig>(bhc => bhc.Rtab == new UInt64[256] && bhc.HashSizeInBits == 32 && bhc.ShiftDirection == CircularShiftDirection.Left && bhc.Clone() == buzHashConfig3);
            var buzHashConfig = Mock.Of<IBuzHashConfig>(bhc => bhc.Clone() == buzHashConfig2);


            var buzHashHash = new BuzHash_Implementation(buzHashConfig);

            Assert.Equal(buzHashConfig3, buzHashHash.Config);
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void BuzHash_Implementation_HashSizeInBits_MatchesConfig()
        {
            var validHashSizes = new[] { 8, 16, 32, 64 };

            foreach (var hashSize in validHashSizes)
            {
                var buzHashConfigMock = new Mock<IBuzHashConfig>();
                {
                    buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                        .Returns(new UInt64[256]);

                    buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                        .Returns(hashSize);

                    buzHashConfigMock.SetupGet(bhc => bhc.ShiftDirection)
                       .Returns(CircularShiftDirection.Left);

                    buzHashConfigMock.Setup(bhc => bhc.Clone())
                        .Returns(() => buzHashConfigMock.Object);
                }


                var buzHash = new BuzHash_Implementation(buzHashConfigMock.Object);

                Assert.Equal(hashSize, buzHash.HashSizeInBits);
            }
        }

        #endregion

        #region ComputeHash{,Async}Internal

        [Fact]
        public async Task BuzHash_Implementation_ComputeHashInternal_WhenInvalidHashSize_Throws()
        {
            var shouldReturnValidHashSize = true;

            var buzHashConfigMock = new Mock<IBuzHashConfig>();
            {
                buzHashConfigMock.SetupGet(bhc => bhc.HashSizeInBits)
                    .Returns(() => shouldReturnValidHashSize ? 32 : 1);

                buzHashConfigMock.SetupGet(bhc => bhc.Rtab)
                    .Returns(new UInt64[256]);

                buzHashConfigMock.Setup(bhc => bhc.Clone())
                    .Returns(() => buzHashConfigMock.Object);
            }


            var buzHash = new BuzHash_Implementation(buzHashConfigMock.Object);

            shouldReturnValidHashSize = false;

            Assert.Throws<NotImplementedException>(
                () => buzHash.ComputeHash(new byte[0]));

            using (var memoryStream = new MemoryStream(new byte[0]))
            {
                Assert.Throws<NotImplementedException>(
                    () => buzHash.ComputeHash(memoryStream));

                await Assert.ThrowsAsync<NotImplementedException>(
                    () => buzHash.ComputeHashAsync(memoryStream));
            }
        }

        #endregion


        public class IHashFunctionAsync_Tests
            : IStreamableHashFunction_TestBase<IBuzHash>
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
            : IStreamableHashFunction_TestBase<IBuzHash>
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
            : IStreamableHashFunction_TestBase<IBuzHash>
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
            : IStreamableHashFunction_TestBase<IBuzHash>
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
