//! Automatically generated from IHashFunctionTests_CRC.tt
//! Direct modifications to this file will be lost.

using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.CRCStandards;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test
{

    public class IHashFunctionTests_CRC3_ROHC
        : IHashFunctionAsyncTests<CRC3_ROHC>
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

        protected override CRC3_ROHC CreateHashFunction(int hashSize)
        {
            return new CRC3_ROHC();
        }
    }

    public class IHashFunctionTests_CRC4_ITU
        : IHashFunctionAsyncTests<CRC4_ITU>
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

        protected override CRC4_ITU CreateHashFunction(int hashSize)
        {
            return new CRC4_ITU();
        }
    }

    public class IHashFunctionTests_CRC5_EPC
        : IHashFunctionAsyncTests<CRC5_EPC>
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

        protected override CRC5_EPC CreateHashFunction(int hashSize)
        {
            return new CRC5_EPC();
        }
    }

    public class IHashFunctionTests_CRC5_ITU
        : IHashFunctionAsyncTests<CRC5_ITU>
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

        protected override CRC5_ITU CreateHashFunction(int hashSize)
        {
            return new CRC5_ITU();
        }
    }

    public class IHashFunctionTests_CRC5_USB
        : IHashFunctionAsyncTests<CRC5_USB>
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

        protected override CRC5_USB CreateHashFunction(int hashSize)
        {
            return new CRC5_USB();
        }
    }

    public class IHashFunctionTests_CRC6_CDMA2000A
        : IHashFunctionAsyncTests<CRC6_CDMA2000A>
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

        protected override CRC6_CDMA2000A CreateHashFunction(int hashSize)
        {
            return new CRC6_CDMA2000A();
        }
    }

    public class IHashFunctionTests_CRC6_CDMA2000B
        : IHashFunctionAsyncTests<CRC6_CDMA2000B>
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

        protected override CRC6_CDMA2000B CreateHashFunction(int hashSize)
        {
            return new CRC6_CDMA2000B();
        }
    }

    public class IHashFunctionTests_CRC6_DARC
        : IHashFunctionAsyncTests<CRC6_DARC>
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

        protected override CRC6_DARC CreateHashFunction(int hashSize)
        {
            return new CRC6_DARC();
        }
    }

    public class IHashFunctionTests_CRC6_ITU
        : IHashFunctionAsyncTests<CRC6_ITU>
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

        protected override CRC6_ITU CreateHashFunction(int hashSize)
        {
            return new CRC6_ITU();
        }
    }

    public class IHashFunctionTests_CRC7
        : IHashFunctionAsyncTests<CRC7>
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

        protected override CRC7 CreateHashFunction(int hashSize)
        {
            return new CRC7();
        }
    }

    public class IHashFunctionTests_CRC7_ROHC
        : IHashFunctionAsyncTests<CRC7_ROHC>
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

        protected override CRC7_ROHC CreateHashFunction(int hashSize)
        {
            return new CRC7_ROHC();
        }
    }

    public class IHashFunctionTests_CRC8
        : IHashFunctionAsyncTests<CRC8>
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

        protected override CRC8 CreateHashFunction(int hashSize)
        {
            return new CRC8();
        }
    }

    public class IHashFunctionTests_CRC8_CDMA2000
        : IHashFunctionAsyncTests<CRC8_CDMA2000>
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

        protected override CRC8_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC8_CDMA2000();
        }
    }

    public class IHashFunctionTests_CRC8_DARC
        : IHashFunctionAsyncTests<CRC8_DARC>
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

        protected override CRC8_DARC CreateHashFunction(int hashSize)
        {
            return new CRC8_DARC();
        }
    }

    public class IHashFunctionTests_CRC8_DVBS2
        : IHashFunctionAsyncTests<CRC8_DVBS2>
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

        protected override CRC8_DVBS2 CreateHashFunction(int hashSize)
        {
            return new CRC8_DVBS2();
        }
    }

    public class IHashFunctionTests_CRC8_EBU
        : IHashFunctionAsyncTests<CRC8_EBU>
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

        protected override CRC8_EBU CreateHashFunction(int hashSize)
        {
            return new CRC8_EBU();
        }
    }

    public class IHashFunctionTests_CRC8_ICODE
        : IHashFunctionAsyncTests<CRC8_ICODE>
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

        protected override CRC8_ICODE CreateHashFunction(int hashSize)
        {
            return new CRC8_ICODE();
        }
    }

    public class IHashFunctionTests_CRC8_ITU
        : IHashFunctionAsyncTests<CRC8_ITU>
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

        protected override CRC8_ITU CreateHashFunction(int hashSize)
        {
            return new CRC8_ITU();
        }
    }

    public class IHashFunctionTests_CRC8_MAXIM
        : IHashFunctionAsyncTests<CRC8_MAXIM>
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

        protected override CRC8_MAXIM CreateHashFunction(int hashSize)
        {
            return new CRC8_MAXIM();
        }
    }

    public class IHashFunctionTests_CRC8_ROHC
        : IHashFunctionAsyncTests<CRC8_ROHC>
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

        protected override CRC8_ROHC CreateHashFunction(int hashSize)
        {
            return new CRC8_ROHC();
        }
    }

    public class IHashFunctionTests_CRC8_WCDMA
        : IHashFunctionAsyncTests<CRC8_WCDMA>
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

        protected override CRC8_WCDMA CreateHashFunction(int hashSize)
        {
            return new CRC8_WCDMA();
        }
    }

    public class IHashFunctionTests_CRC10
        : IHashFunctionAsyncTests<CRC10>
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

        protected override CRC10 CreateHashFunction(int hashSize)
        {
            return new CRC10();
        }
    }

    public class IHashFunctionTests_CRC10_CDMA2000
        : IHashFunctionAsyncTests<CRC10_CDMA2000>
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

        protected override CRC10_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC10_CDMA2000();
        }
    }

    public class IHashFunctionTests_CRC11
        : IHashFunctionAsyncTests<CRC11>
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

        protected override CRC11 CreateHashFunction(int hashSize)
        {
            return new CRC11();
        }
    }

    public class IHashFunctionTests_CRC12_3GPP
        : IHashFunctionAsyncTests<CRC12_3GPP>
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

        protected override CRC12_3GPP CreateHashFunction(int hashSize)
        {
            return new CRC12_3GPP();
        }
    }

    public class IHashFunctionTests_CRC12_CDMA2000
        : IHashFunctionAsyncTests<CRC12_CDMA2000>
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

        protected override CRC12_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC12_CDMA2000();
        }
    }

    public class IHashFunctionTests_CRC12_DECT
        : IHashFunctionAsyncTests<CRC12_DECT>
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

        protected override CRC12_DECT CreateHashFunction(int hashSize)
        {
            return new CRC12_DECT();
        }
    }

    public class IHashFunctionTests_CRC13_BBC
        : IHashFunctionAsyncTests<CRC13_BBC>
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

        protected override CRC13_BBC CreateHashFunction(int hashSize)
        {
            return new CRC13_BBC();
        }
    }

    public class IHashFunctionTests_CRC14_DARC
        : IHashFunctionAsyncTests<CRC14_DARC>
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

        protected override CRC14_DARC CreateHashFunction(int hashSize)
        {
            return new CRC14_DARC();
        }
    }

    public class IHashFunctionTests_CRC15
        : IHashFunctionAsyncTests<CRC15>
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

        protected override CRC15 CreateHashFunction(int hashSize)
        {
            return new CRC15();
        }
    }

    public class IHashFunctionTests_CRC15_MPT1327
        : IHashFunctionAsyncTests<CRC15_MPT1327>
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

        protected override CRC15_MPT1327 CreateHashFunction(int hashSize)
        {
            return new CRC15_MPT1327();
        }
    }

    public class IHashFunctionTests_ARC
        : IHashFunctionAsyncTests<ARC>
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

        protected override ARC CreateHashFunction(int hashSize)
        {
            return new ARC();
        }
    }

    public class IHashFunctionTests_CRC16_AUGCCITT
        : IHashFunctionAsyncTests<CRC16_AUGCCITT>
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

        protected override CRC16_AUGCCITT CreateHashFunction(int hashSize)
        {
            return new CRC16_AUGCCITT();
        }
    }

    public class IHashFunctionTests_CRC16_BUYPASS
        : IHashFunctionAsyncTests<CRC16_BUYPASS>
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

        protected override CRC16_BUYPASS CreateHashFunction(int hashSize)
        {
            return new CRC16_BUYPASS();
        }
    }

    public class IHashFunctionTests_CRC16_CCITTFALSE
        : IHashFunctionAsyncTests<CRC16_CCITTFALSE>
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

        protected override CRC16_CCITTFALSE CreateHashFunction(int hashSize)
        {
            return new CRC16_CCITTFALSE();
        }
    }

    public class IHashFunctionTests_CRC16_CDMA2000
        : IHashFunctionAsyncTests<CRC16_CDMA2000>
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

        protected override CRC16_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC16_CDMA2000();
        }
    }

    public class IHashFunctionTests_CRC16_DDS110
        : IHashFunctionAsyncTests<CRC16_DDS110>
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

        protected override CRC16_DDS110 CreateHashFunction(int hashSize)
        {
            return new CRC16_DDS110();
        }
    }

    public class IHashFunctionTests_CRC16_DECTR
        : IHashFunctionAsyncTests<CRC16_DECTR>
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

        protected override CRC16_DECTR CreateHashFunction(int hashSize)
        {
            return new CRC16_DECTR();
        }
    }

    public class IHashFunctionTests_CRC16_DECTX
        : IHashFunctionAsyncTests<CRC16_DECTX>
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

        protected override CRC16_DECTX CreateHashFunction(int hashSize)
        {
            return new CRC16_DECTX();
        }
    }

    public class IHashFunctionTests_CRC16_DNP
        : IHashFunctionAsyncTests<CRC16_DNP>
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

        protected override CRC16_DNP CreateHashFunction(int hashSize)
        {
            return new CRC16_DNP();
        }
    }

    public class IHashFunctionTests_CRC16_EN13757
        : IHashFunctionAsyncTests<CRC16_EN13757>
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

        protected override CRC16_EN13757 CreateHashFunction(int hashSize)
        {
            return new CRC16_EN13757();
        }
    }

    public class IHashFunctionTests_CRC16_GENIBUS
        : IHashFunctionAsyncTests<CRC16_GENIBUS>
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

        protected override CRC16_GENIBUS CreateHashFunction(int hashSize)
        {
            return new CRC16_GENIBUS();
        }
    }

    public class IHashFunctionTests_CRC16_MAXIM
        : IHashFunctionAsyncTests<CRC16_MAXIM>
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

        protected override CRC16_MAXIM CreateHashFunction(int hashSize)
        {
            return new CRC16_MAXIM();
        }
    }

    public class IHashFunctionTests_CRC16_MCRF4XX
        : IHashFunctionAsyncTests<CRC16_MCRF4XX>
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

        protected override CRC16_MCRF4XX CreateHashFunction(int hashSize)
        {
            return new CRC16_MCRF4XX();
        }
    }

    public class IHashFunctionTests_CRC16_RIELLO
        : IHashFunctionAsyncTests<CRC16_RIELLO>
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

        protected override CRC16_RIELLO CreateHashFunction(int hashSize)
        {
            return new CRC16_RIELLO();
        }
    }

    public class IHashFunctionTests_CRC16_T10DIF
        : IHashFunctionAsyncTests<CRC16_T10DIF>
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

        protected override CRC16_T10DIF CreateHashFunction(int hashSize)
        {
            return new CRC16_T10DIF();
        }
    }

    public class IHashFunctionTests_CRC16_TELEDISK
        : IHashFunctionAsyncTests<CRC16_TELEDISK>
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

        protected override CRC16_TELEDISK CreateHashFunction(int hashSize)
        {
            return new CRC16_TELEDISK();
        }
    }

    public class IHashFunctionTests_CRC16_TMS37157
        : IHashFunctionAsyncTests<CRC16_TMS37157>
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

        protected override CRC16_TMS37157 CreateHashFunction(int hashSize)
        {
            return new CRC16_TMS37157();
        }
    }

    public class IHashFunctionTests_CRC16_USB
        : IHashFunctionAsyncTests<CRC16_USB>
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

        protected override CRC16_USB CreateHashFunction(int hashSize)
        {
            return new CRC16_USB();
        }
    }

    public class IHashFunctionTests_CRCA
        : IHashFunctionAsyncTests<CRCA>
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

        protected override CRCA CreateHashFunction(int hashSize)
        {
            return new CRCA();
        }
    }

    public class IHashFunctionTests_KERMIT
        : IHashFunctionAsyncTests<KERMIT>
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

        protected override KERMIT CreateHashFunction(int hashSize)
        {
            return new KERMIT();
        }
    }

    public class IHashFunctionTests_MODBUS
        : IHashFunctionAsyncTests<MODBUS>
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

        protected override MODBUS CreateHashFunction(int hashSize)
        {
            return new MODBUS();
        }
    }

    public class IHashFunctionTests_X25
        : IHashFunctionAsyncTests<X25>
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

        protected override X25 CreateHashFunction(int hashSize)
        {
            return new X25();
        }
    }

    public class IHashFunctionTests_XMODEM
        : IHashFunctionAsyncTests<XMODEM>
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

        protected override XMODEM CreateHashFunction(int hashSize)
        {
            return new XMODEM();
        }
    }

    public class IHashFunctionTests_CRC24
        : IHashFunctionAsyncTests<CRC24>
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

        protected override CRC24 CreateHashFunction(int hashSize)
        {
            return new CRC24();
        }
    }

    public class IHashFunctionTests_CRC24_FLEXRAYA
        : IHashFunctionAsyncTests<CRC24_FLEXRAYA>
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

        protected override CRC24_FLEXRAYA CreateHashFunction(int hashSize)
        {
            return new CRC24_FLEXRAYA();
        }
    }

    public class IHashFunctionTests_CRC24_FLEXRAYB
        : IHashFunctionAsyncTests<CRC24_FLEXRAYB>
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

        protected override CRC24_FLEXRAYB CreateHashFunction(int hashSize)
        {
            return new CRC24_FLEXRAYB();
        }
    }

    public class IHashFunctionTests_CRC31_PHILIPS
        : IHashFunctionAsyncTests<CRC31_PHILIPS>
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

        protected override CRC31_PHILIPS CreateHashFunction(int hashSize)
        {
            return new CRC31_PHILIPS();
        }
    }

    public class IHashFunctionTests_CRC32
        : IHashFunctionAsyncTests<CRC32>
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

        protected override CRC32 CreateHashFunction(int hashSize)
        {
            return new CRC32();
        }
    }

    public class IHashFunctionTests_CRC32_BZIP2
        : IHashFunctionAsyncTests<CRC32_BZIP2>
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

        protected override CRC32_BZIP2 CreateHashFunction(int hashSize)
        {
            return new CRC32_BZIP2();
        }
    }

    public class IHashFunctionTests_CRC32C
        : IHashFunctionAsyncTests<CRC32C>
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

        protected override CRC32C CreateHashFunction(int hashSize)
        {
            return new CRC32C();
        }
    }

    public class IHashFunctionTests_CRC32D
        : IHashFunctionAsyncTests<CRC32D>
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

        protected override CRC32D CreateHashFunction(int hashSize)
        {
            return new CRC32D();
        }
    }

    public class IHashFunctionTests_CRC32_MPEG2
        : IHashFunctionAsyncTests<CRC32_MPEG2>
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

        protected override CRC32_MPEG2 CreateHashFunction(int hashSize)
        {
            return new CRC32_MPEG2();
        }
    }

    public class IHashFunctionTests_CRC32_POSIX
        : IHashFunctionAsyncTests<CRC32_POSIX>
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

        protected override CRC32_POSIX CreateHashFunction(int hashSize)
        {
            return new CRC32_POSIX();
        }
    }

    public class IHashFunctionTests_CRC32Q
        : IHashFunctionAsyncTests<CRC32Q>
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

        protected override CRC32Q CreateHashFunction(int hashSize)
        {
            return new CRC32Q();
        }
    }

    public class IHashFunctionTests_JAMCRC
        : IHashFunctionAsyncTests<JAMCRC>
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

        protected override JAMCRC CreateHashFunction(int hashSize)
        {
            return new JAMCRC();
        }
    }

    public class IHashFunctionTests_XFER
        : IHashFunctionAsyncTests<XFER>
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

        protected override XFER CreateHashFunction(int hashSize)
        {
            return new XFER();
        }
    }

    public class IHashFunctionTests_CRC40_GSM
        : IHashFunctionAsyncTests<CRC40_GSM>
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

        protected override CRC40_GSM CreateHashFunction(int hashSize)
        {
            return new CRC40_GSM();
        }
    }

    public class IHashFunctionTests_CRC64
        : IHashFunctionAsyncTests<CRC64>
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

        protected override CRC64 CreateHashFunction(int hashSize)
        {
            return new CRC64();
        }
    }

    public class IHashFunctionTests_CRC64_WE
        : IHashFunctionAsyncTests<CRC64_WE>
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

        protected override CRC64_WE CreateHashFunction(int hashSize)
        {
            return new CRC64_WE();
        }
    }

    public class IHashFunctionTests_CRC64_XZ
        : IHashFunctionAsyncTests<CRC64_XZ>
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

        protected override CRC64_XZ CreateHashFunction(int hashSize)
        {
            return new CRC64_XZ();
        }
    }

}



