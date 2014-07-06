//! Automatically generated from CRCStandards.tt
//! Direct modifications to this file will be lost.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test
{

    public class IHashFunctionTests_CRC3_ROHC
        : IHashFunctionTests<CRC3_ROHC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(3, "123456789", 0x6),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC4_ITU
        : IHashFunctionTests<CRC4_ITU>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(4, "123456789", 0x7),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC5_EPC
        : IHashFunctionTests<CRC5_EPC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(5, "123456789", 0x00),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC5_ITU
        : IHashFunctionTests<CRC5_ITU>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(5, "123456789", 0x07),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC5_USB
        : IHashFunctionTests<CRC5_USB>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(5, "123456789", 0x19),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC6_CDMA2000A
        : IHashFunctionTests<CRC6_CDMA2000A>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(6, "123456789", 0x0d),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC6_CDMA2000B
        : IHashFunctionTests<CRC6_CDMA2000B>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(6, "123456789", 0x3b),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC6_DARC
        : IHashFunctionTests<CRC6_DARC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(6, "123456789", 0x26),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC6_ITU
        : IHashFunctionTests<CRC6_ITU>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(6, "123456789", 0x06),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC7
        : IHashFunctionTests<CRC7>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(7, "123456789", 0x75),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC7_ROHC
        : IHashFunctionTests<CRC7_ROHC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(7, "123456789", 0x53),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8
        : IHashFunctionTests<CRC8>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0xf4),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_CDMA2000
        : IHashFunctionTests<CRC8_CDMA2000>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0xda),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_DARC
        : IHashFunctionTests<CRC8_DARC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0x15),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_DVBS2
        : IHashFunctionTests<CRC8_DVBS2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0xbc),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_EBU
        : IHashFunctionTests<CRC8_EBU>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0x97),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_ICODE
        : IHashFunctionTests<CRC8_ICODE>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0x7e),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_ITU
        : IHashFunctionTests<CRC8_ITU>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0xa1),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_MAXIM
        : IHashFunctionTests<CRC8_MAXIM>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0xa1),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_ROHC
        : IHashFunctionTests<CRC8_ROHC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0xd0),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC8_WCDMA
        : IHashFunctionTests<CRC8_WCDMA>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, "123456789", 0x25),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC10
        : IHashFunctionTests<CRC10>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(10, "123456789", 0x199),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC10_CDMA2000
        : IHashFunctionTests<CRC10_CDMA2000>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(10, "123456789", 0x233),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC11
        : IHashFunctionTests<CRC11>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(11, "123456789", 0x5a3),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC12_3GPP
        : IHashFunctionTests<CRC12_3GPP>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(12, "123456789", 0xdaf),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC12_CDMA2000
        : IHashFunctionTests<CRC12_CDMA2000>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(12, "123456789", 0xd4d),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC12_DECT
        : IHashFunctionTests<CRC12_DECT>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(12, "123456789", 0xf5b),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC13_BBC
        : IHashFunctionTests<CRC13_BBC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(13, "123456789", 0x04fa),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC14_DARC
        : IHashFunctionTests<CRC14_DARC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(14, "123456789", 0x082d),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC15
        : IHashFunctionTests<CRC15>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(15, "123456789", 0x059e),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC15_MPT1327
        : IHashFunctionTests<CRC15_MPT1327>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(15, "123456789", 0x2566),
                };
            }
        }
    }

    public class IHashFunctionTests_ARC
        : IHashFunctionTests<ARC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xbb3d),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_AUGCCITT
        : IHashFunctionTests<CRC16_AUGCCITT>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xe5cc),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_BUYPASS
        : IHashFunctionTests<CRC16_BUYPASS>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xfee8),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_CCITTFALSE
        : IHashFunctionTests<CRC16_CCITTFALSE>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x29b1),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_CDMA2000
        : IHashFunctionTests<CRC16_CDMA2000>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x4c06),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_DDS110
        : IHashFunctionTests<CRC16_DDS110>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x9ecf),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_DECTR
        : IHashFunctionTests<CRC16_DECTR>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x007e),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_DECTX
        : IHashFunctionTests<CRC16_DECTX>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x007f),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_DNP
        : IHashFunctionTests<CRC16_DNP>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xea82),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_EN13757
        : IHashFunctionTests<CRC16_EN13757>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xc2b7),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_GENIBUS
        : IHashFunctionTests<CRC16_GENIBUS>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xd64e),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_MAXIM
        : IHashFunctionTests<CRC16_MAXIM>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x44c2),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_MCRF4XX
        : IHashFunctionTests<CRC16_MCRF4XX>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x6f91),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_RIELLO
        : IHashFunctionTests<CRC16_RIELLO>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x63d0),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_T10DIF
        : IHashFunctionTests<CRC16_T10DIF>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xd0db),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_TELEDISK
        : IHashFunctionTests<CRC16_TELEDISK>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x0fb3),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_TMS37157
        : IHashFunctionTests<CRC16_TMS37157>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x26b1),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC16_USB
        : IHashFunctionTests<CRC16_USB>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xb4c8),
                };
            }
        }
    }

    public class IHashFunctionTests_CRCA
        : IHashFunctionTests<CRCA>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0xbf05),
                };
            }
        }
    }

    public class IHashFunctionTests_KERMIT
        : IHashFunctionTests<KERMIT>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x2189),
                };
            }
        }
    }

    public class IHashFunctionTests_MODBUS
        : IHashFunctionTests<MODBUS>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x4b37),
                };
            }
        }
    }

    public class IHashFunctionTests_X25
        : IHashFunctionTests<X25>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x906e),
                };
            }
        }
    }

    public class IHashFunctionTests_XMODEM
        : IHashFunctionTests<XMODEM>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(16, "123456789", 0x31c3),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC24
        : IHashFunctionTests<CRC24>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(24, "123456789", 0x21cf02),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC24_FLEXRAYA
        : IHashFunctionTests<CRC24_FLEXRAYA>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(24, "123456789", 0x7979bd),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC24_FLEXRAYB
        : IHashFunctionTests<CRC24_FLEXRAYB>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(24, "123456789", 0x1f23b8),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC31_PHILIPS
        : IHashFunctionTests<CRC31_PHILIPS>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(31, "123456789", 0x0ce9e46c),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC32
        : IHashFunctionTests<CRC32>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0xcbf43926),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC32_BZIP2
        : IHashFunctionTests<CRC32_BZIP2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0xfc891918),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC32C
        : IHashFunctionTests<CRC32C>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0xe3069283),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC32D
        : IHashFunctionTests<CRC32D>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0x87315576),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC32_MPEG2
        : IHashFunctionTests<CRC32_MPEG2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0x0376e6e7),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC32_POSIX
        : IHashFunctionTests<CRC32_POSIX>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0x765e7680),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC32Q
        : IHashFunctionTests<CRC32Q>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0x3010bf7f),
                };
            }
        }
    }

    public class IHashFunctionTests_JAMCRC
        : IHashFunctionTests<JAMCRC>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0x340bc6d9),
                };
            }
        }
    }

    public class IHashFunctionTests_XFER
        : IHashFunctionTests<XFER>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, "123456789", 0xbd0be338),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC40_GSM
        : IHashFunctionTests<CRC40_GSM>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(40, "123456789", 0xd4164fc646),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC64
        : IHashFunctionTests<CRC64>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, "123456789", 0x6c40df5f0b497347),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC64_WE
        : IHashFunctionTests<CRC64_WE>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, "123456789", 0x62ec59e3f1a4f00a),
                };
            }
        }
    }

    public class IHashFunctionTests_CRC64_XZ
        : IHashFunctionTests<CRC64_XZ>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, "123456789", 0x995dc9bbdf1939fa),
                };
            }
        }
    }

}



