using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Test._Utilities;
using OpenSource.Data.HashFunction.xxHash;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.xxHash
{
    public class xxHash_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void xxHash_Implementation_Constructor_ValidInputs_Works()
        {
            var xxHashConfigMock = new Mock<IxxHashConfig>();
            {
                xxHashConfigMock.SetupGet(xhc => xhc.HashSizeInBits)
                    .Returns(32);

                xxHashConfigMock.SetupGet(xhc => xhc.Seed)
                    .Returns(0UL);

                xxHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => xxHashConfigMock.Object);
            }

            GC.KeepAlive(
                new xxHash_Implementation(xxHashConfigMock.Object));
        }


        #region Config

        [Fact]
        public void xxHash_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new xxHash_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void xxHash_Implementation_Constructor_Config_IsCloned()
        {
            var xxHashConfigMock = new Mock<IxxHashConfig>();
            {
                xxHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => new xxHashConfig() {
                        HashSizeInBits = 32,
                    });
            }

            GC.KeepAlive(
                new xxHash_Implementation(xxHashConfigMock.Object));


            xxHashConfigMock.Verify(xhc => xhc.Clone(), Times.Once);

            xxHashConfigMock.VerifyGet(xhc => xhc.HashSizeInBits, Times.Never);
            xxHashConfigMock.VerifyGet(xhc => xhc.Seed, Times.Never);
        }
        
        #region HashSizeInBits

        [Fact]
        public void xxHash_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidHashSizes = new[] { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 31, 33, 63, 65, 127, 128, 129, 65535 };

            foreach (var invalidHashSize in invalidHashSizes)
            {
                var xxHashConfigMock = new Mock<IxxHashConfig>();
                {
                    xxHashConfigMock.SetupGet(pc => pc.HashSizeInBits)
                        .Returns(invalidHashSize);

                    xxHashConfigMock.Setup(pc => pc.Clone())
                        .Returns(() => xxHashConfigMock.Object);
                }

                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new xxHash_Implementation(xxHashConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void xxHash_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validHashSizes = new[] { 32, 64 };

            foreach (var validHashSize in validHashSizes)
            {

                var xxHashConfigMock = new Mock<IxxHashConfig>();
                {
                    xxHashConfigMock.SetupGet(pc => pc.HashSizeInBits)
                        .Returns(validHashSize);

                    xxHashConfigMock.Setup(pc => pc.Clone())
                        .Returns(() => xxHashConfigMock.Object);
                }

                GC.KeepAlive(
                    new xxHash_Implementation(xxHashConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion


        #region ComputeHash

        [Fact]
        public void xxHash_Implementation_ComputeHash_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var xxHashConfigMock = new Mock<IxxHashConfig>();
            {
                var readCount = 0;

                xxHashConfigMock.SetupGet(xhc => xhc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                xxHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => xxHashConfigMock.Object);
            }


            var xxHash = new xxHash_Implementation(xxHashConfigMock.Object);

            Assert.Throws<NotImplementedException>(
                () => xxHash.ComputeHash(new byte[1]));
        }

        #endregion

        #region ComputeHashAsync

        [Fact]
        public async Task xxHash_Implementation_ComputeHashAsync_HashSizeInBits_MagicallyInvalid_Throws()
        {
            var xxHashConfigMock = new Mock<IxxHashConfig>();
            {
                var readCount = 0;

                xxHashConfigMock.SetupGet(xhc => xhc.HashSizeInBits)
                    .Returns(() => {
                        readCount += 1;

                        if (readCount == 1)
                            return 32;

                        return 33;
                    });

                xxHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => xxHashConfigMock.Object);
            }


            var xxHash = new xxHash_Implementation(xxHashConfigMock.Object);

            using (var memoryStream = new MemoryStream(new byte[1]))
            {
                await Assert.ThrowsAsync<NotImplementedException>(
                    () => xxHash.ComputeHashAsync(memoryStream));
            }
        }

        #endregion


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
