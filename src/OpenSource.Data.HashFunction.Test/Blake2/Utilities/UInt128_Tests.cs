using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Blake2.Utilities;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Blake2.Utilities
{
    public class UInt128_Tests
    {
        [Fact]
        public void UInt128_Constructor_LowOnly_Works()
        {
            var uint128 = new UInt128(1337UL);

            Assert.Equal(1337UL, uint128.Low);
            Assert.Equal(0UL, uint128.High);
        }

        [Fact]
        public void UInt128_Constructor_LowHigh_Works()
        {
            var uint128 = new UInt128(1337UL, 7331UL);

            Assert.Equal(1337UL, uint128.Low);
            Assert.Equal(7331UL, uint128.High);
        }


        [Fact]
        public void UInt128_opAddition_Works()
        {
            var x = new UInt128(1337UL);
            var y = x + new UInt128(1UL);

            Assert.Equal(1338UL, y.Low);
            Assert.Equal(0UL, y.High);
        }


        [Fact]
        public void UInt128_opAddition_WithCarryOver_Works()
        {
            var x = new UInt128(0xFFFFFFFFFFFFFFFFUL);
            var y = x + new UInt128(3UL);

            Assert.Equal(2UL, y.Low);
            Assert.Equal(1UL, y.High);
        }
    }
}
