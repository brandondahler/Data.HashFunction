//! Automatically generated from CRCStandards.tt
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

        protected override CRC3_ROHC CreateHashFunction(int hashSize)
        {
            return new CRC3_ROHC();
        }

        protected override Mock<CRC3_ROHC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC3_ROHC>();
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

        protected override CRC4_ITU CreateHashFunction(int hashSize)
        {
            return new CRC4_ITU();
        }

        protected override Mock<CRC4_ITU> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC4_ITU>();
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

        protected override CRC5_EPC CreateHashFunction(int hashSize)
        {
            return new CRC5_EPC();
        }

        protected override Mock<CRC5_EPC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC5_EPC>();
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

        protected override CRC5_ITU CreateHashFunction(int hashSize)
        {
            return new CRC5_ITU();
        }

        protected override Mock<CRC5_ITU> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC5_ITU>();
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

        protected override CRC5_USB CreateHashFunction(int hashSize)
        {
            return new CRC5_USB();
        }

        protected override Mock<CRC5_USB> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC5_USB>();
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

        protected override CRC6_CDMA2000A CreateHashFunction(int hashSize)
        {
            return new CRC6_CDMA2000A();
        }

        protected override Mock<CRC6_CDMA2000A> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC6_CDMA2000A>();
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

        protected override CRC6_CDMA2000B CreateHashFunction(int hashSize)
        {
            return new CRC6_CDMA2000B();
        }

        protected override Mock<CRC6_CDMA2000B> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC6_CDMA2000B>();
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

        protected override CRC6_DARC CreateHashFunction(int hashSize)
        {
            return new CRC6_DARC();
        }

        protected override Mock<CRC6_DARC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC6_DARC>();
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

        protected override CRC6_ITU CreateHashFunction(int hashSize)
        {
            return new CRC6_ITU();
        }

        protected override Mock<CRC6_ITU> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC6_ITU>();
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

        protected override CRC7 CreateHashFunction(int hashSize)
        {
            return new CRC7();
        }

        protected override Mock<CRC7> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC7>();
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

        protected override CRC7_ROHC CreateHashFunction(int hashSize)
        {
            return new CRC7_ROHC();
        }

        protected override Mock<CRC7_ROHC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC7_ROHC>();
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

        protected override CRC8 CreateHashFunction(int hashSize)
        {
            return new CRC8();
        }

        protected override Mock<CRC8> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8>();
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

        protected override CRC8_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC8_CDMA2000();
        }

        protected override Mock<CRC8_CDMA2000> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_CDMA2000>();
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

        protected override CRC8_DARC CreateHashFunction(int hashSize)
        {
            return new CRC8_DARC();
        }

        protected override Mock<CRC8_DARC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_DARC>();
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

        protected override CRC8_DVBS2 CreateHashFunction(int hashSize)
        {
            return new CRC8_DVBS2();
        }

        protected override Mock<CRC8_DVBS2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_DVBS2>();
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

        protected override CRC8_EBU CreateHashFunction(int hashSize)
        {
            return new CRC8_EBU();
        }

        protected override Mock<CRC8_EBU> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_EBU>();
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

        protected override CRC8_ICODE CreateHashFunction(int hashSize)
        {
            return new CRC8_ICODE();
        }

        protected override Mock<CRC8_ICODE> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_ICODE>();
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

        protected override CRC8_ITU CreateHashFunction(int hashSize)
        {
            return new CRC8_ITU();
        }

        protected override Mock<CRC8_ITU> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_ITU>();
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

        protected override CRC8_MAXIM CreateHashFunction(int hashSize)
        {
            return new CRC8_MAXIM();
        }

        protected override Mock<CRC8_MAXIM> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_MAXIM>();
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

        protected override CRC8_ROHC CreateHashFunction(int hashSize)
        {
            return new CRC8_ROHC();
        }

        protected override Mock<CRC8_ROHC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_ROHC>();
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

        protected override CRC8_WCDMA CreateHashFunction(int hashSize)
        {
            return new CRC8_WCDMA();
        }

        protected override Mock<CRC8_WCDMA> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC8_WCDMA>();
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

        protected override CRC10 CreateHashFunction(int hashSize)
        {
            return new CRC10();
        }

        protected override Mock<CRC10> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC10>();
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

        protected override CRC10_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC10_CDMA2000();
        }

        protected override Mock<CRC10_CDMA2000> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC10_CDMA2000>();
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

        protected override CRC11 CreateHashFunction(int hashSize)
        {
            return new CRC11();
        }

        protected override Mock<CRC11> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC11>();
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

        protected override CRC12_3GPP CreateHashFunction(int hashSize)
        {
            return new CRC12_3GPP();
        }

        protected override Mock<CRC12_3GPP> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC12_3GPP>();
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

        protected override CRC12_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC12_CDMA2000();
        }

        protected override Mock<CRC12_CDMA2000> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC12_CDMA2000>();
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

        protected override CRC12_DECT CreateHashFunction(int hashSize)
        {
            return new CRC12_DECT();
        }

        protected override Mock<CRC12_DECT> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC12_DECT>();
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

        protected override CRC13_BBC CreateHashFunction(int hashSize)
        {
            return new CRC13_BBC();
        }

        protected override Mock<CRC13_BBC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC13_BBC>();
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

        protected override CRC14_DARC CreateHashFunction(int hashSize)
        {
            return new CRC14_DARC();
        }

        protected override Mock<CRC14_DARC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC14_DARC>();
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

        protected override CRC15 CreateHashFunction(int hashSize)
        {
            return new CRC15();
        }

        protected override Mock<CRC15> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC15>();
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

        protected override CRC15_MPT1327 CreateHashFunction(int hashSize)
        {
            return new CRC15_MPT1327();
        }

        protected override Mock<CRC15_MPT1327> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC15_MPT1327>();
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

        protected override ARC CreateHashFunction(int hashSize)
        {
            return new ARC();
        }

        protected override Mock<ARC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<ARC>();
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

        protected override CRC16_AUGCCITT CreateHashFunction(int hashSize)
        {
            return new CRC16_AUGCCITT();
        }

        protected override Mock<CRC16_AUGCCITT> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_AUGCCITT>();
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

        protected override CRC16_BUYPASS CreateHashFunction(int hashSize)
        {
            return new CRC16_BUYPASS();
        }

        protected override Mock<CRC16_BUYPASS> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_BUYPASS>();
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

        protected override CRC16_CCITTFALSE CreateHashFunction(int hashSize)
        {
            return new CRC16_CCITTFALSE();
        }

        protected override Mock<CRC16_CCITTFALSE> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_CCITTFALSE>();
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

        protected override CRC16_CDMA2000 CreateHashFunction(int hashSize)
        {
            return new CRC16_CDMA2000();
        }

        protected override Mock<CRC16_CDMA2000> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_CDMA2000>();
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

        protected override CRC16_DDS110 CreateHashFunction(int hashSize)
        {
            return new CRC16_DDS110();
        }

        protected override Mock<CRC16_DDS110> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_DDS110>();
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

        protected override CRC16_DECTR CreateHashFunction(int hashSize)
        {
            return new CRC16_DECTR();
        }

        protected override Mock<CRC16_DECTR> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_DECTR>();
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

        protected override CRC16_DECTX CreateHashFunction(int hashSize)
        {
            return new CRC16_DECTX();
        }

        protected override Mock<CRC16_DECTX> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_DECTX>();
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

        protected override CRC16_DNP CreateHashFunction(int hashSize)
        {
            return new CRC16_DNP();
        }

        protected override Mock<CRC16_DNP> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_DNP>();
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

        protected override CRC16_EN13757 CreateHashFunction(int hashSize)
        {
            return new CRC16_EN13757();
        }

        protected override Mock<CRC16_EN13757> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_EN13757>();
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

        protected override CRC16_GENIBUS CreateHashFunction(int hashSize)
        {
            return new CRC16_GENIBUS();
        }

        protected override Mock<CRC16_GENIBUS> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_GENIBUS>();
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

        protected override CRC16_MAXIM CreateHashFunction(int hashSize)
        {
            return new CRC16_MAXIM();
        }

        protected override Mock<CRC16_MAXIM> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_MAXIM>();
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

        protected override CRC16_MCRF4XX CreateHashFunction(int hashSize)
        {
            return new CRC16_MCRF4XX();
        }

        protected override Mock<CRC16_MCRF4XX> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_MCRF4XX>();
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

        protected override CRC16_RIELLO CreateHashFunction(int hashSize)
        {
            return new CRC16_RIELLO();
        }

        protected override Mock<CRC16_RIELLO> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_RIELLO>();
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

        protected override CRC16_T10DIF CreateHashFunction(int hashSize)
        {
            return new CRC16_T10DIF();
        }

        protected override Mock<CRC16_T10DIF> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_T10DIF>();
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

        protected override CRC16_TELEDISK CreateHashFunction(int hashSize)
        {
            return new CRC16_TELEDISK();
        }

        protected override Mock<CRC16_TELEDISK> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_TELEDISK>();
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

        protected override CRC16_TMS37157 CreateHashFunction(int hashSize)
        {
            return new CRC16_TMS37157();
        }

        protected override Mock<CRC16_TMS37157> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_TMS37157>();
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

        protected override CRC16_USB CreateHashFunction(int hashSize)
        {
            return new CRC16_USB();
        }

        protected override Mock<CRC16_USB> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC16_USB>();
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

        protected override CRCA CreateHashFunction(int hashSize)
        {
            return new CRCA();
        }

        protected override Mock<CRCA> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRCA>();
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

        protected override KERMIT CreateHashFunction(int hashSize)
        {
            return new KERMIT();
        }

        protected override Mock<KERMIT> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<KERMIT>();
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

        protected override MODBUS CreateHashFunction(int hashSize)
        {
            return new MODBUS();
        }

        protected override Mock<MODBUS> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<MODBUS>();
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

        protected override X25 CreateHashFunction(int hashSize)
        {
            return new X25();
        }

        protected override Mock<X25> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<X25>();
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

        protected override XMODEM CreateHashFunction(int hashSize)
        {
            return new XMODEM();
        }

        protected override Mock<XMODEM> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<XMODEM>();
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

        protected override CRC24 CreateHashFunction(int hashSize)
        {
            return new CRC24();
        }

        protected override Mock<CRC24> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC24>();
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

        protected override CRC24_FLEXRAYA CreateHashFunction(int hashSize)
        {
            return new CRC24_FLEXRAYA();
        }

        protected override Mock<CRC24_FLEXRAYA> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC24_FLEXRAYA>();
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

        protected override CRC24_FLEXRAYB CreateHashFunction(int hashSize)
        {
            return new CRC24_FLEXRAYB();
        }

        protected override Mock<CRC24_FLEXRAYB> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC24_FLEXRAYB>();
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

        protected override CRC31_PHILIPS CreateHashFunction(int hashSize)
        {
            return new CRC31_PHILIPS();
        }

        protected override Mock<CRC31_PHILIPS> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC31_PHILIPS>();
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

        protected override CRC32 CreateHashFunction(int hashSize)
        {
            return new CRC32();
        }

        protected override Mock<CRC32> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC32>();
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

        protected override CRC32_BZIP2 CreateHashFunction(int hashSize)
        {
            return new CRC32_BZIP2();
        }

        protected override Mock<CRC32_BZIP2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC32_BZIP2>();
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

        protected override CRC32C CreateHashFunction(int hashSize)
        {
            return new CRC32C();
        }

        protected override Mock<CRC32C> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC32C>();
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

        protected override CRC32D CreateHashFunction(int hashSize)
        {
            return new CRC32D();
        }

        protected override Mock<CRC32D> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC32D>();
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

        protected override CRC32_MPEG2 CreateHashFunction(int hashSize)
        {
            return new CRC32_MPEG2();
        }

        protected override Mock<CRC32_MPEG2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC32_MPEG2>();
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

        protected override CRC32_POSIX CreateHashFunction(int hashSize)
        {
            return new CRC32_POSIX();
        }

        protected override Mock<CRC32_POSIX> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC32_POSIX>();
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

        protected override CRC32Q CreateHashFunction(int hashSize)
        {
            return new CRC32Q();
        }

        protected override Mock<CRC32Q> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC32Q>();
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

        protected override JAMCRC CreateHashFunction(int hashSize)
        {
            return new JAMCRC();
        }

        protected override Mock<JAMCRC> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JAMCRC>();
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

        protected override XFER CreateHashFunction(int hashSize)
        {
            return new XFER();
        }

        protected override Mock<XFER> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<XFER>();
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

        protected override CRC40_GSM CreateHashFunction(int hashSize)
        {
            return new CRC40_GSM();
        }

        protected override Mock<CRC40_GSM> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC40_GSM>();
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

        protected override CRC64 CreateHashFunction(int hashSize)
        {
            return new CRC64();
        }

        protected override Mock<CRC64> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC64>();
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

        protected override CRC64_WE CreateHashFunction(int hashSize)
        {
            return new CRC64_WE();
        }

        protected override Mock<CRC64_WE> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC64_WE>();
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

        protected override CRC64_XZ CreateHashFunction(int hashSize)
        {
            return new CRC64_XZ();
        }

        protected override Mock<CRC64_XZ> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CRC64_XZ>();
        }
    }

}



