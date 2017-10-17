using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash1_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void MurmurHash1_Implementation_Constructor_ValidInputs_Works()
        {
            var murmurHash1ConfigMock = new Mock<IMurmurHash1Config>();
            {
                murmurHash1ConfigMock.Setup(mmhc => mmhc.Clone())
                    .Returns(() => murmurHash1ConfigMock.Object);
            }

            GC.KeepAlive(
                new MurmurHash1_Implementation(murmurHash1ConfigMock.Object));
        }


        #region Config

        [Fact]
        public void MurmurHash1_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new MurmurHash1_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void MurmurHash1_Implementation_Constructor_Config_IsCloned()
        {
            var murmurHash1ConfigMock = new Mock<IMurmurHash1Config>();
            {
                murmurHash1ConfigMock.Setup(mmhc => mmhc.Clone())
                    .Returns(() => new MurmurHash1Config());
            }

            GC.KeepAlive(
                new MurmurHash1_Implementation(murmurHash1ConfigMock.Object));


            murmurHash1ConfigMock.Verify(mmhc => mmhc.Clone(), Times.Once);

            murmurHash1ConfigMock.VerifyGet(mmhc => mmhc.Seed, Times.Never);
        }

        #endregion
        
        #endregion


        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IMurmurHash1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x5c45f49b),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0xff3a37aa),
                };

            protected override IMurmurHash1 CreateHashFunction(int hashSize) =>
                new MurmurHash1_Implementation(
                    new MurmurHash1Config());
        }


    }
}
