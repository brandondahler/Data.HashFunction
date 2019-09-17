using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.CityHash;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.CityHash
{
    public class CityHash_Implementation_Tests
    {
        #region Constructor

        #region  Config

        [Fact]
        public void CityHash_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new CityHash_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void CityHash_Implementation_Constructor_Config_IsCloned()
        {
            var cityHashConfigMock = new Mock<ICityHashConfig>();
            {
                cityHashConfigMock.Setup(bc => bc.Clone())
                    .Returns(new CityHashConfig());
            }

            GC.KeepAlive(
                new CityHash_Implementation(cityHashConfigMock.Object));


            cityHashConfigMock.Verify(bc => bc.Clone(), Times.Once);

            cityHashConfigMock.VerifyGet(bc => bc.HashSizeInBits, Times.Never);
        }

        #endregion

        #region HashSizeInBits
        
        [Fact]
        public void CityHash_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidLengths = new[] { 0, 1, 8, 16, 31, 33, 63, 65, 127, 129, 256 };

            foreach (var length in invalidLengths)
            {
                var cityHashConfigMock = new Mock<ICityHashConfig>();
                {
                    cityHashConfigMock.SetupGet(chc => chc.HashSizeInBits)
                        .Returns(length);

                    cityHashConfigMock.Setup(chc => chc.Clone())
                        .Returns(() => cityHashConfigMock.Object);
                }


                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new CityHash_Implementation(cityHashConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void CityHash_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validLengths = new[] { 32, 64, 128 };

            foreach (var length in validLengths)
            {
                var cityHashConfigMock = new Mock<ICityHashConfig>();
                {
                    cityHashConfigMock.SetupGet(chc => chc.HashSizeInBits)
                        .Returns(length);

                    cityHashConfigMock.Setup(chc => chc.Clone())
                        .Returns(() => cityHashConfigMock.Object);
                }


                GC.KeepAlive(
                    new CityHash_Implementation(cityHashConfigMock.Object));
            }
        }

        #endregion

        #endregion

        #region Config

        [Fact]
        public void CityHash_Implementation_Config_IsCloneOfClone()
        {
            var cityHashConfig3 = Mock.Of<ICityHashConfig>();
            var cityHashConfig2 = Mock.Of<ICityHashConfig>(chc => chc.HashSizeInBits == 32 && chc.Clone() == cityHashConfig3);
            var cityHashConfig = Mock.Of<ICityHashConfig>(chc => chc.Clone() == cityHashConfig2);


            var cityHashHash = new CityHash_Implementation(cityHashConfig);

            Assert.Equal(cityHashConfig3, cityHashHash.Config);
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void CityHash_Implementation_HashSizeInBits_MatchesConfig()
        {
            var validHashSizes = new[] { 32, 64, 128 };

            foreach (var hashSize in validHashSizes)
            {
                var cityHashConfigMock = new Mock<ICityHashConfig>();
                {
                    cityHashConfigMock.SetupGet(chc => chc.HashSizeInBits)
                        .Returns(hashSize);

                    cityHashConfigMock.Setup(chc => chc.Clone())
                        .Returns(() => cityHashConfigMock.Object);
                }


                var cityHash = new CityHash_Implementation(cityHashConfigMock.Object);

                Assert.Equal(hashSize, cityHash.HashSizeInBits);
            }
        }

        #endregion


        public class IHashFunction_Tests
            : IHashFunction_TestBase<ICityHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xc2ebd64e),
                    new KnownValue(32, TestConstants.RandomShort, 0x1fcea779),
                    new KnownValue(32, TestConstants.RandomLong, 0x9dba44d0),
                    new KnownValue(64, TestConstants.Empty, 0x9ae16a3b2f90404f),
                    new KnownValue(64, TestConstants.FooBar, 0xc43fb29ab5effcfe),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x764df1e17d92d1eb),
                    new KnownValue(64, TestConstants.RandomShort, 0x3ef8698eae651b16),
                    new KnownValue(64, TestConstants.RandomLong, 0x39e9fcdba69979b0),
                    new KnownValue(128, TestConstants.Empty, "2b9ac064fc9df03d291ee592c340b53c"),
                    new KnownValue(128, TestConstants.FooBar, "5064c017cf2c1672daa1f13a15b78c98"),
                    new KnownValue(128, TestConstants.LoremIpsum, "31dd5cb57a6c29dc0826565eeb0cf6a4"),
                    new KnownValue(128, TestConstants.RandomShort, "67cbf6f803487e7e09bffce371172c13"),
                    new KnownValue(128, TestConstants.RandomLong, "98999f077f446a5ee962148d86279ea0"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(3), 0xd83c2fa0),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(3), 0xfa10ac780bf932dd),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(3), "8b5529b80301a1c414cc313959fd5255"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa6480aae),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x3a03aa21105c4286),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(23), "658f52c24d66d71d844823de90c3d9ac"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(64), 0x8ace2a1a),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(64), 0x2167be8daa61f94d),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(64), "50e89788e0dbb5d6784fbcbdf57264d1"),
                };

            protected override ICityHash CreateHashFunction(int hashSize) =>
                new CityHash_Implementation(
                    new CityHashConfig() {
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunction_Tests_DefaultConstructor
            : IHashFunction_TestBase<ICityHash>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xc2ebd64e),
                    new KnownValue(32, TestConstants.RandomShort, 0x1fcea779),
                    new KnownValue(32, TestConstants.RandomLong, 0x9dba44d0),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(3), 0xd83c2fa0),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa6480aae),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(64), 0x8ace2a1a),
                };

            protected override ICityHash CreateHashFunction(int hashSize) =>
                new CityHash_Implementation(
                    new CityHashConfig());
        }
    

    }
}
