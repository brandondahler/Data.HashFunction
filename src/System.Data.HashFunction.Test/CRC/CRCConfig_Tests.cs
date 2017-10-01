using System;
using System.Collections.Generic;
using System.Data.HashFunction.CRC;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.CRC
{
    public class CRCConfig_Tests
    {
        [Fact]
        public void CRCConfig_Defaults_HaventChanged()
        {
            var crcConfig = new CRCConfig();


            Assert.Equal(0, crcConfig.HashSizeInBits);
            Assert.Equal(0UL, crcConfig.InitialValue);
            Assert.Equal(0UL, crcConfig.Polynomial);
            Assert.Equal(false, crcConfig.ReflectIn);
            Assert.Equal(false, crcConfig.ReflectOut);
            Assert.Equal(0UL, crcConfig.XOrOut);
        }

        [Fact]
        public void CRCConfig_Standards_HaventChanged()
        {
            foreach (var crcStandard in _expectedCrcStandards)
            {
                var crcConfig = crcStandard.Key();
                var expectedCrcConig = crcStandard.Value;

                Assert.Equal(expectedCrcConig.HashSizeInBits, crcConfig.HashSizeInBits);
                Assert.Equal(expectedCrcConig.InitialValue, crcConfig.InitialValue);
                Assert.Equal(expectedCrcConig.Polynomial, crcConfig.Polynomial);
                Assert.Equal(expectedCrcConig.ReflectIn, crcConfig.ReflectIn);
                Assert.Equal(expectedCrcConig.ReflectOut, crcConfig.ReflectOut);
                Assert.Equal(expectedCrcConig.XOrOut, crcConfig.XOrOut);
            }
        }

        [Fact]
        public void CRCConfig_Clone_Works()
        {
            var crcConfig = CRCConfig.CRC64;

            var crcConfigClone = crcConfig.Clone();

            Assert.IsType<CRCConfig>(crcConfigClone);

            Assert.Equal(crcConfig.HashSizeInBits, crcConfigClone.HashSizeInBits);
            Assert.Equal(crcConfig.InitialValue, crcConfigClone.InitialValue);
            Assert.Equal(crcConfig.Polynomial, crcConfigClone.Polynomial);
            Assert.Equal(crcConfig.ReflectIn, crcConfigClone.ReflectIn);
            Assert.Equal(crcConfig.ReflectOut, crcConfigClone.ReflectOut);
            Assert.Equal(crcConfig.XOrOut, crcConfigClone.XOrOut);
        }



        private readonly IEnumerable<KeyValuePair<Func<ICRCConfig>, ICRCConfig>> _expectedCrcStandards =
            new[] {
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC3_ROHC,
                    new CRCConfig {
                        HashSizeInBits = 3,
                        Polynomial = 0x3,
                        InitialValue = 0x7,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC4_ITU,
                    new CRCConfig {
                        HashSizeInBits = 4,
                        Polynomial = 0x3,
                        InitialValue = 0x0,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC5_EPC,
                    new CRCConfig {
                        HashSizeInBits = 5,
                        Polynomial = 0x09,
                        InitialValue = 0x09,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC5_ITU,
                    new CRCConfig {
                        HashSizeInBits = 5,
                        Polynomial = 0x15,
                        InitialValue = 0x00,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC5_USB,
                    new CRCConfig {
                        HashSizeInBits = 5,
                        Polynomial = 0x05,
                        InitialValue = 0x1f,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x1f,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC6_CDMA2000A,
                    new CRCConfig {
                        HashSizeInBits = 6,
                        Polynomial = 0x27,
                        InitialValue = 0x3f,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC6_CDMA2000B,
                    new CRCConfig {
                        HashSizeInBits = 6,
                        Polynomial = 0x07,
                        InitialValue = 0x3f,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC6_DARC,
                    new CRCConfig {
                        HashSizeInBits = 6,
                        Polynomial = 0x19,
                        InitialValue = 0x00,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC6_ITU,
                    new CRCConfig {
                        HashSizeInBits = 6,
                        Polynomial = 0x03,
                        InitialValue = 0x00,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC7,
                    new CRCConfig {
                        HashSizeInBits = 7,
                        Polynomial = 0x09,
                        InitialValue = 0x00,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC7_ROHC,
                    new CRCConfig {
                        HashSizeInBits = 7,
                        Polynomial = 0x4f,
                        InitialValue = 0x7f,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x07,
                        InitialValue = 0x00,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_CDMA2000,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x9b,
                        InitialValue = 0xff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_DARC,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x39,
                        InitialValue = 0x00,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_DVBS2,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0xd5,
                        InitialValue = 0x00,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_EBU,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x1d,
                        InitialValue = 0xff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_ICODE,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x1d,
                        InitialValue = 0xfd,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_ITU,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x07,
                        InitialValue = 0x00,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x55,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_MAXIM,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x31,
                        InitialValue = 0x00,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_ROHC,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x07,
                        InitialValue = 0xff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC8_WCDMA,
                    new CRCConfig {
                        HashSizeInBits = 8,
                        Polynomial = 0x9b,
                        InitialValue = 0x00,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC10,
                    new CRCConfig {
                        HashSizeInBits = 10,
                        Polynomial = 0x233,
                        InitialValue = 0x000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC10_CDMA2000,
                    new CRCConfig {
                        HashSizeInBits = 10,
                        Polynomial = 0x3d9,
                        InitialValue = 0x3ff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC11,
                    new CRCConfig {
                        HashSizeInBits = 11,
                        Polynomial = 0x385,
                        InitialValue = 0x01a,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC12_3GPP,
                    new CRCConfig {
                        HashSizeInBits = 12,
                        Polynomial = 0x80f,
                        InitialValue = 0x000,
                        ReflectIn = false,
                        ReflectOut = true,
                        XOrOut = 0x000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC12_CDMA2000,
                    new CRCConfig {
                        HashSizeInBits = 12,
                        Polynomial = 0xf13,
                        InitialValue = 0xfff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC12_DECT,
                    new CRCConfig {
                        HashSizeInBits = 12,
                        Polynomial = 0x80f,
                        InitialValue = 0x000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC13_BBC,
                    new CRCConfig {
                        HashSizeInBits = 13,
                        Polynomial = 0x1cf5,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC14_DARC,
                    new CRCConfig {
                        HashSizeInBits = 14,
                        Polynomial = 0x0805,
                        InitialValue = 0x0000,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC15,
                    new CRCConfig {
                        HashSizeInBits = 15,
                        Polynomial = 0x4599,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC15_MPT1327,
                    new CRCConfig {
                        HashSizeInBits = 15,
                        Polynomial = 0x6815,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0001,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.ARC,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x8005,
                        InitialValue = 0x0000,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_AUGCCITT,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0x1d0f,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_BUYPASS,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x8005,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_CCITTFALSE,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0xffff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_CDMA2000,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0xc867,
                        InitialValue = 0xffff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_DDS110,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x8005,
                        InitialValue = 0x800d,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_DECTR,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x0589,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0001,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_DECTX,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x0589,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_DNP,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x3d65,
                        InitialValue = 0x0000,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_EN13757,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x3d65,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0xffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_GENIBUS,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0xffff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0xffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_MAXIM,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x8005,
                        InitialValue = 0x0000,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_MCRF4XX,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0xffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_RIELLO,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0xb2aa,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_T10DIF,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x8bb7,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_TELEDISK,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0xa097,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_TMS37157,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0x89ec,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC16_USB,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x8005,
                        InitialValue = 0xffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRCA,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0xc6c6,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.KERMIT,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0x0000,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.MODBUS,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x8005,
                        InitialValue = 0xffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.X25,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0xffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.XMODEM,
                    new CRCConfig {
                        HashSizeInBits = 16,
                        Polynomial = 0x1021,
                        InitialValue = 0x0000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC24,
                    new CRCConfig {
                        HashSizeInBits = 24,
                        Polynomial = 0x864cfb,
                        InitialValue = 0xb704ce,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC24_FLEXRAYA,
                    new CRCConfig {
                        HashSizeInBits = 24,
                        Polynomial = 0x5d6dcb,
                        InitialValue = 0xfedcba,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC24_FLEXRAYB,
                    new CRCConfig {
                        HashSizeInBits = 24,
                        Polynomial = 0x5d6dcb,
                        InitialValue = 0xabcdef,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC31_PHILIPS,
                    new CRCConfig {
                        HashSizeInBits = 31,
                        Polynomial = 0x04c11db7,
                        InitialValue = 0x7fffffff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x7fffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC32,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x04c11db7,
                        InitialValue = 0xffffffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC32_BZIP2,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x04c11db7,
                        InitialValue = 0xffffffff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0xffffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC32C,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x1edc6f41,
                        InitialValue = 0xffffffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC32D,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0xa833982b,
                        InitialValue = 0xffffffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC32_MPEG2,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x04c11db7,
                        InitialValue = 0xffffffff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC32_POSIX,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x04c11db7,
                        InitialValue = 0x00000000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0xffffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC32Q,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x814141ab,
                        InitialValue = 0x00000000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.JAMCRC,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x04c11db7,
                        InitialValue = 0xffffffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0x00000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.XFER,
                    new CRCConfig {
                        HashSizeInBits = 32,
                        Polynomial = 0x000000af,
                        InitialValue = 0x00000000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x00000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC40_GSM,
                    new CRCConfig {
                        HashSizeInBits = 40,
                        Polynomial = 0x0004820009,
                        InitialValue = 0x0000000000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0xffffffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC64,
                    new CRCConfig {
                        HashSizeInBits = 64,
                        Polynomial = 0x42f0e1eba9ea3693,
                        InitialValue = 0x0000000000000000,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0x0000000000000000,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC64_WE,
                    new CRCConfig {
                        HashSizeInBits = 64,
                        Polynomial = 0x42f0e1eba9ea3693,
                        InitialValue = 0xffffffffffffffff,
                        ReflectIn = false,
                        ReflectOut = false,
                        XOrOut = 0xffffffffffffffff,
                    }
                ),
                new KeyValuePair<Func<ICRCConfig>, ICRCConfig>(
                    () => CRCConfig.CRC64_XZ,
                    new CRCConfig {
                        HashSizeInBits = 64,
                        Polynomial = 0x42f0e1eba9ea3693,
                        InitialValue = 0xffffffffffffffff,
                        ReflectIn = true,
                        ReflectOut = true,
                        XOrOut = 0xffffffffffffffff,
                    }
                ),
            };
    }
}
