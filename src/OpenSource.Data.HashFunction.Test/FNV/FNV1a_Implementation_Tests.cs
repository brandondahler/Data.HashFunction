using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FNV;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Numerics;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FNV
{
    public class FNV1a_Implementation_Tests
    {
        
        #region Constructor

        [Fact]
        public void FNV1a_Implementation_Constructor_ValidInputs_Work()
        {
            var fnvConfigMock = new Mock<IFNVConfig>();
            {
                fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                    .Returns(32);

                fnvConfigMock.SetupGet(fc => fc.Prime)
                    .Returns(BigInteger.One);

                fnvConfigMock.SetupGet(fc => fc.Offset)
                    .Returns(BigInteger.One);

                fnvConfigMock.Setup(fc => fc.Clone())
                    .Returns(() => fnvConfigMock.Object);
            }


            GC.KeepAlive(
                new FNV1a_Implementation(fnvConfigMock.Object));
        }


        #region Config

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new FNV1a_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_IsCloned()
        {
            var fnvConfigMock = new Mock<IFNVConfig>();
            {
                fnvConfigMock.Setup(fc => fc.Clone())
                    .Returns(
                        new FNVConfig() {
                            HashSizeInBits = 32,
                            Prime = BigInteger.One,
                            Offset = BigInteger.One
                        });
            }

            GC.KeepAlive(
                new FNV1a_Implementation(fnvConfigMock.Object));


            fnvConfigMock.Verify(fc => fc.Clone(), Times.Once);

            fnvConfigMock.VerifyGet(fc => fc.HashSizeInBits, Times.Never);
            fnvConfigMock.VerifyGet(fc => fc.Prime, Times.Never);
            fnvConfigMock.VerifyGet(fc => fc.Offset, Times.Never);
        }

        #region HashSizeInBits

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidLengths = new[] { -1, 0, 31, 33, 63, 65, 127, 129 };

            foreach (var length in invalidLengths)
            {
                var fnvConfigMock = new Mock<IFNVConfig>();
                {
                    fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                        .Returns(length);

                    fnvConfigMock.SetupGet(fc => fc.Prime)
                        .Returns(BigInteger.One);

                    fnvConfigMock.SetupGet(fc => fc.Offset)
                        .Returns(BigInteger.One);

                    fnvConfigMock.Setup(fc => fc.Clone())
                        .Returns(() => fnvConfigMock.Object);
                }


                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new FNV1a_Implementation(fnvConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validLengths = Enumerable.Range(1, 1024).Select(i => i * 32);

            foreach (var length in validLengths)
            {
                var fnvConfigMock = new Mock<IFNVConfig>();
                {
                    fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                        .Returns(length);

                    fnvConfigMock.SetupGet(fc => fc.Prime)
                        .Returns(BigInteger.One);

                    fnvConfigMock.SetupGet(fc => fc.Offset)
                        .Returns(BigInteger.One);

                    fnvConfigMock.Setup(fc => fc.Clone())
                        .Returns(() => fnvConfigMock.Object);
                }


                GC.KeepAlive(
                    new FNV1a_Implementation(fnvConfigMock.Object));
            }
        }

        #endregion

        #region Prime

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_Prime_IsInvalid_Throws()
        {
            var invalidPrimes = new[] { new BigInteger(-256), new BigInteger(-1), BigInteger.Zero };

            foreach (var prime in invalidPrimes)
            {
                var fnvConfigMock = new Mock<IFNVConfig>();
                {
                    fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                        .Returns(32);

                    fnvConfigMock.SetupGet(fc => fc.Prime)
                        .Returns(prime);

                    fnvConfigMock.SetupGet(fc => fc.Offset)
                        .Returns(BigInteger.One);

                    fnvConfigMock.Setup(fc => fc.Clone())
                        .Returns(() => fnvConfigMock.Object);
                }


                Assert.Equal(
                    "config.Prime",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new FNV1a_Implementation(fnvConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_Prime_IsValid_Works()
        {
            var validPrimes = new[] { 1, 2, 8, 64, 256, 65536, 1073741824 };

            foreach (var prime in validPrimes)
            {
                var fnvConfigMock = new Mock<IFNVConfig>();
                {
                    fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                        .Returns(32);

                    fnvConfigMock.SetupGet(fc => fc.Prime)
                        .Returns(prime);

                    fnvConfigMock.SetupGet(fc => fc.Offset)
                        .Returns(BigInteger.One);

                    fnvConfigMock.Setup(fc => fc.Clone())
                        .Returns(() => fnvConfigMock.Object);
                }


                GC.KeepAlive(
                    new FNV1a_Implementation(fnvConfigMock.Object));
            }
        }

        #endregion

        #region Offset

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_Offset_IsInvalid_Throws()
        {
            var invalidOffsets = new[] { new BigInteger(-256), new BigInteger(-1), BigInteger.Zero };

            foreach (var offset in invalidOffsets)
            {
                var fnvConfigMock = new Mock<IFNVConfig>();
                {
                    fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                        .Returns(32);

                    fnvConfigMock.SetupGet(fc => fc.Prime)
                        .Returns(BigInteger.One);

                    fnvConfigMock.SetupGet(fc => fc.Offset)
                        .Returns(offset);

                    fnvConfigMock.Setup(fc => fc.Clone())
                        .Returns(() => fnvConfigMock.Object);
                }


                Assert.Equal(
                    "config.Offset",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new FNV1a_Implementation(fnvConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void FNV1a_Implementation_Constructor_Config_Offset_IsValid_Works()
        {
            var validOffsets = new[] { 1, 2, 8, 64, 256, 65536, 1073741824 };

            foreach (var offset in validOffsets)
            {
                var fnvConfigMock = new Mock<IFNVConfig>();
                {
                    fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                        .Returns(32);

                    fnvConfigMock.SetupGet(fc => fc.Prime)
                        .Returns(BigInteger.One);

                    fnvConfigMock.SetupGet(fc => fc.Offset)
                        .Returns(offset);

                    fnvConfigMock.Setup(fc => fc.Clone())
                        .Returns(() => fnvConfigMock.Object);
                }


                GC.KeepAlive(
                    new FNV1a_Implementation(fnvConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion

        #region Config

        [Fact]
        public void FNV1a_Implementation_Config_IsCloneOfClone()
        {
            var fnvConfig3 = Mock.Of<IFNVConfig>();
            var fnvConfig2 = Mock.Of<IFNVConfig>(fc => fc.HashSizeInBits == 32 && fc.Prime == BigInteger.One && fc.Offset == BigInteger.One && fc.Clone() == fnvConfig3);
            var fnvConfig = Mock.Of<IFNVConfig>(fc => fc.Clone() == fnvConfig2);


            var fnv1aHash = new FNV1a_Implementation(fnvConfig);

            Assert.Equal(fnvConfig3, fnv1aHash.Config);
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void FNV1a_Implementation_HashSizeInBits_MatchesConfig()
        {
            var validHashSizes = Enumerable.Range(1, 1024).Select(i => i * 32);

            foreach (var hashSize in validHashSizes)
            {
                var fnvConfigMock = new Mock<IFNVConfig>();
                {
                    fnvConfigMock.SetupGet(fc => fc.HashSizeInBits)
                        .Returns(hashSize);

                    fnvConfigMock.SetupGet(fc => fc.Prime)
                        .Returns(BigInteger.One);

                    fnvConfigMock.SetupGet(fc => fc.Offset)
                        .Returns(BigInteger.One);

                    fnvConfigMock.Setup(fc => fc.Clone())
                        .Returns(() => fnvConfigMock.Object);
                }


                var fnv1a = new FNV1a_Implementation(fnvConfigMock.Object);

                Assert.Equal(hashSize, fnv1a.HashSizeInBits);
            }
        }

        #endregion


        public class IStreamableHashFunction_Tests
            : IStreamableHashFunction_TestBase<IFNV1a>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x811c9dc5),
                    new KnownValue(32, TestConstants.FooBar, 0xbf9cf968),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x15cc3fdb),
                    new KnownValue(64, TestConstants.Empty, 0xcbf29ce484222325),
                    new KnownValue(64, TestConstants.FooBar, 0x85944171f73967e8),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xd9daf73d6af17cfb),
                    new KnownValue(128, TestConstants.Empty, "8dc595627521b8624201bb072e27626c"),
                    new KnownValue(128, TestConstants.FooBar, "186f44ba97350d6fbf643c7962163e34"),
                    new KnownValue(128, TestConstants.LoremIpsum, "b3db4ee71f492ed1c2166a4bccdce8b6"),
                };

            protected override IFNV1a CreateHashFunction(int hashSize) =>
                new FNV1a_Implementation(
                    FNVConfig.GetPredefinedConfig(hashSize));
        }
    }
}
