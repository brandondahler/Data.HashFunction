using System;
using System.Collections.Generic;
using System.Data.HashFunction.CRC;
using System.Text;

namespace System.Data.HashFunction.Test.CRC
{
    public class CRC_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests_CRC3_ROHC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(3, "123456789", 0x6),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC3_ROHC);
        }

        public class IHashFunctionAsync_Tests_CRC4_ITU
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(4, "123456789", 0x7),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC4_ITU);
        }

        public class IHashFunctionAsync_Tests_CRC5_EPC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues =>
new KnownValue[] {
                        new KnownValue(5, "123456789", 0x00),
                    };


            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC5_EPC);
        }

        public class IHashFunctionAsync_Tests_CRC5_ITU
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues =>
                new KnownValue[] {
                    new KnownValue(5, "123456789", 0x07),
                };


            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC5_ITU);
        }

        public class IHashFunctionAsync_Tests_CRC5_USB
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(5, "123456789", 0x19),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC5_USB);
        }

        public class IHashFunctionAsync_Tests_CRC6_CDMA2000A
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x0d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_CDMA2000A);
        }

        public class IHashFunctionAsync_Tests_CRC6_CDMA2000B
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x3b),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_CDMA2000B);
        }

        public class IHashFunctionAsync_Tests_CRC6_DARC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x26),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_DARC);
        }

        public class IHashFunctionAsync_Tests_CRC6_ITU
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x06),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_ITU);
        }

        public class IHashFunctionAsync_Tests_CRC7
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(7, "123456789", 0x75),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC7);
        }

        public class IHashFunctionAsync_Tests_CRC7_ROHC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(7, "123456789", 0x53),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC7_ROHC);
        }

        public class IHashFunctionAsync_Tests_CRC8
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xf4),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8);
        }

        public class IHashFunctionAsync_Tests_CRC8_CDMA2000
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xda),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_CDMA2000);
        }

        public class IHashFunctionAsync_Tests_CRC8_DARC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x15),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_DARC);
        }

        public class IHashFunctionAsync_Tests_CRC8_DVBS2
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xbc),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_DVBS2);
        }

        public class IHashFunctionAsync_Tests_CRC8_EBU
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x97),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_EBU);
        }

        public class IHashFunctionAsync_Tests_CRC8_ICODE
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x7e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_ICODE);
        }

        public class IHashFunctionAsync_Tests_CRC8_ITU
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xa1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_ITU);
        }

        public class IHashFunctionAsync_Tests_CRC8_MAXIM
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xa1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_MAXIM);
        }

        public class IHashFunctionAsync_Tests_CRC8_ROHC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xd0),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_ROHC);
        }

        public class IHashFunctionAsync_Tests_CRC8_WCDMA
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x25),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_WCDMA);
        }

        public class IHashFunctionAsync_Tests_CRC10
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(10, "123456789", 0x199),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC10);
        }

        public class IHashFunctionAsync_Tests_CRC10_CDMA2000
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(10, "123456789", 0x233),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC10_CDMA2000);
        }

        public class IHashFunctionAsync_Tests_CRC11
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(11, "123456789", 0x5a3),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC11);
        }

        public class IHashFunctionAsync_Tests_CRC12_3GPP
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(12, "123456789", 0xdaf),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC12_3GPP);
        }

        public class IHashFunctionAsync_Tests_CRC12_CDMA2000
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(12, "123456789", 0xd4d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC12_CDMA2000);
        }

        public class IHashFunctionAsync_Tests_CRC12_DECT
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(12, "123456789", 0xf5b),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC12_DECT);
        }

        public class IHashFunctionAsync_Tests_CRC13_BBC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(13, "123456789", 0x04fa),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC13_BBC);
        }

        public class IHashFunctionAsync_Tests_CRC14_DARC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(14, "123456789", 0x082d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC14_DARC);
        }

        public class IHashFunctionAsync_Tests_CRC15
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(15, "123456789", 0x059e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC15);
        }

        public class IHashFunctionAsync_Tests_CRC15_MPT1327
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(15, "123456789", 0x2566),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC15_MPT1327);
        }

        public class IHashFunctionAsync_Tests_ARC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xbb3d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.ARC);
        }

        public class IHashFunctionAsync_Tests_CRC16_AUGCCITT
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xe5cc),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_AUGCCITT);
        }

        public class IHashFunctionAsync_Tests_CRC16_BUYPASS
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xfee8),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_BUYPASS);
        }

        public class IHashFunctionAsync_Tests_CRC16_CCITTFALSE
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x29b1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_CCITTFALSE);
        }

        public class IHashFunctionAsync_Tests_CRC16_CDMA2000
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x4c06),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_CDMA2000);
        }

        public class IHashFunctionAsync_Tests_CRC16_DDS110
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x9ecf),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DDS110);
        }

        public class IHashFunctionAsync_Tests_CRC16_DECTR
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x007e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DECTR);
        }

        public class IHashFunctionAsync_Tests_CRC16_DECTX
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x007f),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DECTX);
        }

        public class IHashFunctionAsync_Tests_CRC16_DNP
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xea82),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DNP);
        }

        public class IHashFunctionAsync_Tests_CRC16_EN13757
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xc2b7),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_EN13757);
        }

        public class IHashFunctionAsync_Tests_CRC16_GENIBUS
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xd64e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_GENIBUS);
        }

        public class IHashFunctionAsync_Tests_CRC16_MAXIM
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x44c2),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_MAXIM);
        }

        public class IHashFunctionAsync_Tests_CRC16_MCRF4XX
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x6f91),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_MCRF4XX);
        }

        public class IHashFunctionAsync_Tests_CRC16_RIELLO
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x63d0),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_RIELLO);
        }

        public class IHashFunctionAsync_Tests_CRC16_T10DIF
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xd0db),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_T10DIF);
        }

        public class IHashFunctionAsync_Tests_CRC16_TELEDISK
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x0fb3),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_TELEDISK);
        }

        public class IHashFunctionAsync_Tests_CRC16_TMS37157
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x26b1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_TMS37157);
        }

        public class IHashFunctionAsync_Tests_CRC16_USB
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xb4c8),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_USB);
        }

        public class IHashFunctionAsync_Tests_CRCA
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xbf05),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRCA);
        }

        public class IHashFunctionAsync_Tests_KERMIT
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x2189),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.KERMIT);
        }

        public class IHashFunctionAsync_Tests_MODBUS
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x4b37),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.MODBUS);
        }

        public class IHashFunctionAsync_Tests_X25
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x906e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.X25);
        }

        public class IHashFunctionAsync_Tests_XMODEM
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x31c3),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.XMODEM);
        }

        public class IHashFunctionAsync_Tests_CRC24
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(24, "123456789", 0x21cf02),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC24);
        }

        public class IHashFunctionAsync_Tests_CRC24_FLEXRAYA
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(24, "123456789", 0x7979bd),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC24_FLEXRAYA);
        }

        public class IHashFunctionAsync_Tests_CRC24_FLEXRAYB
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(24, "123456789", 0x1f23b8),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC24_FLEXRAYB);
        }

        public class IHashFunctionAsync_Tests_CRC31_PHILIPS
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(31, "123456789", 0x0ce9e46c),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC31_PHILIPS);
        }

        public class IHashFunctionAsync_Tests_CRC32
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xcbf43926),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32);
        }

        public class IHashFunctionAsync_Tests_CRC32_BZIP2
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xfc891918),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32_BZIP2);
        }

        public class IHashFunctionAsync_Tests_CRC32C
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xe3069283),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32C);
        }

        public class IHashFunctionAsync_Tests_CRC32D
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x87315576),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32D);
        }

        public class IHashFunctionAsync_Tests_CRC32_MPEG2
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x0376e6e7),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32_MPEG2);
        }

        public class IHashFunctionAsync_Tests_CRC32_POSIX
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x765e7680),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32_POSIX);
        }

        public class IHashFunctionAsync_Tests_CRC32Q
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x3010bf7f),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32Q);
        }

        public class IHashFunctionAsync_Tests_JAMCRC
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x340bc6d9),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.JAMCRC);
        }

        public class IHashFunctionAsync_Tests_XFER
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xbd0be338),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.XFER);
        }

        public class IHashFunctionAsync_Tests_CRC40_GSM
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(40, "123456789", 0xd4164fc646),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC40_GSM);
        }

        public class IHashFunctionAsync_Tests_CRC64
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "123456789", 0x6c40df5f0b497347),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC64);
        }

        public class IHashFunctionAsync_Tests_CRC64_WE
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "123456789", 0x62ec59e3f1a4f00a),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC64_WE);
        }

        public class IHashFunctionAsync_Tests_CRC64_XZ
            : IHashFunctionAsync_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "123456789", 0x995dc9bbdf1939fa),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC64_XZ);
        }

    }
}
