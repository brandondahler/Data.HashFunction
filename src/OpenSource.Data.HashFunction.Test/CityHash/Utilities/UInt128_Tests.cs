using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.CityHash.Utilities;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.CityHash.Utilities
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
    }
}
