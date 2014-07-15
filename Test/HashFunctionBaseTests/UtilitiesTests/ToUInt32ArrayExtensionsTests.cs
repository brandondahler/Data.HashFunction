using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.HashFunctionBaseTests.UtilitiesTests
{
    public class ToUInt32ArrayExtensionsTests
    {
        [Fact]
        public void ToUInt32Array_BigInteger_ComputesCorrectly()
        {
            var testValue = new BigInteger(new byte[] {
                0x5A, 0x53, 0x08, 0x8E,
                0x00, 0x00, 0x00, 0x00,
                0x46, 0xaa, 0xef, 0x01,
                0x5f, 0xdb, 0xca, 0x0d
            });

            var expected = new[] { 2382910298U, 0U, 32483910U, 231398239U };

            Assert.Equal(expected, testValue.ToUInt32Array(128));
        }

        [Fact]
        public void ToUInt32Array_BigInteger_InvalidBitSize_Throws()
        {
            var invalidBitSizes = new[] { -1, 16, 31, 48, 65 };
            var testValue = new BigInteger(0);

            foreach (var invalidBitSize in invalidBitSizes)
            {
                Assert.Equal("bitSize",
                    Assert.Throws<ArgumentOutOfRangeException>(
                        () => testValue.ToUInt32Array(invalidBitSize))
                    .ParamName);
            }
        }

        [Fact]
        public void ToUInt32Array_BigInteger_ValidBitSize_DoesNotThrow()
        {
            var invalidBitSizes = new[] { -1, 16, 31, 48, 65 };
            var testValue = new BigInteger(0);

            foreach (var uint32Count in Enumerable.Range(0, 32).Select(i => i * 32))
            {
                UInt32[] resultArray = null;

                Assert.DoesNotThrow(() =>
                    resultArray = testValue.ToUInt32Array(uint32Count * 32));

                Assert.Equal(uint32Count, resultArray.Length);
            }
        }
    }
}
