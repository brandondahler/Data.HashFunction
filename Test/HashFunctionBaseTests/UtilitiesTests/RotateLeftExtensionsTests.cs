using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.HashFunctionBaseTests.UtilitiesTests
{
    public class RotateLeftExtensionsTests
    {
        [Fact]
        public void RotateLeft_byte_RotatesCorrectly()
        {
            var operand = (byte) 198;

            Assert.Equal((byte) 108, operand.RotateLeft(4));
        }

        [Fact]
        public void RotateLeft_UInt16_RotatesCorrectly()
        {
            var operand = (UInt16) 38291;

            Assert.Equal((UInt16) 22841, operand.RotateLeft(4));
        }

        [Fact]
        public void RotateLeft_UInt32_RotatesCorrectly()
        {
            var operand = 2916644410U;

            Assert.Equal(3716637610U, operand.RotateLeft(4));
        }

        [Fact]
        public void RotateLeft_UInt64_RotatesCorrectly()
        {
            var operand = 3421843292831082394UL;
            
            Assert.Equal(17856004537878215074UL, operand.RotateLeft(4));
        }
    }
}
