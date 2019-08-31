using System;
using System.Collections.Generic;
using System.Data.HashFunction.BuzHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.BuzHash
{
    public class BuzHashConfig_Tests
    {
        [Fact]
        public void BuzHashConfig_Defaults_HaventChanged()
        {
            var buzHashConfig = new BuzHashConfig();

            Assert.Null(buzHashConfig.Rtab);

            Assert.Equal(64, buzHashConfig.HashSizeInBits);
            Assert.Equal(0UL, buzHashConfig.Seed);
            Assert.Equal(CircularShiftDirection.Left, buzHashConfig.ShiftDirection);
        }

        [Fact]
        public void BuzHashConfig_Clone_Works()
        {
            var buzHashConfig = new BuzHashConfig() {
                Rtab = new UInt64[256],
                HashSizeInBits = 32,
                Seed = 1337UL,
                ShiftDirection = CircularShiftDirection.Right
            };

            var buzHashConfigClone = buzHashConfig.Clone();

            Assert.IsType<BuzHashConfig>(buzHashConfigClone);

            Assert.Equal(buzHashConfig.Rtab, buzHashConfigClone.Rtab);
            Assert.Equal(buzHashConfig.HashSizeInBits, buzHashConfigClone.HashSizeInBits);
            Assert.Equal(buzHashConfig.Seed, buzHashConfigClone.Seed);
            Assert.Equal(buzHashConfig.ShiftDirection, buzHashConfigClone.ShiftDirection);
        }

        [Fact]
        public void BuzHashConfig_Clone_WithNullArrays_Works()
        {
            var buzHashConfig = new BuzHashConfig() {
                Rtab = null,
                HashSizeInBits = 32,
                Seed = 1337UL,
                ShiftDirection = CircularShiftDirection.Right
            };

            var buzHashConfigClone = buzHashConfig.Clone();

            Assert.IsType<BuzHashConfig>(buzHashConfigClone);

            Assert.Equal(buzHashConfig.Rtab, buzHashConfigClone.Rtab);
            Assert.Equal(buzHashConfig.HashSizeInBits, buzHashConfigClone.HashSizeInBits);
            Assert.Equal(buzHashConfig.Seed, buzHashConfigClone.Seed);
            Assert.Equal(buzHashConfig.ShiftDirection, buzHashConfigClone.ShiftDirection);
        }
    }
}
