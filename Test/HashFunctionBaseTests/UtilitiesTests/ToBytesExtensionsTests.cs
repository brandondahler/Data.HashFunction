using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.HashFunctionBaseTests.UtilitiesTests
{
    public class ToBytesExtensionsTests
    {
        [Fact]
        public void ToBytes_IReadOnlyList_UInt32_ComputesCorrectly()
        {
            var testValues = new[] { 2382910298U, 0U, 32483910U, 231398239U };

            var expected = new byte[] {
                0x5A, 0x53, 0x08, 0x8E,
                0x00, 0x00, 0x00, 0x00,
                0x46, 0xaa, 0xef, 0x01,
                0x5f, 0xdb, 0xca, 0x0d
            };

            Assert.Equal(expected, testValues.ToBytes());
        }
    }
}
