using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.IntegerManipulation
{
    public class ReverseByteOrderExtensionsTests
    {
        [Fact]
        public void ReverseBytes_UInt16_ReversesCorrectly()
        {
            var operand = (UInt16) 39280;

            var expected = BitConverter.ToUInt16(
                BitConverter.GetBytes(operand)
                    .Reverse()
                    .ToArray(),
                0);

            Assert.Equal(expected, operand.ReverseByteOrder());
        }

        [Fact]
        public void ReverseBytes_UInt32_ReversesCorrectly()
        {
            var operand = 328183279U;

            var expected = BitConverter.ToUInt32(
                BitConverter.GetBytes(operand)
                    .Reverse()
                    .ToArray(),
                0);

            Assert.Equal(expected, operand.ReverseByteOrder());
        }

        [Fact]
        public void ReverseBytes_UInt64_ReversesCorrectly()
        {
            var operand = 213480921384301209UL;

            var expected = BitConverter.ToUInt64(
                BitConverter.GetBytes(operand)
                    .Reverse()
                    .ToArray(),
                0);

            Assert.Equal(expected, operand.ReverseByteOrder());
        }
    }
}
