using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.IntegerManipulation
{
    using System.Data.HashFunction.Utilities.IntegerManipulation;

    public class ReflectBitsExtensionsTests
    {
        [Fact]
        public void ReflectBits_Byte_InvalidBitLength_Throws()
        {
            byte value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 9, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ReflectBits(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ReflectBits_Byte_Works()
        {
            byte value = 0xcb;
            var expectedValues = new Dictionary<int, byte>() {
                { 1, 0x01 },
                { 2, 0x03 },
                { 3, 0x06 },
                { 4, 0x0d },
                { 5, 0x1a },
                { 6, 0x34 },
                { 7, 0x69 },
                { 8, 0xd3 },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    expectedValue.Value, 
                    value.ReflectBits(expectedValue.Key));
            }
        }


        [Fact]
        public void ReflectBits_UInt16_InvalidBitLength_Throws()
        {
            UInt16 value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 17, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ReflectBits(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ReflectBits_UInt16_Works()
        {
            UInt16 value = 0x5bc1;
            var expectedValues = new Dictionary<int, UInt16>() {
                {  1, 0x0001 },       {  9, 0x0107 },
                {  2, 0x0002 },       { 10, 0x020f },
                {  3, 0x0004 },       { 11, 0x041e },
                {  4, 0x0008 },       { 12, 0x083d },
                {  5, 0x0010 },       { 13, 0x107b },
                {  6, 0x0020 },       { 14, 0x20f6 },
                {  7, 0x0041 },       { 15, 0x41ed },
                {  8, 0x0083 },       { 16, 0x83da },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    expectedValue.Value,
                    value.ReflectBits(expectedValue.Key));
            }
        }


        [Fact]
        public void ReflectBits_UInt32_InvalidBitLength_Throws()
        {
            UInt32 value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 33, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ReflectBits(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ReflectBits_UInt32_Works()
        {
            UInt32 value = 0x9df3404c;
            var expectedValues = new Dictionary<int, UInt32>() {
                {  1, 0x0000 },       { 17, 0x00006405 },
                {  2, 0x0000 },       { 18, 0x0000c80b },
                {  3, 0x0001 },       { 19, 0x00019016 },
                {  4, 0x0003 },       { 20, 0x0003202c },
                {  5, 0x0006 },       { 21, 0x00064059 },
                {  6, 0x000c },       { 22, 0x000c80b3 },
                {  7, 0x0019 },       { 23, 0x00190167 },
                {  8, 0x0032 },       { 24, 0x003202cf },
                {  9, 0x0064 },       { 25, 0x0064059f },
                { 10, 0x00c8 },       { 26, 0x00c80b3e },
                { 11, 0x0190 },       { 27, 0x0190167d },
                { 12, 0x0320 },       { 28, 0x03202cfb },
                { 13, 0x0640 },       { 29, 0x064059f7 },
                { 14, 0x0c80 },       { 30, 0x0c80b3ee },
                { 15, 0x1901 },       { 31, 0x190167dc },
                { 16, 0x3202 },       { 32, 0x3202cfb9 },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    expectedValue.Value,
                    value.ReflectBits(expectedValue.Key));
            }
        }


        [Fact]
        public void ReflectBits_UInt64_InvalidBitLength_Throws()
        {
            UInt64 value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 65, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ReflectBits(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ReflectBits_UInt64_Works()
        {
            UInt64 value = 0xf2faab56cb0b277f;
            var expectedValues = new Dictionary<int, UInt64>() {
                {  1, 0x0001 },       { 17, 0x0001fdc9 },       { 33, 0x0001fdc9a1a6 },       { 49, 0x0001fdc9a1a6d5aa },
                {  2, 0x0003 },       { 18, 0x0003fb93 },       { 34, 0x0003fb93434d },       { 50, 0x0003fb93434dab55 },
                {  3, 0x0007 },       { 19, 0x0007f726 },       { 35, 0x0007f726869b },       { 51, 0x0007f726869b56aa },
                {  4, 0x000f },       { 20, 0x000fee4d },       { 36, 0x000fee4d0d36 },       { 52, 0x000fee4d0d36ad55 },
                {  5, 0x001f },       { 21, 0x001fdc9a },       { 37, 0x001fdc9a1a6d },       { 53, 0x001fdc9a1a6d5aab },
                {  6, 0x003f },       { 22, 0x003fb934 },       { 38, 0x003fb93434da },       { 54, 0x003fb93434dab557 },
                {  7, 0x007f },       { 23, 0x007f7268 },       { 39, 0x007f726869b5 },       { 55, 0x007f726869b56aaf },
                {  8, 0x00fe },       { 24, 0x00fee4d0 },       { 40, 0x00fee4d0d36a },       { 56, 0x00fee4d0d36ad55f },
                {  9, 0x01fd },       { 25, 0x01fdc9a1 },       { 41, 0x01fdc9a1a6d5 },       { 57, 0x01fdc9a1a6d5aabe },
                { 10, 0x03fb },       { 26, 0x03fb9343 },       { 42, 0x03fb93434dab },       { 58, 0x03fb93434dab557d },
                { 11, 0x07f7 },       { 27, 0x07f72686 },       { 43, 0x07f726869b56 },       { 59, 0x07f726869b56aafa },
                { 12, 0x0fee },       { 28, 0x0fee4d0d },       { 44, 0x0fee4d0d36ad },       { 60, 0x0fee4d0d36ad55f4 },
                { 13, 0x1fdc },       { 29, 0x1fdc9a1a },       { 45, 0x1fdc9a1a6d5a },       { 61, 0x1fdc9a1a6d5aabe9 },
                { 14, 0x3fb9 },       { 30, 0x3fb93434 },       { 46, 0x3fb93434dab5 },       { 62, 0x3fb93434dab557d3 },
                { 15, 0x7f72 },       { 31, 0x7f726869 },       { 47, 0x7f726869b56a },       { 63, 0x7f726869b56aafa7 },
                { 16, 0xfee4 },       { 32, 0xfee4d0d3 },       { 48, 0xfee4d0d36ad5 },       { 64, 0xfee4d0d36ad55f4f },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    expectedValue.Value,
                    value.ReflectBits(expectedValue.Key));
            }
        }

    }
}
