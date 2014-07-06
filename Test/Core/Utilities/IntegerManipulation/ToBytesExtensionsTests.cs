using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.IntegerManipulation
{
    using System.Data.HashFunction.Utilities.IntegerManipulation;

    public class ToBytesExtensionsTests
    {
        [Fact]
        public void ToBytes_Byte_InvalidBitLength_Throws()
        {
            byte value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 9, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ToBytes(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ToBytes_Byte_Works()
        {
            byte value = 0xD2;
            var expectedValues = new Dictionary<int, byte>() {
                { 1, 0x00 },
                { 2, 0x02 },
                { 3, 0x02 },
                { 4, 0x02 },
                { 5, 0x12 },
                { 6, 0x12 },
                { 7, 0x52 },
                { 8, 0xd2 },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    new[] { expectedValue.Value }, 
                    value.ToBytes(expectedValue.Key));
            }
        }


        [Fact]
        public void ToBytes_UInt16_InvalidBitLength_Throws()
        {
            UInt16 value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 17, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ToBytes(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ToBytes_UInt16_Works()
        {
            UInt16 value = 0xf6e1;
            var expectedValues = new Dictionary<int, UInt16>() {
                {  1, 0x01 },       {  9, 0x00e1 },
                {  2, 0x01 },       { 10, 0x02e1 },
                {  3, 0x01 },       { 11, 0x06e1 },
                {  4, 0x01 },       { 12, 0x06e1 },
                {  5, 0x01 },       { 13, 0x16e1 },
                {  6, 0x21 },       { 14, 0x36e1 },
                {  7, 0x61 },       { 15, 0x76e1 },
                {  8, 0xe1 },       { 16, 0xf6e1 },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    BitConverter.GetBytes(expectedValue.Value).Take((expectedValue.Key + 7) / 8), 
                    value.ToBytes(expectedValue.Key));
            }
        }


        [Fact]
        public void ToBytes_UInt32_InvalidBitLength_Throws()
        {
            UInt32 value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 33, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ToBytes(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ToBytes_UInt32_Works()
        {
            UInt32 value = 0x9df3404c;
            var expectedValues = new Dictionary<int, UInt32>() {
                {  1, 0x0000 },       { 17, 0x0001404c },
                {  2, 0x0000 },       { 18, 0x0003404c },
                {  3, 0x0004 },       { 19, 0x0003404c },
                {  4, 0x000c },       { 20, 0x0003404c },
                {  5, 0x000c },       { 21, 0x0013404c },
                {  6, 0x000c },       { 22, 0x0033404c },
                {  7, 0x004c },       { 23, 0x0073404c },
                {  8, 0x004c },       { 24, 0x00f3404c },
                {  9, 0x004c },       { 25, 0x01f3404c },
                { 10, 0x004c },       { 26, 0x01f3404c },
                { 11, 0x004c },       { 27, 0x05f3404c },
                { 12, 0x004c },       { 28, 0x0df3404c },
                { 13, 0x004c },       { 29, 0x1df3404c },
                { 14, 0x004c },       { 30, 0x1df3404c },
                { 15, 0x404c },       { 31, 0x1df3404c },
                { 16, 0x404c },       { 32, 0x9df3404c },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    BitConverter.GetBytes(expectedValue.Value).Take((expectedValue.Key + 7) / 8), 
                    value.ToBytes(expectedValue.Key));
            }
        }


        [Fact]
        public void ToBytes_UInt64_InvalidBitLength_Throws()
        {
            UInt64 value = 0;

            foreach (var invalidBitLength in new[] { int.MinValue, short.MinValue, -1, 0, 65, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        value.ToBytes(invalidBitLength))
                    .ParamName);
            }
        }

        [Fact]
        public void ToBytes_UInt64_Works()
        {
            UInt64 value = 0xaf9363cc7e0d89eb;
            var expectedValues = new Dictionary<int, UInt64>() {
                {  1, 0x0001 },       { 17, 0x000189eb },       { 33, 0x00007e0d89eb },       { 49, 0x000163cc7e0d89eb },
                {  2, 0x0003 },       { 18, 0x000189eb },       { 34, 0x00007e0d89eb },       { 50, 0x000363cc7e0d89eb },
                {  3, 0x0003 },       { 19, 0x000589eb },       { 35, 0x00047e0d89eb },       { 51, 0x000363cc7e0d89eb },
                {  4, 0x000b },       { 20, 0x000d89eb },       { 36, 0x000c7e0d89eb },       { 52, 0x000363cc7e0d89eb },
                {  5, 0x000b },       { 21, 0x000d89eb },       { 37, 0x000c7e0d89eb },       { 53, 0x001363cc7e0d89eb },
                {  6, 0x002b },       { 22, 0x000d89eb },       { 38, 0x000c7e0d89eb },       { 54, 0x001363cc7e0d89eb },
                {  7, 0x006b },       { 23, 0x000d89eb },       { 39, 0x004c7e0d89eb },       { 55, 0x001363cc7e0d89eb },
                {  8, 0x00eb },       { 24, 0x000d89eb },       { 40, 0x00cc7e0d89eb },       { 56, 0x009363cc7e0d89eb },
                {  9, 0x01eb },       { 25, 0x000d89eb },       { 41, 0x01cc7e0d89eb },       { 57, 0x019363cc7e0d89eb },
                { 10, 0x01eb },       { 26, 0x020d89eb },       { 42, 0x03cc7e0d89eb },       { 58, 0x039363cc7e0d89eb },
                { 11, 0x01eb },       { 27, 0x060d89eb },       { 43, 0x03cc7e0d89eb },       { 59, 0x079363cc7e0d89eb },
                { 12, 0x09eb },       { 28, 0x0e0d89eb },       { 44, 0x03cc7e0d89eb },       { 60, 0x0f9363cc7e0d89eb },
                { 13, 0x09eb },       { 29, 0x1e0d89eb },       { 45, 0x03cc7e0d89eb },       { 61, 0x0f9363cc7e0d89eb },
                { 14, 0x09eb },       { 30, 0x3e0d89eb },       { 46, 0x23cc7e0d89eb },       { 62, 0x2f9363cc7e0d89eb },
                { 15, 0x09eb },       { 31, 0x7e0d89eb },       { 47, 0x63cc7e0d89eb },       { 63, 0x2f9363cc7e0d89eb },
                { 16, 0x89eb },       { 32, 0x7e0d89eb },       { 48, 0x63cc7e0d89eb },       { 64, 0xaf9363cc7e0d89eb },
            };

            foreach (var expectedValue in expectedValues)
            {
                Assert.Equal(
                    BitConverter.GetBytes(expectedValue.Value).Take((expectedValue.Key + 7) / 8), 
                    value.ToBytes(expectedValue.Key));
            }
        }

    }
}
