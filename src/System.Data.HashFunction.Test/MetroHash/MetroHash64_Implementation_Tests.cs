using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.MetroHash;
using System.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.MetroHash
{
    public class MetroHash64_Implementation_Tests
    {
        #region Constructor

        #region  Config

        [Fact]
        public void MetroHash64_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new MetroHash64_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void MetroHash64_Implementation_Constructor_Config_IsCloned()
        {
            var metroHashConfigMock = new Mock<IMetroHashConfig>();
            {
                metroHashConfigMock.Setup(bc => bc.Clone())
                    .Returns(new MetroHashConfig());
            }

            GC.KeepAlive(
                new MetroHash64_Implementation(metroHashConfigMock.Object));


            metroHashConfigMock.Verify(bc => bc.Clone(), Times.Once);

            metroHashConfigMock.VerifyGet(bc => bc.Seed, Times.Never);
        }

        #endregion
        
        #endregion

        #region Config

        [Fact]
        public void MetroHash64_Implementation_Config_IsCloneOfClone()
        {
            var metroHashConfig3 = Mock.Of<IMetroHashConfig>();
            var metroHashConfig2 = Mock.Of<IMetroHashConfig>(mhc => mhc.Clone() == metroHashConfig3);
            var metroHashConfig = Mock.Of<IMetroHashConfig>(mhc => mhc.Clone() == metroHashConfig2);


            var metroHash = new MetroHash64_Implementation(metroHashConfig);

            Assert.Equal(metroHashConfig3, metroHash.Config);
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void MetroHash64_Implementation_HashSizeInBits_Is64()
        {
            var metroHashConfig = Mock.Of<IMetroHashConfig>(mhc => mhc.Clone() == mhc);
            var metroHash = new MetroHash64_Implementation(metroHashConfig);

            Assert.Equal(64, metroHash.HashSizeInBits);
        }

        #endregion
        
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IMetroHash64>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "012345678901234567890123456789012345678901234567890123456789012", "6b753dae06704bad"),

                    //new KnownValue(64, TestConstants.Empty, 0x9ae16a3b2f90404f),
                    //new KnownValue(64, TestConstants.FooBar, 0xc43fb29ab5effcfe),
                    //new KnownValue(64, TestConstants.LoremIpsum, 0x764df1e17d92d1eb),
                    //new KnownValue(64, TestConstants.RandomShort, 0x3ef8698eae651b16),
                    //new KnownValue(64, TestConstants.RandomLong, 0x39e9fcdba69979b0),
                };

            protected override IMetroHash64 CreateHashFunction(int hashSize) =>
                new MetroHash64_Implementation(
                    new MetroHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_WithSeed
            : IHashFunctionAsync_TestBase<IMetroHash64>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "012345678901234567890123456789012345678901234567890123456789012", "3b0d481cf4b9b8df"),

                    //new KnownValue(64, TestConstants.Empty, 0xdc56d17a),
                    //new KnownValue(64, TestConstants.FooBar, 0xe2f34cdf),
                    //new KnownValue(64, TestConstants.LoremIpsum, 0xc2ebd64e),
                    //new KnownValue(64, TestConstants.RandomShort, 0x1fcea779),
                    //new KnownValue(64, TestConstants.RandomLong, 0x9dba44d0),
                };

            protected override IMetroHash64 CreateHashFunction(int hashSize) =>
                new MetroHash64_Implementation(
                    new MetroHashConfig() {
                        Seed = 1
                    });
        }
    

    }
}
