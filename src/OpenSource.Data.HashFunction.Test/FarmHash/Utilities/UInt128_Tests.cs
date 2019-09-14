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

        #region GetBytes

        [Fact]
        public void UInt128_GetBytes_Works()
        {
            var uint128 = new UInt128(0x010afe7329b03c39UL, 0x238e8c0d1aa452b0UL);

            var expectedBytes = new byte[] {
                0x39, 0x3c, 0xb0, 0x29, 0x73, 0xfe, 0x0a, 0x01,
                0xb0, 0x52, 0xa4, 0x1a, 0x0d, 0x8c, 0x8e, 0x23
            };

            Assert.Equal(expectedBytes, uint128.GetBytes());
        }

        #endregion

    }
}
