using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.IntegerManipulation
{
    public class RotateRightExtensionsTests
    {
        [Fact]
        public void RotateRight_byte_RotatesCorrectly()
        {
            var operand = (byte) 198;

            Assert.Equal((byte) 108, operand.RotateRight(4));
        }

        [Fact]
        public void RotateRight_UInt16_RotatesCorrectly()
        {
            var operand = (UInt16) 38291;

            Assert.Equal((UInt16) 14681, operand.RotateRight(4));
        }

        [Fact]
        public void RotateRight_UInt32_RotatesCorrectly()
        {
            var operand = 2916644410U;

            Assert.Equal(2866644835U, operand.RotateRight(4));
        }

        [Fact]
        public void RotateRight_UInt64_RotatesCorrectly()
        {
            var operand = 3421843292831082394UL;

            Assert.Equal(11743080251870412409UL, operand.RotateRight(4));
        }
    }
}
