using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.CRC
{
    public class CRCSettingsTests
    {

        #region Constructor
        
        #region Parameters

        [Fact]
        public void CRCSettings_Constructor_ValidBits_Works()
        {
            foreach (var validBits in Enumerable.Range(1, 64))
            {
                Assert.DoesNotThrow(() =>
                    new CRCSettings(validBits, 0, 0, false, false, 0));
            }
        }

        [Fact]
        public void CRCSettings_Constructor_InvalidBits_Throws()
        {
            foreach (var invalidBits in new[] { int.MinValue, short.MinValue, -1, 0, 65, short.MaxValue, int.MaxValue })
            {
                Assert.Equal("bits",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        new CRCSettings(invalidBits, 0, 0, false, false, 0))
                    .ParamName);
            }
        }


        [Fact]
        public void CRCSettings_Constructor_ValidPolynomial_Works()
        {
            foreach (var validBits in Enumerable.Range(1, 64))
            {
                for (int x = 1; x <= validBits; ++x)
                {
                    var validPolynomial = UInt64.MaxValue >> (64 - x);

                    Assert.DoesNotThrow(() =>
                        new CRCSettings(validBits, validPolynomial, 0, false, false, 0));
                }
            }
        }

        [Fact]
        public void CRCSettings_Constructor_InvalidPolynomial_Throws()
        {
            foreach (var validBits in Enumerable.Range(1, 64))
            {
                for (int x = validBits; x < 64; ++x)
                {
                    var invalidPolynomial = 1UL << x;

                    Assert.Equal("polynomial",
                        Assert.Throws<ArgumentOutOfRangeException>(() =>
                            new CRCSettings(validBits, invalidPolynomial, 0, false, false, 0))
                        .ParamName);
                }
            }
        }


        [Fact]
        public void CRCSettings_Constructor_ValidInitialValue_Works()
        {
            foreach (var validBits in Enumerable.Range(1, 64))
            {
                for (int x = 1; x <= validBits; ++x)
                {
                    var validInitialValue = UInt64.MaxValue >> (64 - x);

                    Assert.DoesNotThrow(() =>
                        new CRCSettings(validBits, 0, validInitialValue, false, false, 0));
                }
            }
        }

        [Fact]
        public void CRCSettings_Constructor_InvalidInitialValue_Throws()
        {
            foreach (var validBits in Enumerable.Range(1, 64))
            {
                for (int x = validBits; x < 64; ++x)
                {
                    var invalidInitialValue = 1UL << x;

                    Assert.Equal("initialValue",
                        Assert.Throws<ArgumentOutOfRangeException>(() =>
                            new CRCSettings(validBits, 0, invalidInitialValue, false, false, 0))
                        .ParamName);
                }
            }
        }


        [Fact]
        public void CRCSettings_Constructor_ValidXOrOut_Works()
        {
            foreach (var validBits in Enumerable.Range(1, 64))
            {
                for (int x = 1; x <= validBits; ++x)
                {
                    var validXOrOut = UInt64.MaxValue >> (64 - x);

                    Assert.DoesNotThrow(() =>
                        new CRCSettings(validBits, 0, 0, false, false, validXOrOut));
                }
            }
        }

        [Fact]
        public void CRCSettings_Constructor_InvalidXOrOut_Throws()
        {
            foreach (var validBits in Enumerable.Range(1, 64))
            {
                for (int x = validBits; x < 64; ++x)
                {
                    var invalidXOrOut = 1UL << x;

                    Assert.Equal("xOrOut",
                        Assert.Throws<ArgumentOutOfRangeException>(() =>
                            new CRCSettings(validBits, 0, 0, false, false, invalidXOrOut))
                        .ParamName);
                }
            }
        }

        #endregion

        [Fact]
        public void CRCSettings_Constructor_SetsParameters()
        {
            var r = new Random();

            foreach (var bits in Enumerable.Range(1, 64))
            {
                for (int x = 0; x < 4; ++x)
                {
                    var randomBytes = new byte[24];
                    r.NextBytes(randomBytes);

                    var bitMask = UInt64.MaxValue >> (64 - bits);

                    var polynomial   = BitConverter.ToUInt64(randomBytes,  0) & bitMask;
                    var initialValue = BitConverter.ToUInt64(randomBytes,  8) & bitMask;
                    var reflectIn    = (x & 1) > 0;
                    var reflectOut   = (x & 2) > 0;
                    var xOrOut       = BitConverter.ToUInt64(randomBytes, 16) & bitMask;


                    var settings = 
                        new CRCSettings(bits, polynomial, initialValue, reflectIn, reflectOut, xOrOut);

                    Assert.Equal(bits,       settings.Bits);
                    Assert.Equal(polynomial, settings.Polynomial);
                    Assert.Equal(reflectIn,  settings.ReflectIn);
                    Assert.Equal(reflectOut, settings.ReflectOut);
                    Assert.Equal(xOrOut,     settings.XOrOut);
                }
            }
        }

        #endregion

        [Fact]
        public void CRCSettings_PreCalculateTable_Works()
        {
            var settings = new CRCSettings(1, 0, 0, false, false, 0);

            Assert.True(settings.PreCalculateTable());
            Assert.False(settings.PreCalculateTable());
        }

    }
}
