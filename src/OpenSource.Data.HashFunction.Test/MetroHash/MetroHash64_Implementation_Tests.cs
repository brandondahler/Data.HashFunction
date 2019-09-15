using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.MetroHash;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.MetroHash
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
            : IStreamableHashFunction_TestBase<IMetroHash64>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "012345678901234567890123456789012345678901234567890123456789012", "6b753dae06704bad"),

                    new KnownValue(64, TestConstants.Empty, 0x705fb008071e967d),
                    new KnownValue(64, TestConstants.FooBar, 0xafdc1105b8a90a61),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xf2083d32ac311dab),
                };

            protected override IMetroHash64 CreateHashFunction(int hashSize) =>
                new MetroHash64_Implementation(
                    new MetroHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_WithSeed
            : IStreamableHashFunction_TestBase<IMetroHash64>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "012345678901234567890123456789012345678901234567890123456789012", "3b0d481cf4b9b8df"),

                    new KnownValue(64, TestConstants.Empty, 0xe6f660fe36b85a05),
                    new KnownValue(64, TestConstants.FooBar, 0xa4e1647f495bd189),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x74f8c5ccdd69b4b3),
                };

            protected override IMetroHash64 CreateHashFunction(int hashSize) =>
                new MetroHash64_Implementation(
                    new MetroHashConfig() {
                        Seed = 1
                    });
        }
    

    }
}
