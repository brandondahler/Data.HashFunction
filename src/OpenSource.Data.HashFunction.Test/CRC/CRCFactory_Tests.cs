using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.CRC;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.CRC
{
    public class CRCFactory_Tests
    {
        [Fact]
        public void CRCFactory_Instance_IsDefined()
        {
            Assert.NotNull(CRCFactory.Instance);
            Assert.IsType<CRCFactory>(CRCFactory.Instance);
        }

        [Fact]
        public void CRCFactory_Create_Works()
        {
            var defaultCRCConfig = CRCConfig.CRC32;

            var crcFactory = CRCFactory.Instance;
            var crc = crcFactory.Create();

            Assert.NotNull(crc);
            Assert.IsType<CRC_Implementation>(crc);


            var resultingCRCConfig = crc.Config;

            Assert.Equal(defaultCRCConfig.HashSizeInBits, resultingCRCConfig.HashSizeInBits);
            Assert.Equal(defaultCRCConfig.InitialValue, resultingCRCConfig.InitialValue);
            Assert.Equal(defaultCRCConfig.Polynomial, resultingCRCConfig.Polynomial);
            Assert.Equal(defaultCRCConfig.ReflectIn, resultingCRCConfig.ReflectIn);
            Assert.Equal(defaultCRCConfig.ReflectOut, resultingCRCConfig.ReflectOut);
            Assert.Equal(defaultCRCConfig.XOrOut, resultingCRCConfig.XOrOut);
        }


        [Fact]
        public void CRCFactory_Create_Config_IsNull_Throws()
        {
            var crcFactory = CRCFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => crcFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void CRCFactory_Create_Config_Works()
        {
            var crcConfig = CRCConfig.CRC64;

            var crcFactory = CRCFactory.Instance;
            var crc = crcFactory.Create(crcConfig);

            Assert.NotNull(crc);
            Assert.IsType<CRC_Implementation>(crc);


            var resultingCRCConfig = crc.Config;


            Assert.Equal(crcConfig.HashSizeInBits, resultingCRCConfig.HashSizeInBits);
            Assert.Equal(crcConfig.InitialValue, resultingCRCConfig.InitialValue);
            Assert.Equal(crcConfig.Polynomial, resultingCRCConfig.Polynomial);
            Assert.Equal(crcConfig.ReflectIn, resultingCRCConfig.ReflectIn);
            Assert.Equal(crcConfig.ReflectOut, resultingCRCConfig.ReflectOut);
            Assert.Equal(crcConfig.XOrOut, resultingCRCConfig.XOrOut);
        }
    }
}
