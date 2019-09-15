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
    public class MetroHash128_Implementation_Tests
    {
        #region Constructor

        #region  Config

        [Fact]
        public void MetroHash128_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new MetroHash128_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void MetroHash128_Implementation_Constructor_Config_IsCloned()
        {
            var metroHashConfigMock = new Mock<IMetroHashConfig>();
            {
                metroHashConfigMock.Setup(bc => bc.Clone())
                    .Returns(new MetroHashConfig());
            }

            GC.KeepAlive(
                new MetroHash128_Implementation(metroHashConfigMock.Object));


            metroHashConfigMock.Verify(bc => bc.Clone(), Times.Once);

            metroHashConfigMock.VerifyGet(bc => bc.Seed, Times.Never);
        }

        #endregion
        
        #endregion

        #region Config

        [Fact]
        public void MetroHash128_Implementation_Config_IsCloneOfClone()
        {
            var metroHashConfig3 = Mock.Of<IMetroHashConfig>();
            var metroHashConfig2 = Mock.Of<IMetroHashConfig>(mhc => mhc.Clone() == metroHashConfig3);
            var metroHashConfig = Mock.Of<IMetroHashConfig>(mhc => mhc.Clone() == metroHashConfig2);


            var metroHash = new MetroHash128_Implementation(metroHashConfig);

            Assert.Equal(metroHashConfig3, metroHash.Config);
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void MetroHash128_Implementation_HashSizeInBits_Is64()
        {
            var metroHashConfig = Mock.Of<IMetroHashConfig>(mhc => mhc.Clone() == mhc);
            var metroHash = new MetroHash128_Implementation(metroHashConfig);

            Assert.Equal(128, metroHash.HashSizeInBits);
        }

        #endregion
        
        public class IHashFunctionAsync_Tests
            : IStreamableHashFunction_TestBase<IMetroHash128>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, "012345678901234567890123456789012345678901234567890123456789012", "c77ce2bfa4ed9f9b0548b2ac5074a297"),

                    new KnownValue(128, TestConstants.Empty,      "0x4606b14684c65fb60005f3ca3d41d1cb"),
                    new KnownValue(128, TestConstants.FooBar,     "0xe5fe590c9b99c223859bf8992882a5e3"),
                    new KnownValue(128, TestConstants.LoremIpsum, "0x52c5e338fb7a400666e9fbabaebcb790"),
                };

            protected override IMetroHash128 CreateHashFunction(int hashSize) =>
                new MetroHash128_Implementation(
                    new MetroHashConfig());
        }
    

        public class IHashFunctionAsync_Tests_WithSeed
            : IStreamableHashFunction_TestBase<IMetroHash128>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, "012345678901234567890123456789012345678901234567890123456789012", "45a3cdb838199d7fbdd68d867a14ecef"),

                    new KnownValue(128, TestConstants.Empty,      "0xf9a908797eef84017d036b44fbede600"),
                    new KnownValue(128, TestConstants.FooBar,     "0x52ff94470b31e45dfcf0cc865889f0df"),
                    new KnownValue(128, TestConstants.LoremIpsum, "0x7786153ea37fe00904733ec964eaeb7c"),
                };

            protected override IMetroHash128 CreateHashFunction(int hashSize) =>
                new MetroHash128_Implementation(
                    new MetroHashConfig() {
                        Seed = 1
                    });
        }
    

    }
}
