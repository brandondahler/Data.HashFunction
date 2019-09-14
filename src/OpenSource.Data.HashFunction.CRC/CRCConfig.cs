using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.CRC
{
    /// <summary>
    /// Defines a configuration for a <see cref="ICRC"/> implementation.
    /// </summary>
    /// <seealso cref="ICRCConfig" />
    public class CRCConfig
        : ICRCConfig
    {
        /// <summary>
        /// Length of the produced CRC value, in bits.
        /// </summary>
        /// <value>
        /// The length of the produced CRC value, in bits
        /// </value>
        public int HashSizeInBits { get; set; }

        /// <summary>
        /// Divisor to use when calculating the CRC.
        /// </summary>
        /// <value>
        /// The divisor that will be used when calculating the CRC value.
        /// </value>
        public UInt64 Polynomial { get; set; }

        /// <summary>
        /// Value to initialize the CRC register to before calculating the CRC.
        /// </summary>
        /// <value>
        /// The value that will be used to initialize the CRC register before the calculation of the CRC value.
        /// </value>
        public UInt64 InitialValue { get; set; }

        /// <summary>
        /// If true, the CRC calculation processes input as big endian bit order.
        /// </summary>
        /// <value>
        /// <c>true</c> if the input should be processed in big endian bit order; otherwise, <c>false</c>.
        /// </value>
        public bool ReflectIn { get; set; }

        /// <summary>
        /// If true, the CRC calculation processes the output as big endian bit order.
        /// </summary>
        /// <value>
        /// <c>true</c> if the CRC calculation processes the output as big endian bit order; otherwise, <c>false</c>.
        /// </value>
        public bool ReflectOut { get; set; }

        /// <summary>
        /// Value to xor with the final CRC value.
        /// </summary>
        /// <value>
        /// The value to xor with the final CRC value.
        /// </value>
        public UInt64 XOrOut { get; set; }


        #region Standards
        
        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC3_ROHC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC3_ROHC => new CRCConfig() { 
            HashSizeInBits = 3,
            Polynomial = 0x3,
            InitialValue = 0x7,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC4_ITU standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC4_ITU => new CRCConfig() { 
            HashSizeInBits = 4,
            Polynomial = 0x3,
            InitialValue = 0x0,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC5_EPC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC5_EPC => new CRCConfig() { 
            HashSizeInBits = 5,
            Polynomial = 0x09,
            InitialValue = 0x09,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC5_ITU standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC5_ITU => new CRCConfig() { 
            HashSizeInBits = 5,
            Polynomial = 0x15,
            InitialValue = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC5_USB standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC5_USB => new CRCConfig() { 
            HashSizeInBits = 5,
            Polynomial = 0x05,
            InitialValue = 0x1f,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x1f,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC6_CDMA2000A standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC6_CDMA2000A => new CRCConfig() { 
            HashSizeInBits = 6,
            Polynomial = 0x27,
            InitialValue = 0x3f,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC6_CDMA2000B standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC6_CDMA2000B => new CRCConfig() { 
            HashSizeInBits = 6,
            Polynomial = 0x07,
            InitialValue = 0x3f,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC6_DARC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC6_DARC => new CRCConfig() { 
            HashSizeInBits = 6,
            Polynomial = 0x19,
            InitialValue = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC6_ITU standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC6_ITU => new CRCConfig() { 
            HashSizeInBits = 6,
            Polynomial = 0x03,
            InitialValue = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC7 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC7 => new CRCConfig() { 
            HashSizeInBits = 7,
            Polynomial = 0x09,
            InitialValue = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC7_ROHC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC7_ROHC => new CRCConfig() { 
            HashSizeInBits = 7,
            Polynomial = 0x4f,
            InitialValue = 0x7f,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8 => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x07,
            InitialValue = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_CDMA2000 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_CDMA2000 => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x9b,
            InitialValue = 0xff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_DARC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_DARC => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x39,
            InitialValue = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_DVBS2 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_DVBS2 => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0xd5,
            InitialValue = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_EBU standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_EBU => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x1d,
            InitialValue = 0xff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_ICODE standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_ICODE => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x1d,
            InitialValue = 0xfd,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_ITU standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_ITU => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x07,
            InitialValue = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x55,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_MAXIM standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_MAXIM => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x31,
            InitialValue = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_ROHC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_ROHC => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x07,
            InitialValue = 0xff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC8_WCDMA standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC8_WCDMA => new CRCConfig() { 
            HashSizeInBits = 8,
            Polynomial = 0x9b,
            InitialValue = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC10 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC10 => new CRCConfig() { 
            HashSizeInBits = 10,
            Polynomial = 0x233,
            InitialValue = 0x000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC10_CDMA2000 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC10_CDMA2000 => new CRCConfig() { 
            HashSizeInBits = 10,
            Polynomial = 0x3d9,
            InitialValue = 0x3ff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC11 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC11 => new CRCConfig() { 
            HashSizeInBits = 11,
            Polynomial = 0x385,
            InitialValue = 0x01a,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC12_3GPP standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC12_3GPP => new CRCConfig() { 
            HashSizeInBits = 12,
            Polynomial = 0x80f,
            InitialValue = 0x000,
            ReflectIn = false,
            ReflectOut = true,
            XOrOut = 0x000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC12_CDMA2000 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC12_CDMA2000 => new CRCConfig() { 
            HashSizeInBits = 12,
            Polynomial = 0xf13,
            InitialValue = 0xfff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC12_DECT standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC12_DECT => new CRCConfig() { 
            HashSizeInBits = 12,
            Polynomial = 0x80f,
            InitialValue = 0x000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC13_BBC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC13_BBC => new CRCConfig() { 
            HashSizeInBits = 13,
            Polynomial = 0x1cf5,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC14_DARC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC14_DARC => new CRCConfig() { 
            HashSizeInBits = 14,
            Polynomial = 0x0805,
            InitialValue = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC15 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC15 => new CRCConfig() { 
            HashSizeInBits = 15,
            Polynomial = 0x4599,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC15_MPT1327 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC15_MPT1327 => new CRCConfig() { 
            HashSizeInBits = 15,
            Polynomial = 0x6815,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0001,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the ARC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig ARC => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x8005,
            InitialValue = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_AUGCCITT standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_AUGCCITT => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0x1d0f,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_BUYPASS standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_BUYPASS => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x8005,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_CCITTFALSE standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_CCITTFALSE => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_CDMA2000 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_CDMA2000 => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0xc867,
            InitialValue = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_DDS110 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_DDS110 => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x8005,
            InitialValue = 0x800d,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_DECTR standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_DECTR => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x0589,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0001,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_DECTX standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_DECTX => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x0589,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_DNP standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_DNP => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x3d65,
            InitialValue = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_EN13757 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_EN13757 => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x3d65,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0xffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_GENIBUS standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_GENIBUS => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0xffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_MAXIM standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_MAXIM => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x8005,
            InitialValue = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_MCRF4XX standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_MCRF4XX => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_RIELLO standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_RIELLO => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0xb2aa,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_T10DIF standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_T10DIF => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x8bb7,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_TELEDISK standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_TELEDISK => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0xa097,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_TMS37157 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_TMS37157 => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0x89ec,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC16_USB standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC16_USB => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x8005,
            InitialValue = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRCA standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRCA => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0xc6c6,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the KERMIT standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig KERMIT => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the MODBUS standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig MODBUS => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x8005,
            InitialValue = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the X25 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig X25 => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the XMODEM standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig XMODEM => new CRCConfig() { 
            HashSizeInBits = 16,
            Polynomial = 0x1021,
            InitialValue = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC24 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC24 => new CRCConfig() { 
            HashSizeInBits = 24,
            Polynomial = 0x864cfb,
            InitialValue = 0xb704ce,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC24_FLEXRAYA standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC24_FLEXRAYA => new CRCConfig() { 
            HashSizeInBits = 24,
            Polynomial = 0x5d6dcb,
            InitialValue = 0xfedcba,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC24_FLEXRAYB standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC24_FLEXRAYB => new CRCConfig() { 
            HashSizeInBits = 24,
            Polynomial = 0x5d6dcb,
            InitialValue = 0xabcdef,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC31_PHILIPS standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC31_PHILIPS => new CRCConfig() { 
            HashSizeInBits = 31,
            Polynomial = 0x04c11db7,
            InitialValue = 0x7fffffff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x7fffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC32 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC32 => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x04c11db7,
            InitialValue = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC32_BZIP2 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC32_BZIP2 => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x04c11db7,
            InitialValue = 0xffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0xffffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC32C standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC32C => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x1edc6f41,
            InitialValue = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC32D standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC32D => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0xa833982b,
            InitialValue = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC32_MPEG2 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC32_MPEG2 => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x04c11db7,
            InitialValue = 0xffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC32_POSIX standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC32_POSIX => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x04c11db7,
            InitialValue = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0xffffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC32Q standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC32Q => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x814141ab,
            InitialValue = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the JAMCRC standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig JAMCRC => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x04c11db7,
            InitialValue = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0x00000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the XFER standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig XFER => new CRCConfig() { 
            HashSizeInBits = 32,
            Polynomial = 0x000000af,
            InitialValue = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x00000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC40_GSM standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC40_GSM => new CRCConfig() { 
            HashSizeInBits = 40,
            Polynomial = 0x0004820009,
            InitialValue = 0x0000000000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0xffffffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC64 standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC64 => new CRCConfig() { 
            HashSizeInBits = 64,
            Polynomial = 0x42f0e1eba9ea3693,
            InitialValue = 0x0000000000000000,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0x0000000000000000,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC64_WE standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC64_WE => new CRCConfig() { 
            HashSizeInBits = 64,
            Polynomial = 0x42f0e1eba9ea3693,
            InitialValue = 0xffffffffffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XOrOut = 0xffffffffffffffff,
        };


        /// <summary>
        /// Creates an instance of <see cref="ICRCConfig"/> configured to the CRC64_XZ standard.
        /// 
        /// Parameters for this standard was provided by http://reveng.sourceforge.net/crc-catalogue/.
        /// </summary>
        /// <value>
        /// A new, configured instance of <see cref="ICRCConfig"/>. 
        /// </value>
        public static ICRCConfig CRC64_XZ => new CRCConfig() { 
            HashSizeInBits = 64,
            Polynomial = 0x42f0e1eba9ea3693,
            InitialValue = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XOrOut = 0xffffffffffffffff,
        };

        #endregion



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public ICRCConfig Clone() =>
            new CRCConfig() {
                HashSizeInBits = HashSizeInBits,
                Polynomial = Polynomial,
                InitialValue = InitialValue,
                ReflectIn = ReflectIn,
                ReflectOut = ReflectOut,
                XOrOut = XOrOut
            };
    }
}
