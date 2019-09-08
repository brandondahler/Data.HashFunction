using Moq;
using MoreLinq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.SpookyHash;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

#pragma warning disable CS0618 // SpookyHashV1_Implementation' is obsolete: 'SpookyHashV1 has known issues, use SpookyHashV2.'

namespace OpenSource.Data.HashFunction.Test.SpookyHash
{
    public class SpookyHashV1_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void SpookyHashV1_Implementation_Constructor_ValidInputs_Works()
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
                new SpookyHashV1_Implementation(spookyHashConfigMock.Object));
        }


        #region Config

        [Fact]
        public void SpookyHashV1_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new SpookyHashV1_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void SpookyHashV1_Implementation_Constructor_Config_IsCloned()
        {
            var spookyHashConfigMock = new Mock<ISpookyHashConfig>();
            {
                spookyHashConfigMock.Setup(xhc => xhc.Clone())
                    .Returns(() => new SpookyHashConfig() {
                        HashSizeInBits = 32,
                    });
            }

            GC.KeepAlive(
                new SpookyHashV1_Implementation(spookyHashConfigMock.Object));


            spookyHashConfigMock.Verify(xhc => xhc.Clone(), Times.Once);

            spookyHashConfigMock.VerifyGet(xhc => xhc.HashSizeInBits, Times.Never);
            spookyHashConfigMock.VerifyGet(xhc => xhc.Seed, Times.Never);
            spookyHashConfigMock.VerifyGet(xhc => xhc.Seed2, Times.Never);
        }

        #region HashSizeInBits

        [Fact]
        public void SpookyHashV1_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
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
                            () => new SpookyHashV1_Implementation(spookyHashConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void SpookyHashV1_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
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
                    new SpookyHashV1_Implementation(spookyHashConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion


        #region ComputeHash

        [Fact]
        public void SpookyHashV1_Implementation_ComputeHash_HashSizeInBits_MagicallyInvalid_Throws()
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


            var spookyHashV1 = new SpookyHashV1_Implementation(spookyHashConfigMock.Object);
            
            Assert.Throws<NotImplementedException>(
                () => spookyHashV1.ComputeHash(new byte[1]));

            Assert.Throws<NotImplementedException>(
                () => spookyHashV1.ComputeHash(new byte[200]));
        }

        #endregion

        #region ComputeHashAsync

        [Fact]
        public async Task SpookyHashV1_Implementation_ComputeHashAsync_HashSizeInBits_MagicallyInvalid_Throws()
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


            var spookyHashV1 = new SpookyHashV1_Implementation(spookyHashConfigMock.Object);

            using (var memoryStream = new MemoryStream(new byte[1]))
            {
                await Assert.ThrowsAsync<NotImplementedException>(
                    () => spookyHashV1.ComputeHashAsync(memoryStream));
            }

            using (var memoryStream = new MemoryStream(new byte[200]))
            {
                await Assert.ThrowsAsync<NotImplementedException>(
                    () => spookyHashV1.ComputeHashAsync(memoryStream));
            }
        }

        #endregion


        public class IHashFunctionAsync_Tests_SpookyHashV1
            : IStreamableHashFunction_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xeade5f81),
                    new KnownValue(64, TestConstants.FooBar, 0xf998152deade5f81),
                    new KnownValue(128, TestConstants.FooBar, "0x7346a4738d6e73c1f998152deade5f81"),

                    new KnownValue(32, TestConstants.LoremIpsum.Take(32), 0x16720302),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(32), 0x17736e9f16720302),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(32), "0x34ab0a4fc2b7c38b17736e9f16720302"),

                    new KnownValue(32, TestConstants.LoremIpsum, 0xa63cfe42),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x24e936e7a63cfe42),
                    new KnownValue(128, TestConstants.LoremIpsum, "0x71bed3339591abd424e936e7a63cfe42"),

                    new KnownValue(32, TestConstants.LoremIpsum.Repeat(2).Take(192), 0xdb3e967e),
                    new KnownValue(64, TestConstants.LoremIpsum.Repeat(2).Take(192), 0x893195cbdb3e967e),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2).Take(192), "0x786ba1f4af67dd09893195cbdb3e967e"),

                    new KnownValue(32, TestConstants.LoremIpsum.Repeat(2), 0xbfc4d1ba),
                    new KnownValue(64, TestConstants.LoremIpsum.Repeat(2), 0x81b8c9d9bfc4d1ba),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2), "0x74f5bc2ea0c3f5fa81b8c9d9bfc4d1ba"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig() { 
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV1_DefaultConstructor
            : IStreamableHashFunction_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "0x7346a4738d6e73c1f998152deade5f81"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(32), "0x34ab0a4fc2b7c38b17736e9f16720302"),
                    new KnownValue(128, TestConstants.LoremIpsum, "0x71bed3339591abd424e936e7a63cfe42"),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2).Take(192), "0x786ba1f4af67dd09893195cbdb3e967e"),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2), "0x74f5bc2ea0c3f5fa81b8c9d9bfc4d1ba"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV1_WithInitVals
            : IStreamableHashFunction_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xf17a1610),
                    new KnownValue(64, TestConstants.FooBar, 0x6d9f61e13758729f),
                    new KnownValue(128, TestConstants.FooBar, "0xb75012e143d73c2f130a88c31545d9eb"),

                    new KnownValue(32, TestConstants.LoremIpsum.Take(32), 0xdfea73f6),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(32), 0x79f5b1436aa8c030),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(32), "0x808dcc692499aab6d4df426610efb171"),

                    new KnownValue(32, TestConstants.LoremIpsum, 0x781c0698),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x6850697d7c6fd140),
                    new KnownValue(128, TestConstants.LoremIpsum, "0x69fe25ed1c58a3c41af2d85f5c123c33"),

                    new KnownValue(32, TestConstants.LoremIpsum.Repeat(2).Take(192), 0x2fe8c893),
                    new KnownValue(64, TestConstants.LoremIpsum.Repeat(2).Take(192), 0x224f33b61ce67217),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2).Take(192), "0xf541594c2392a36c2de991720ceeb287"),

                    new KnownValue(32, TestConstants.LoremIpsum.Repeat(2), 0x4888ba9f),
                    new KnownValue(64, TestConstants.LoremIpsum.Repeat(2), 0xf62407f12a8bc7ee),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2), "0x99c24b1636a379f784888337ee023e52"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig() {
                        HashSizeInBits = hashSize,
                        Seed = 0x7da236b987930b75U,
                        Seed2 = 0x2eb994a3851d2f54U
                    });
        }
    

        public class IHashFunctionAsync_Tests_SpookyHashV1_WithInitVals_DefaultHashSize
            : IStreamableHashFunction_TestBase<ISpookyHashV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "0xb75012e143d73c2f130a88c31545d9eb"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(32), "0x808dcc692499aab6d4df426610efb171"),
                    new KnownValue(128, TestConstants.LoremIpsum, "0x69fe25ed1c58a3c41af2d85f5c123c33"),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2).Take(192), "0xf541594c2392a36c2de991720ceeb287"),
                    new KnownValue(128, TestConstants.LoremIpsum.Repeat(2), "0x99c24b1636a379f784888337ee023e52"),
                };

            protected override ISpookyHashV1 CreateHashFunction(int hashSize) =>
                new SpookyHashV1_Implementation(
                    new SpookyHashConfig() {
                        Seed = 0x7da236b987930b75U,
                        Seed2 = 0x2eb994a3851d2f54U
                    });
        }
    
    }

}

#pragma warning restore CS0618 // SpookyHashV1_Implementation' is obsolete: 'SpookyHashV1 has known issues, use SpookyHashV2.'
