using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.CRC_Tests
{
    using System.Data.HashFunction;

    public class CRCTests
    {
        [Fact]
        public void CRC_DefaultSettings_Null_Throws()
        {
            Assert.Equal(
                "value",
                Assert.Throws<ArgumentNullException>(() =>
                    CRC.DefaultSettings = null)
                .ParamName);
        }

        [Fact]
        public void CRC_DefaultSettings_Works()
        {
            var testSettings = CRC.Standards[CRC.Standard.CRC24];

            // Ensure we're actually testing something
            Assert.NotEqual(
                testSettings,
                CRC.DefaultSettings);



            CRC.DefaultSettings = testSettings;

            Assert.Equal(
                testSettings,
                CRC.DefaultSettings);


            var crc = new CRC();

            Assert.Equal(
                testSettings,
                crc.Settings);
        }


        [Fact]
        public void CRC_Consturctor_Null_Throws()
        {
            Assert.Equal(
                "settings",
                Assert.Throws<ArgumentNullException>(() => 
                    new CRC(null))
                .ParamName);
        }

        [Fact]
        public void CRC_ComputeHashInternal_InvalidSettings()
        {
            var mockCRC = new Mock<CRC>() { CallBase = true };

            mockCRC.SetupGet(c => c.Settings)
                .Returns((CRC.Setting) null);


            var crc = mockCRC.Object;

            using (var ms = new MemoryStream())
            {
                Assert.Contains("Settings",
                    Assert.Throws<InvalidOperationException>(() =>
                        crc.ComputeHash(ms))
                    .Message);
            }
        }
    }
}
