using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.CRC
{
    using System.Data.HashFunction;

    public class CRCTests
    {
        [Fact]
        public void CRC_ComputeHashInternal_InvalidSettings()
        {
            var mockCRC = new Mock<CRC>() { CallBase = true };

            mockCRC.SetupGet(c => c.Settings)
                .Returns((CRCSettings) null);


            var crc = mockCRC.Object;

            using (var ms = new MemoryStream())
            {
                Assert.Equal("Settings",
                    Assert.Throws<ArgumentNullException>(() =>
                        crc.ComputeHash(ms))
                    .ParamName);
            }
        }
    }
}
