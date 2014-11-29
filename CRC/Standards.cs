//! Automatically generated from CRCStandards.tt
//! Direct modifications to this file will be lost.
//!
//! A vast majority if not all of the parameters for these standards 
//!   were provided by http://reveng.sourceforge.net/crc-catalogue/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public partial class CRC
    {
        /// <summary>
        /// Enumeration of all defined and implemented CRC standards.
        /// </summary>
        public enum Standard
        {

            /// <summary>
            /// CRC standard named "CRC3_ROHC".
            /// </summary>
            CRC3_ROHC,

            /// <summary>
            /// CRC standard named "CRC4_ITU".
            /// </summary>
            CRC4_ITU,

            /// <summary>
            /// CRC standard named "CRC5_EPC".
            /// </summary>
            CRC5_EPC,

            /// <summary>
            /// CRC standard named "CRC5_ITU".
            /// </summary>
            CRC5_ITU,

            /// <summary>
            /// CRC standard named "CRC5_USB".
            /// </summary>
            CRC5_USB,

            /// <summary>
            /// CRC standard named "CRC6_CDMA2000A".
            /// </summary>
            CRC6_CDMA2000A,

            /// <summary>
            /// CRC standard named "CRC6_CDMA2000B".
            /// </summary>
            CRC6_CDMA2000B,

            /// <summary>
            /// CRC standard named "CRC6_DARC".
            /// </summary>
            CRC6_DARC,

            /// <summary>
            /// CRC standard named "CRC6_ITU".
            /// </summary>
            CRC6_ITU,

            /// <summary>
            /// CRC standard named "CRC7".
            /// </summary>
            CRC7,

            /// <summary>
            /// CRC standard named "CRC7_ROHC".
            /// </summary>
            CRC7_ROHC,

            /// <summary>
            /// CRC standard named "CRC8".
            /// </summary>
            CRC8,

            /// <summary>
            /// CRC standard named "CRC8_CDMA2000".
            /// </summary>
            CRC8_CDMA2000,

            /// <summary>
            /// CRC standard named "CRC8_DARC".
            /// </summary>
            CRC8_DARC,

            /// <summary>
            /// CRC standard named "CRC8_DVBS2".
            /// </summary>
            CRC8_DVBS2,

            /// <summary>
            /// CRC standard named "CRC8_EBU".
            /// </summary>
            CRC8_EBU,

            /// <summary>
            /// CRC standard named "CRC8_ICODE".
            /// </summary>
            CRC8_ICODE,

            /// <summary>
            /// CRC standard named "CRC8_ITU".
            /// </summary>
            CRC8_ITU,

            /// <summary>
            /// CRC standard named "CRC8_MAXIM".
            /// </summary>
            CRC8_MAXIM,

            /// <summary>
            /// CRC standard named "CRC8_ROHC".
            /// </summary>
            CRC8_ROHC,

            /// <summary>
            /// CRC standard named "CRC8_WCDMA".
            /// </summary>
            CRC8_WCDMA,

            /// <summary>
            /// CRC standard named "CRC10".
            /// </summary>
            CRC10,

            /// <summary>
            /// CRC standard named "CRC10_CDMA2000".
            /// </summary>
            CRC10_CDMA2000,

            /// <summary>
            /// CRC standard named "CRC11".
            /// </summary>
            CRC11,

            /// <summary>
            /// CRC standard named "CRC12_3GPP".
            /// </summary>
            CRC12_3GPP,

            /// <summary>
            /// CRC standard named "CRC12_CDMA2000".
            /// </summary>
            CRC12_CDMA2000,

            /// <summary>
            /// CRC standard named "CRC12_DECT".
            /// </summary>
            CRC12_DECT,

            /// <summary>
            /// CRC standard named "CRC13_BBC".
            /// </summary>
            CRC13_BBC,

            /// <summary>
            /// CRC standard named "CRC14_DARC".
            /// </summary>
            CRC14_DARC,

            /// <summary>
            /// CRC standard named "CRC15".
            /// </summary>
            CRC15,

            /// <summary>
            /// CRC standard named "CRC15_MPT1327".
            /// </summary>
            CRC15_MPT1327,

            /// <summary>
            /// CRC standard named "ARC".
            /// </summary>
            ARC,

            /// <summary>
            /// CRC standard named "CRC16_AUGCCITT".
            /// </summary>
            CRC16_AUGCCITT,

            /// <summary>
            /// CRC standard named "CRC16_BUYPASS".
            /// </summary>
            CRC16_BUYPASS,

            /// <summary>
            /// CRC standard named "CRC16_CCITTFALSE".
            /// </summary>
            CRC16_CCITTFALSE,

            /// <summary>
            /// CRC standard named "CRC16_CDMA2000".
            /// </summary>
            CRC16_CDMA2000,

            /// <summary>
            /// CRC standard named "CRC16_DDS110".
            /// </summary>
            CRC16_DDS110,

            /// <summary>
            /// CRC standard named "CRC16_DECTR".
            /// </summary>
            CRC16_DECTR,

            /// <summary>
            /// CRC standard named "CRC16_DECTX".
            /// </summary>
            CRC16_DECTX,

            /// <summary>
            /// CRC standard named "CRC16_DNP".
            /// </summary>
            CRC16_DNP,

            /// <summary>
            /// CRC standard named "CRC16_EN13757".
            /// </summary>
            CRC16_EN13757,

            /// <summary>
            /// CRC standard named "CRC16_GENIBUS".
            /// </summary>
            CRC16_GENIBUS,

            /// <summary>
            /// CRC standard named "CRC16_MAXIM".
            /// </summary>
            CRC16_MAXIM,

            /// <summary>
            /// CRC standard named "CRC16_MCRF4XX".
            /// </summary>
            CRC16_MCRF4XX,

            /// <summary>
            /// CRC standard named "CRC16_RIELLO".
            /// </summary>
            CRC16_RIELLO,

            /// <summary>
            /// CRC standard named "CRC16_T10DIF".
            /// </summary>
            CRC16_T10DIF,

            /// <summary>
            /// CRC standard named "CRC16_TELEDISK".
            /// </summary>
            CRC16_TELEDISK,

            /// <summary>
            /// CRC standard named "CRC16_TMS37157".
            /// </summary>
            CRC16_TMS37157,

            /// <summary>
            /// CRC standard named "CRC16_USB".
            /// </summary>
            CRC16_USB,

            /// <summary>
            /// CRC standard named "CRCA".
            /// </summary>
            CRCA,

            /// <summary>
            /// CRC standard named "KERMIT".
            /// </summary>
            KERMIT,

            /// <summary>
            /// CRC standard named "MODBUS".
            /// </summary>
            MODBUS,

            /// <summary>
            /// CRC standard named "X25".
            /// </summary>
            X25,

            /// <summary>
            /// CRC standard named "XMODEM".
            /// </summary>
            XMODEM,

            /// <summary>
            /// CRC standard named "CRC24".
            /// </summary>
            CRC24,

            /// <summary>
            /// CRC standard named "CRC24_FLEXRAYA".
            /// </summary>
            CRC24_FLEXRAYA,

            /// <summary>
            /// CRC standard named "CRC24_FLEXRAYB".
            /// </summary>
            CRC24_FLEXRAYB,

            /// <summary>
            /// CRC standard named "CRC31_PHILIPS".
            /// </summary>
            CRC31_PHILIPS,

            /// <summary>
            /// CRC standard named "CRC32".
            /// </summary>
            CRC32,

            /// <summary>
            /// CRC standard named "CRC32_BZIP2".
            /// </summary>
            CRC32_BZIP2,

            /// <summary>
            /// CRC standard named "CRC32C".
            /// </summary>
            CRC32C,

            /// <summary>
            /// CRC standard named "CRC32D".
            /// </summary>
            CRC32D,

            /// <summary>
            /// CRC standard named "CRC32_MPEG2".
            /// </summary>
            CRC32_MPEG2,

            /// <summary>
            /// CRC standard named "CRC32_POSIX".
            /// </summary>
            CRC32_POSIX,

            /// <summary>
            /// CRC standard named "CRC32Q".
            /// </summary>
            CRC32Q,

            /// <summary>
            /// CRC standard named "JAMCRC".
            /// </summary>
            JAMCRC,

            /// <summary>
            /// CRC standard named "XFER".
            /// </summary>
            XFER,

            /// <summary>
            /// CRC standard named "CRC40_GSM".
            /// </summary>
            CRC40_GSM,

            /// <summary>
            /// CRC standard named "CRC64".
            /// </summary>
            CRC64,

            /// <summary>
            /// CRC standard named "CRC64_WE".
            /// </summary>
            CRC64_WE,

            /// <summary>
            /// CRC standard named "CRC64_XZ".
            /// </summary>
            CRC64_XZ,

        }

        /// <summary>
        /// Dictionary of Settings for each of the defined and implemented CRC standards.
        /// </summary>
#if NET45
        public static readonly IReadOnlyDictionary<Standard, Setting> Standards = 
#else
        public static readonly IDictionary<Standard, Setting> Standards = 
#endif
            new Dictionary<Standard, Setting>() {
                { Standard.CRC3_ROHC,         new Setting( 3,                0x3,                0x7,  true,  true,                0x0 ) },
                { Standard.CRC4_ITU,          new Setting( 4,                0x3,                0x0,  true,  true,                0x0 ) },
                { Standard.CRC5_EPC,          new Setting( 5,               0x09,               0x09, false, false,               0x00 ) },
                { Standard.CRC5_ITU,          new Setting( 5,               0x15,               0x00,  true,  true,               0x00 ) },
                { Standard.CRC5_USB,          new Setting( 5,               0x05,               0x1f,  true,  true,               0x1f ) },
                { Standard.CRC6_CDMA2000A,    new Setting( 6,               0x27,               0x3f, false, false,               0x00 ) },
                { Standard.CRC6_CDMA2000B,    new Setting( 6,               0x07,               0x3f, false, false,               0x00 ) },
                { Standard.CRC6_DARC,         new Setting( 6,               0x19,               0x00,  true,  true,               0x00 ) },
                { Standard.CRC6_ITU,          new Setting( 6,               0x03,               0x00,  true,  true,               0x00 ) },
                { Standard.CRC7,              new Setting( 7,               0x09,               0x00, false, false,               0x00 ) },
                { Standard.CRC7_ROHC,         new Setting( 7,               0x4f,               0x7f,  true,  true,               0x00 ) },
                { Standard.CRC8,              new Setting( 8,               0x07,               0x00, false, false,               0x00 ) },
                { Standard.CRC8_CDMA2000,     new Setting( 8,               0x9b,               0xff, false, false,               0x00 ) },
                { Standard.CRC8_DARC,         new Setting( 8,               0x39,               0x00,  true,  true,               0x00 ) },
                { Standard.CRC8_DVBS2,        new Setting( 8,               0xd5,               0x00, false, false,               0x00 ) },
                { Standard.CRC8_EBU,          new Setting( 8,               0x1d,               0xff,  true,  true,               0x00 ) },
                { Standard.CRC8_ICODE,        new Setting( 8,               0x1d,               0xfd, false, false,               0x00 ) },
                { Standard.CRC8_ITU,          new Setting( 8,               0x07,               0x00, false, false,               0x55 ) },
                { Standard.CRC8_MAXIM,        new Setting( 8,               0x31,               0x00,  true,  true,               0x00 ) },
                { Standard.CRC8_ROHC,         new Setting( 8,               0x07,               0xff,  true,  true,               0x00 ) },
                { Standard.CRC8_WCDMA,        new Setting( 8,               0x9b,               0x00,  true,  true,               0x00 ) },
                { Standard.CRC10,             new Setting(10,              0x233,              0x000, false, false,              0x000 ) },
                { Standard.CRC10_CDMA2000,    new Setting(10,              0x3d9,              0x3ff, false, false,              0x000 ) },
                { Standard.CRC11,             new Setting(11,              0x385,              0x01a, false, false,              0x000 ) },
                { Standard.CRC12_3GPP,        new Setting(12,              0x80f,              0x000, false,  true,              0x000 ) },
                { Standard.CRC12_CDMA2000,    new Setting(12,              0xf13,              0xfff, false, false,              0x000 ) },
                { Standard.CRC12_DECT,        new Setting(12,              0x80f,              0x000, false, false,              0x000 ) },
                { Standard.CRC13_BBC,         new Setting(13,             0x1cf5,             0x0000, false, false,             0x0000 ) },
                { Standard.CRC14_DARC,        new Setting(14,             0x0805,             0x0000,  true,  true,             0x0000 ) },
                { Standard.CRC15,             new Setting(15,             0x4599,             0x0000, false, false,             0x0000 ) },
                { Standard.CRC15_MPT1327,     new Setting(15,             0x6815,             0x0000, false, false,             0x0001 ) },
                { Standard.ARC,               new Setting(16,             0x8005,             0x0000,  true,  true,             0x0000 ) },
                { Standard.CRC16_AUGCCITT,    new Setting(16,             0x1021,             0x1d0f, false, false,             0x0000 ) },
                { Standard.CRC16_BUYPASS,     new Setting(16,             0x8005,             0x0000, false, false,             0x0000 ) },
                { Standard.CRC16_CCITTFALSE,  new Setting(16,             0x1021,             0xffff, false, false,             0x0000 ) },
                { Standard.CRC16_CDMA2000,    new Setting(16,             0xc867,             0xffff, false, false,             0x0000 ) },
                { Standard.CRC16_DDS110,      new Setting(16,             0x8005,             0x800d, false, false,             0x0000 ) },
                { Standard.CRC16_DECTR,       new Setting(16,             0x0589,             0x0000, false, false,             0x0001 ) },
                { Standard.CRC16_DECTX,       new Setting(16,             0x0589,             0x0000, false, false,             0x0000 ) },
                { Standard.CRC16_DNP,         new Setting(16,             0x3d65,             0x0000,  true,  true,             0xffff ) },
                { Standard.CRC16_EN13757,     new Setting(16,             0x3d65,             0x0000, false, false,             0xffff ) },
                { Standard.CRC16_GENIBUS,     new Setting(16,             0x1021,             0xffff, false, false,             0xffff ) },
                { Standard.CRC16_MAXIM,       new Setting(16,             0x8005,             0x0000,  true,  true,             0xffff ) },
                { Standard.CRC16_MCRF4XX,     new Setting(16,             0x1021,             0xffff,  true,  true,             0x0000 ) },
                { Standard.CRC16_RIELLO,      new Setting(16,             0x1021,             0xb2aa,  true,  true,             0x0000 ) },
                { Standard.CRC16_T10DIF,      new Setting(16,             0x8bb7,             0x0000, false, false,             0x0000 ) },
                { Standard.CRC16_TELEDISK,    new Setting(16,             0xa097,             0x0000, false, false,             0x0000 ) },
                { Standard.CRC16_TMS37157,    new Setting(16,             0x1021,             0x89ec,  true,  true,             0x0000 ) },
                { Standard.CRC16_USB,         new Setting(16,             0x8005,             0xffff,  true,  true,             0xffff ) },
                { Standard.CRCA,              new Setting(16,             0x1021,             0xc6c6,  true,  true,             0x0000 ) },
                { Standard.KERMIT,            new Setting(16,             0x1021,             0x0000,  true,  true,             0x0000 ) },
                { Standard.MODBUS,            new Setting(16,             0x8005,             0xffff,  true,  true,             0x0000 ) },
                { Standard.X25,               new Setting(16,             0x1021,             0xffff,  true,  true,             0xffff ) },
                { Standard.XMODEM,            new Setting(16,             0x1021,             0x0000, false, false,             0x0000 ) },
                { Standard.CRC24,             new Setting(24,           0x864cfb,           0xb704ce, false, false,           0x000000 ) },
                { Standard.CRC24_FLEXRAYA,    new Setting(24,           0x5d6dcb,           0xfedcba, false, false,           0x000000 ) },
                { Standard.CRC24_FLEXRAYB,    new Setting(24,           0x5d6dcb,           0xabcdef, false, false,           0x000000 ) },
                { Standard.CRC31_PHILIPS,     new Setting(31,         0x04c11db7,         0x7fffffff, false, false,         0x7fffffff ) },
                { Standard.CRC32,             new Setting(32,         0x04c11db7,         0xffffffff,  true,  true,         0xffffffff ) },
                { Standard.CRC32_BZIP2,       new Setting(32,         0x04c11db7,         0xffffffff, false, false,         0xffffffff ) },
                { Standard.CRC32C,            new Setting(32,         0x1edc6f41,         0xffffffff,  true,  true,         0xffffffff ) },
                { Standard.CRC32D,            new Setting(32,         0xa833982b,         0xffffffff,  true,  true,         0xffffffff ) },
                { Standard.CRC32_MPEG2,       new Setting(32,         0x04c11db7,         0xffffffff, false, false,         0x00000000 ) },
                { Standard.CRC32_POSIX,       new Setting(32,         0x04c11db7,         0x00000000, false, false,         0xffffffff ) },
                { Standard.CRC32Q,            new Setting(32,         0x814141ab,         0x00000000, false, false,         0x00000000 ) },
                { Standard.JAMCRC,            new Setting(32,         0x04c11db7,         0xffffffff,  true,  true,         0x00000000 ) },
                { Standard.XFER,              new Setting(32,         0x000000af,         0x00000000, false, false,         0x00000000 ) },
                { Standard.CRC40_GSM,         new Setting(40,       0x0004820009,       0x0000000000, false, false,       0xffffffffff ) },
                { Standard.CRC64,             new Setting(64, 0x42f0e1eba9ea3693, 0x0000000000000000, false, false, 0x0000000000000000 ) },
                { Standard.CRC64_WE,          new Setting(64, 0x42f0e1eba9ea3693, 0xffffffffffffffff, false, false, 0xffffffffffffffff ) },
                { Standard.CRC64_XZ,          new Setting(64, 0x42f0e1eba9ea3693, 0xffffffffffffffff,  true,  true, 0xffffffffffffffff ) },
            };
    }

    namespace CRCStandards
    {
        /// <summary>
        /// Namespace containing all implemented CRC standards.
        /// </summary>
        internal static class NamespaceDoc
        {
            
        }

        /// <summary>
        /// Automatically generated implementation of CRC3_ROHC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC3_ROHC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC3_ROHC"/> class.
            /// </summary>
            public CRC3_ROHC()
                : base(CRC.Standards[CRC.Standard.CRC3_ROHC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC4_ITU CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC4_ITU
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC4_ITU"/> class.
            /// </summary>
            public CRC4_ITU()
                : base(CRC.Standards[CRC.Standard.CRC4_ITU])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC5_EPC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC5_EPC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC5_EPC"/> class.
            /// </summary>
            public CRC5_EPC()
                : base(CRC.Standards[CRC.Standard.CRC5_EPC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC5_ITU CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC5_ITU
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC5_ITU"/> class.
            /// </summary>
            public CRC5_ITU()
                : base(CRC.Standards[CRC.Standard.CRC5_ITU])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC5_USB CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC5_USB
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC5_USB"/> class.
            /// </summary>
            public CRC5_USB()
                : base(CRC.Standards[CRC.Standard.CRC5_USB])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC6_CDMA2000A CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC6_CDMA2000A
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC6_CDMA2000A"/> class.
            /// </summary>
            public CRC6_CDMA2000A()
                : base(CRC.Standards[CRC.Standard.CRC6_CDMA2000A])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC6_CDMA2000B CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC6_CDMA2000B
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC6_CDMA2000B"/> class.
            /// </summary>
            public CRC6_CDMA2000B()
                : base(CRC.Standards[CRC.Standard.CRC6_CDMA2000B])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC6_DARC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC6_DARC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC6_DARC"/> class.
            /// </summary>
            public CRC6_DARC()
                : base(CRC.Standards[CRC.Standard.CRC6_DARC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC6_ITU CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC6_ITU
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC6_ITU"/> class.
            /// </summary>
            public CRC6_ITU()
                : base(CRC.Standards[CRC.Standard.CRC6_ITU])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC7 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC7
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC7"/> class.
            /// </summary>
            public CRC7()
                : base(CRC.Standards[CRC.Standard.CRC7])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC7_ROHC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC7_ROHC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC7_ROHC"/> class.
            /// </summary>
            public CRC7_ROHC()
                : base(CRC.Standards[CRC.Standard.CRC7_ROHC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8"/> class.
            /// </summary>
            public CRC8()
                : base(CRC.Standards[CRC.Standard.CRC8])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_CDMA2000 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_CDMA2000
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_CDMA2000"/> class.
            /// </summary>
            public CRC8_CDMA2000()
                : base(CRC.Standards[CRC.Standard.CRC8_CDMA2000])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_DARC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_DARC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_DARC"/> class.
            /// </summary>
            public CRC8_DARC()
                : base(CRC.Standards[CRC.Standard.CRC8_DARC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_DVBS2 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_DVBS2
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_DVBS2"/> class.
            /// </summary>
            public CRC8_DVBS2()
                : base(CRC.Standards[CRC.Standard.CRC8_DVBS2])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_EBU CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_EBU
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_EBU"/> class.
            /// </summary>
            public CRC8_EBU()
                : base(CRC.Standards[CRC.Standard.CRC8_EBU])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_ICODE CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_ICODE
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_ICODE"/> class.
            /// </summary>
            public CRC8_ICODE()
                : base(CRC.Standards[CRC.Standard.CRC8_ICODE])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_ITU CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_ITU
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_ITU"/> class.
            /// </summary>
            public CRC8_ITU()
                : base(CRC.Standards[CRC.Standard.CRC8_ITU])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_MAXIM CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_MAXIM
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_MAXIM"/> class.
            /// </summary>
            public CRC8_MAXIM()
                : base(CRC.Standards[CRC.Standard.CRC8_MAXIM])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_ROHC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_ROHC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_ROHC"/> class.
            /// </summary>
            public CRC8_ROHC()
                : base(CRC.Standards[CRC.Standard.CRC8_ROHC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC8_WCDMA CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC8_WCDMA
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC8_WCDMA"/> class.
            /// </summary>
            public CRC8_WCDMA()
                : base(CRC.Standards[CRC.Standard.CRC8_WCDMA])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC10 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC10
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC10"/> class.
            /// </summary>
            public CRC10()
                : base(CRC.Standards[CRC.Standard.CRC10])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC10_CDMA2000 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC10_CDMA2000
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC10_CDMA2000"/> class.
            /// </summary>
            public CRC10_CDMA2000()
                : base(CRC.Standards[CRC.Standard.CRC10_CDMA2000])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC11 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC11
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC11"/> class.
            /// </summary>
            public CRC11()
                : base(CRC.Standards[CRC.Standard.CRC11])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC12_3GPP CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC12_3GPP
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC12_3GPP"/> class.
            /// </summary>
            public CRC12_3GPP()
                : base(CRC.Standards[CRC.Standard.CRC12_3GPP])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC12_CDMA2000 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC12_CDMA2000
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC12_CDMA2000"/> class.
            /// </summary>
            public CRC12_CDMA2000()
                : base(CRC.Standards[CRC.Standard.CRC12_CDMA2000])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC12_DECT CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC12_DECT
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC12_DECT"/> class.
            /// </summary>
            public CRC12_DECT()
                : base(CRC.Standards[CRC.Standard.CRC12_DECT])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC13_BBC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC13_BBC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC13_BBC"/> class.
            /// </summary>
            public CRC13_BBC()
                : base(CRC.Standards[CRC.Standard.CRC13_BBC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC14_DARC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC14_DARC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC14_DARC"/> class.
            /// </summary>
            public CRC14_DARC()
                : base(CRC.Standards[CRC.Standard.CRC14_DARC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC15 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC15
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC15"/> class.
            /// </summary>
            public CRC15()
                : base(CRC.Standards[CRC.Standard.CRC15])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC15_MPT1327 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC15_MPT1327
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC15_MPT1327"/> class.
            /// </summary>
            public CRC15_MPT1327()
                : base(CRC.Standards[CRC.Standard.CRC15_MPT1327])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of ARC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class ARC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ARC"/> class.
            /// </summary>
            public ARC()
                : base(CRC.Standards[CRC.Standard.ARC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_AUGCCITT CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_AUGCCITT
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_AUGCCITT"/> class.
            /// </summary>
            public CRC16_AUGCCITT()
                : base(CRC.Standards[CRC.Standard.CRC16_AUGCCITT])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_BUYPASS CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_BUYPASS
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_BUYPASS"/> class.
            /// </summary>
            public CRC16_BUYPASS()
                : base(CRC.Standards[CRC.Standard.CRC16_BUYPASS])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_CCITTFALSE CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_CCITTFALSE
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_CCITTFALSE"/> class.
            /// </summary>
            public CRC16_CCITTFALSE()
                : base(CRC.Standards[CRC.Standard.CRC16_CCITTFALSE])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_CDMA2000 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_CDMA2000
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_CDMA2000"/> class.
            /// </summary>
            public CRC16_CDMA2000()
                : base(CRC.Standards[CRC.Standard.CRC16_CDMA2000])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_DDS110 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_DDS110
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_DDS110"/> class.
            /// </summary>
            public CRC16_DDS110()
                : base(CRC.Standards[CRC.Standard.CRC16_DDS110])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_DECTR CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_DECTR
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_DECTR"/> class.
            /// </summary>
            public CRC16_DECTR()
                : base(CRC.Standards[CRC.Standard.CRC16_DECTR])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_DECTX CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_DECTX
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_DECTX"/> class.
            /// </summary>
            public CRC16_DECTX()
                : base(CRC.Standards[CRC.Standard.CRC16_DECTX])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_DNP CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_DNP
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_DNP"/> class.
            /// </summary>
            public CRC16_DNP()
                : base(CRC.Standards[CRC.Standard.CRC16_DNP])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_EN13757 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_EN13757
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_EN13757"/> class.
            /// </summary>
            public CRC16_EN13757()
                : base(CRC.Standards[CRC.Standard.CRC16_EN13757])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_GENIBUS CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_GENIBUS
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_GENIBUS"/> class.
            /// </summary>
            public CRC16_GENIBUS()
                : base(CRC.Standards[CRC.Standard.CRC16_GENIBUS])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_MAXIM CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_MAXIM
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_MAXIM"/> class.
            /// </summary>
            public CRC16_MAXIM()
                : base(CRC.Standards[CRC.Standard.CRC16_MAXIM])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_MCRF4XX CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_MCRF4XX
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_MCRF4XX"/> class.
            /// </summary>
            public CRC16_MCRF4XX()
                : base(CRC.Standards[CRC.Standard.CRC16_MCRF4XX])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_RIELLO CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_RIELLO
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_RIELLO"/> class.
            /// </summary>
            public CRC16_RIELLO()
                : base(CRC.Standards[CRC.Standard.CRC16_RIELLO])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_T10DIF CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_T10DIF
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_T10DIF"/> class.
            /// </summary>
            public CRC16_T10DIF()
                : base(CRC.Standards[CRC.Standard.CRC16_T10DIF])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_TELEDISK CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_TELEDISK
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_TELEDISK"/> class.
            /// </summary>
            public CRC16_TELEDISK()
                : base(CRC.Standards[CRC.Standard.CRC16_TELEDISK])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_TMS37157 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_TMS37157
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_TMS37157"/> class.
            /// </summary>
            public CRC16_TMS37157()
                : base(CRC.Standards[CRC.Standard.CRC16_TMS37157])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC16_USB CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC16_USB
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC16_USB"/> class.
            /// </summary>
            public CRC16_USB()
                : base(CRC.Standards[CRC.Standard.CRC16_USB])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRCA CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRCA
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRCA"/> class.
            /// </summary>
            public CRCA()
                : base(CRC.Standards[CRC.Standard.CRCA])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of KERMIT CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class KERMIT
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="KERMIT"/> class.
            /// </summary>
            public KERMIT()
                : base(CRC.Standards[CRC.Standard.KERMIT])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of MODBUS CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class MODBUS
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MODBUS"/> class.
            /// </summary>
            public MODBUS()
                : base(CRC.Standards[CRC.Standard.MODBUS])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of X25 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class X25
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="X25"/> class.
            /// </summary>
            public X25()
                : base(CRC.Standards[CRC.Standard.X25])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of XMODEM CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class XMODEM
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="XMODEM"/> class.
            /// </summary>
            public XMODEM()
                : base(CRC.Standards[CRC.Standard.XMODEM])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC24 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC24
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC24"/> class.
            /// </summary>
            public CRC24()
                : base(CRC.Standards[CRC.Standard.CRC24])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC24_FLEXRAYA CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC24_FLEXRAYA
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC24_FLEXRAYA"/> class.
            /// </summary>
            public CRC24_FLEXRAYA()
                : base(CRC.Standards[CRC.Standard.CRC24_FLEXRAYA])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC24_FLEXRAYB CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC24_FLEXRAYB
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC24_FLEXRAYB"/> class.
            /// </summary>
            public CRC24_FLEXRAYB()
                : base(CRC.Standards[CRC.Standard.CRC24_FLEXRAYB])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC31_PHILIPS CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC31_PHILIPS
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC31_PHILIPS"/> class.
            /// </summary>
            public CRC31_PHILIPS()
                : base(CRC.Standards[CRC.Standard.CRC31_PHILIPS])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC32 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC32
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC32"/> class.
            /// </summary>
            public CRC32()
                : base(CRC.Standards[CRC.Standard.CRC32])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC32_BZIP2 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC32_BZIP2
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC32_BZIP2"/> class.
            /// </summary>
            public CRC32_BZIP2()
                : base(CRC.Standards[CRC.Standard.CRC32_BZIP2])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC32C CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC32C
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC32C"/> class.
            /// </summary>
            public CRC32C()
                : base(CRC.Standards[CRC.Standard.CRC32C])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC32D CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC32D
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC32D"/> class.
            /// </summary>
            public CRC32D()
                : base(CRC.Standards[CRC.Standard.CRC32D])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC32_MPEG2 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC32_MPEG2
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC32_MPEG2"/> class.
            /// </summary>
            public CRC32_MPEG2()
                : base(CRC.Standards[CRC.Standard.CRC32_MPEG2])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC32_POSIX CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC32_POSIX
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC32_POSIX"/> class.
            /// </summary>
            public CRC32_POSIX()
                : base(CRC.Standards[CRC.Standard.CRC32_POSIX])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC32Q CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC32Q
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC32Q"/> class.
            /// </summary>
            public CRC32Q()
                : base(CRC.Standards[CRC.Standard.CRC32Q])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of JAMCRC CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class JAMCRC
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="JAMCRC"/> class.
            /// </summary>
            public JAMCRC()
                : base(CRC.Standards[CRC.Standard.JAMCRC])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of XFER CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class XFER
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="XFER"/> class.
            /// </summary>
            public XFER()
                : base(CRC.Standards[CRC.Standard.XFER])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC40_GSM CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC40_GSM
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC40_GSM"/> class.
            /// </summary>
            public CRC40_GSM()
                : base(CRC.Standards[CRC.Standard.CRC40_GSM])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC64 CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC64
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC64"/> class.
            /// </summary>
            public CRC64()
                : base(CRC.Standards[CRC.Standard.CRC64])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC64_WE CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC64_WE
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC64_WE"/> class.
            /// </summary>
            public CRC64_WE()
                : base(CRC.Standards[CRC.Standard.CRC64_WE])
            {
            
            }
        }

        /// <summary>
        /// Automatically generated implementation of CRC64_XZ CRC standard, based on the <see cref="CRC" /> class.
        /// </summary>
        public class CRC64_XZ
            : CRC
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CRC64_XZ"/> class.
            /// </summary>
            public CRC64_XZ()
                : base(CRC.Standards[CRC.Standard.CRC64_XZ])
            {
            
            }
        }

    }
}

