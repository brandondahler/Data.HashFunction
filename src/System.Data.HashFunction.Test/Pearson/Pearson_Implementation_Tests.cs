using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Pearson;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Pearson
{
    public class Pearson_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void Pearson_Implementation_Constructor_ValidInputs_Works()
        {
            var pearsonConfigMock = new Mock<IPearsonConfig>();
            {
                pearsonConfigMock.SetupGet(pc => pc.Table)
                    .Returns(
                        Enumerable.Range(0, 256)
                            .Select(i => (byte) i)
                            .ToArray());

                pearsonConfigMock.SetupGet(pc => pc.HashSizeInBits)
                    .Returns(8);

                pearsonConfigMock.Setup(pc => pc.Clone())
                    .Returns(() => pearsonConfigMock.Object);
            }

            GC.KeepAlive(
                new Pearson_Implementation(pearsonConfigMock.Object));
        }


        #region Config

        [Fact]
        public void Pearson_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new Pearson_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void Pearson_Implementation_Constructor_Config_IsCloned()
        {
            var pearsonConfigMock = new Mock<IPearsonConfig>();
            {
                pearsonConfigMock.Setup(pc => pc.Clone())
                    .Returns(() => new WikipediaPearsonConfig() {
                        HashSizeInBits = 8
                    });
            }

            GC.KeepAlive(
                new Pearson_Implementation(pearsonConfigMock.Object));


            pearsonConfigMock.Verify(pc => pc.Clone(), Times.Once);

            pearsonConfigMock.VerifyGet(pc => pc.Table, Times.Never);
            pearsonConfigMock.VerifyGet(pc => pc.HashSizeInBits, Times.Never);
        }

        #region Table

        [Fact]
        public void Pearson_Implementation_Constructor_Config_Table_IsNull_Throws()
        {
            var pearsonConfigMock = new Mock<IPearsonConfig>();
            {
                pearsonConfigMock.SetupGet(pc => pc.Table)
                    .Returns((IReadOnlyList<byte>) null);

                pearsonConfigMock.SetupGet(pc => pc.HashSizeInBits)
                    .Returns(8);

                pearsonConfigMock.Setup(pc => pc.Clone())
                    .Returns(() => pearsonConfigMock.Object);
            }

            Assert.Equal(
                "config.Table",
                Assert.Throws<ArgumentException>(
                        () => new Pearson_Implementation(pearsonConfigMock.Object))
                    .ParamName);
        }

        [Fact]
        public void Pearson_Implementation_Constructor_Config_Table_IsInvalidLength_Throws()
        {
            var invalidLengths = new[] { 1, 64, 128, 255, 257, 512 };

            foreach (var invalidLength in invalidLengths)
            {
                var pearsonConfigMock = new Mock<IPearsonConfig>();
                {
                    pearsonConfigMock.SetupGet(pc => pc.Table)
                        .Returns(
                            Enumerable.Range(0, invalidLength)
                                .Select(i => (byte) (i % 256))
                                .ToArray());

                    pearsonConfigMock.SetupGet(pc => pc.HashSizeInBits)
                        .Returns(8);

                    pearsonConfigMock.Setup(pc => pc.Clone())
                        .Returns(() => pearsonConfigMock.Object);
                }

                Assert.Equal(
                    "config.Table",
                    Assert.Throws<ArgumentException>(
                            () => new Pearson_Implementation(pearsonConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void Pearson_Implementation_Constructor_Config_Table_IsNotDistinct_Throws()
        {
            var pearsonConfigMock = new Mock<IPearsonConfig>();
            {
                pearsonConfigMock.SetupGet(pc => pc.Table)
                    .Returns(
                        Enumerable.Range(0, 255)
                            .Concat(new[] { 0 })
                            .Select(i => (byte) i)
                            .ToArray());

                pearsonConfigMock.SetupGet(pc => pc.HashSizeInBits)
                    .Returns(8);

                pearsonConfigMock.Setup(pc => pc.Clone())
                    .Returns(() => pearsonConfigMock.Object);
            }

            Assert.Equal(
                "config.Table",
                Assert.Throws<ArgumentException>(
                        () => new Pearson_Implementation(pearsonConfigMock.Object))
                    .ParamName);
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void Pearson_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidHashSizes = new[] { -1, 0, 1, 2, 3, 4, 5, 6, 7, 9, 15, 17, 31, 33, 63, 65, 127, 65535 };

            foreach (var invalidHashSize in invalidHashSizes)
            {
                var pearsonConfigMock = new Mock<IPearsonConfig>();
                {
                    pearsonConfigMock.SetupGet(pc => pc.Table)
                        .Returns(
                            Enumerable.Range(0, 256)
                                .Select(i => (byte) i)
                                .ToArray());

                    pearsonConfigMock.SetupGet(pc => pc.HashSizeInBits)
                        .Returns(invalidHashSize);

                    pearsonConfigMock.Setup(pc => pc.Clone())
                        .Returns(() => pearsonConfigMock.Object);
                }

                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new Pearson_Implementation(pearsonConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void Pearson_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validHashSizes = new[] { 8, 16, 24, 32, 64, 128, 256, 65536 };

            foreach (var validHashSize in validHashSizes)
            {
                var pearsonConfigMock = new Mock<IPearsonConfig>();
                {
                    pearsonConfigMock.SetupGet(pc => pc.Table)
                        .Returns(
                            Enumerable.Range(0, 256)
                                .Select(i => (byte) i)
                                .ToArray());

                    pearsonConfigMock.SetupGet(pc => pc.HashSizeInBits)
                        .Returns(validHashSize);

                    pearsonConfigMock.Setup(pc => pc.Clone())
                        .Returns(() => pearsonConfigMock.Object);
                }

                GC.KeepAlive(
                    new Pearson_Implementation(pearsonConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #endregion


        public class IHashFunctionAsync_Tests_WikipediaPearson
            : IHashFunctionAsync_TestBase<IPearson>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0x00),
                    new KnownValue(16, TestConstants.Empty, 0x0000),
                    new KnownValue(24, TestConstants.Empty, 0x000000),
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(40, TestConstants.Empty, 0x0000000000L),
                    new KnownValue(48, TestConstants.Empty, 0x000000000000L),
                    new KnownValue(56, TestConstants.Empty, 0x00000000000000L),
                    new KnownValue(64, TestConstants.Empty, 0x0000000000000000L),
                    new KnownValue(128, TestConstants.Empty, "00000000000000000000000000000000"),
                    new KnownValue(256, TestConstants.Empty, "0000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(512, TestConstants.Empty, "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(1024, TestConstants.Empty, "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(2040, TestConstants.Empty, "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(8, TestConstants.FooBar, 0xac),
                    new KnownValue(16, TestConstants.FooBar, 0xcfac),
                    new KnownValue(24, TestConstants.FooBar, 0xf2cfac),
                    new KnownValue(32, TestConstants.FooBar, 0x36f2cfac),
                    new KnownValue(40, TestConstants.FooBar, 0x0136f2cfac),
                    new KnownValue(48, TestConstants.FooBar, 0xc20136f2cfac),
                    new KnownValue(56, TestConstants.FooBar, 0xa3c20136f2cfac),
                    new KnownValue(64, TestConstants.FooBar, 0xdda3c20136f2cfac),
                    new KnownValue(128, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f"),
                    new KnownValue(256, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4"),
                    new KnownValue(512, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4b4e9695a04db341c64d53723b1dcb631e5de72b2626c3d4ad3c1df1d3c89f0f5"),
                    new KnownValue(1024, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4b4e9695a04db341c64d53723b1dcb631e5de72b2626c3d4ad3c1df1d3c89f0f5dae0befd498428c465d6a6186a1f51d492a2edf44ea12d2774f95e1798b8588f0bf1e1a082a7ca85ae55593a078b60d9914b6f4353e87341766824b097b92996"),
                    new KnownValue(2040, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4b4e9695a04db341c64d53723b1dcb631e5de72b2626c3d4ad3c1df1d3c89f0f5dae0befd498428c465d6a6186a1f51d492a2edf44ea12d2774f95e1798b8588f0bf1e1a082a7ca85ae55593a078b60d9914b6f4353e87341766824b097b92996149dbf20d8af8eec56619afb19e3005f471e7c25c80a939f3b1bba5cbcb77eaa7a8a83d0e7992112eab5bdc605775433882bd77fcbabc7a52c0d70eb3938661a098d7b5081fc4445b371c5ef9e0ce602782a1506cefa0fa9164d7dcd86030e9075f62611ccd22ef8081379a863ff30bb6e104f2f229487f357406b463e485b"),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x92),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x9f92),
                    new KnownValue(24, TestConstants.LoremIpsum, 0xba9f92),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x73ba9f92),
                    new KnownValue(40, TestConstants.LoremIpsum, 0x1073ba9f92),
                    new KnownValue(48, TestConstants.LoremIpsum, 0xc91073ba9f92),
                    new KnownValue(56, TestConstants.LoremIpsum, 0x58c91073ba9f92),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xdf58c91073ba9f92),
                    new KnownValue(128, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c"),
                    new KnownValue(256, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e3"),
                    new KnownValue(512, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e343b9ea988152abace8a54d1b784828674199a2b1e5324779868012694c2fda44"),
                    new KnownValue(1024, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e343b9ea988152abace8a54d1b784828674199a2b1e5324779868012694c2fda4416de2ad7a76c4f19043e62087ec00230ccc53be4661c9eb0944bca3a886af26bb5fed0e714bd978f3cd489a9d1574a45c1d2bf7deb27c7617b9359ef9d9631a8"),
                    new KnownValue(2040, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e343b9ea988152abace8a54d1b784828674199a2b1e5324779868012694c2fda4416de2ad7a76c4f19043e62087ec00230ccc53be4661c9eb0944bca3a886af26bb5fed0e714bd978f3cd489a9d1574a45c1d2bf7deb27c7617b9359ef9d9631a877182d83cb03ff251ef6132cf7503dd811266d538535d60639c8f37fe164faa69024c6ada10e2334cd0c4936fc420fe00154918bf0c24007f1b3b6705c56156fd355eed900fb097460711de29a461ac45e9b22d5e68d0bafa3c3955d68823fec205b21b8b2dca0cf5f65aa29637a2e0dce8a3772bea4b46e4ebc17b7f93833"),
                };

            protected override IPearson CreateHashFunction(int hashSize) =>
                new Pearson_Implementation(
                    new WikipediaPearsonConfig() {
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_WikipediaPearson_DefaultConstructor
            : IHashFunctionAsync_TestBase<IPearson>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0x00),
                    new KnownValue(8, TestConstants.FooBar, 0xac),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x92),
                };

            protected override IPearson CreateHashFunction(int hashSize) =>
                new Pearson_Implementation(
                    new WikipediaPearsonConfig());
        }
    
    }
}
