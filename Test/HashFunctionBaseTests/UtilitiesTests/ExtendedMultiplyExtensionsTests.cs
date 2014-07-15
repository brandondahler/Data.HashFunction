using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.HashFunctionBaseTests.UtilitiesTests
{
    public class ExtendedMultiplyExtensionsTests
    {
        [Fact]
        public void ExtendedMulitply_MultipliesCorrectly()
        {
            var testValues = new[] { 
                0UL, 
                (UInt64) ushort.MaxValue - 1, (UInt64) ushort.MaxValue, (UInt64) ushort.MaxValue + 1,
                (UInt64) uint.MaxValue - 1, (UInt64) uint.MaxValue, (UInt64) uint.MaxValue + 1,
                (UInt64) ulong.MaxValue - 1, (UInt64) ulong.MaxValue,
                3421783489UL, 23891920398UL
            };

            foreach (var operand1 in testValues)
            foreach (var operand2 in testValues)
            {
                var operand1Bytes = new[] { (UInt32)operand1, (UInt32)(operand1 >> 32) };
                var operand2Bytes = new[] { (UInt32)operand2, (UInt32)(operand2 >> 32) };

                var result = operand1Bytes.ExtendedMultiply(operand2Bytes);

                Assert.Equal(operand1 * operand2, ((UInt64)result[1] << 32) | result[0]);
            }
        }

        [Fact]
        public void ExtendedMulitply_ZeroMultiplerProduct_Correct()
        {
            var operand1 = new[] { 1U, 1U, 1U, 0U, 0U };
            var operand2 = new[] { 0U, 1U, 0U, 0U, 0U };

            Assert.Equal(new[] { 0U, 1U, 1U, 1U, 0U },
                operand1.ExtendedMultiply(operand2));
        }

        [Fact]
        public void ExtendedMulitply_UsesLargestOperandLength()
        {
            var smallOperand = new[] { 0U, 0U };
            var largeOperand = new[] { 0U, 0U, 0U, 0U, 0U };

            Assert.Equal(5, smallOperand.ExtendedMultiply(largeOperand).Length);
            Assert.Equal(5, largeOperand.ExtendedMultiply(smallOperand).Length);
        }
    }
}
