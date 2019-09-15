using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash.Utilities;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash.Utilities
{
    public class UInt128_Tests
    {

        #region Constructor

        [Fact]
        public void UInt128_Constructor_Default_IsZero()
        {
            var uint128 = new UInt128();

            Assert.Equal(0UL, uint128.Low);
            Assert.Equal(0UL, uint128.High);
        }

        [Fact]
        public void UInt128_Constructor_LowHigh_Works()
        {
            var uint128 = new UInt128(1337UL, 7331UL);

            Assert.Equal(1337UL, uint128.Low);
            Assert.Equal(7331UL, uint128.High);
        }

        #endregion

        #region Low

        [Fact]
        public void UInt128_Low_Works()
        {
            var uint128 = new UInt128();

            Assert.Equal(0UL, uint128.Low);
            Assert.Equal(0UL, uint128.High);

            uint128.Low = 1337UL;
            Assert.Equal(1337UL, uint128.Low);
            Assert.Equal(0UL, uint128.High);
        }

        #endregion

        #region High

        [Fact]
        public void UInt128_High_Works()
        {
            var uint128 = new UInt128();

            Assert.Equal(0UL, uint128.Low);
            Assert.Equal(0UL, uint128.High);

            uint128.High = 1337UL;
            Assert.Equal(0UL, uint128.Low);
            Assert.Equal(1337UL, uint128.High);
        }

        #endregion

    }
}
