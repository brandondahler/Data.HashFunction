using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of the cyclic redundancy check error-detecting code as defined at http://en.wikipedia.org/wiki/Cyclic_redundancy_check.  
    /// 
    /// This implementation is generalized to encompass all possible CRC parameters from 1 to 64 bits.
    /// </summary>
    public class CRC
        : HashFunctionBase
    {
        /// <summary>
        /// The specific parameters to use when calculating the CRC value.
        /// </summary>
        public virtual CRCSettings Settings
        {
            get { return _Settings; }
        }

        
        /// <inheritdoc/>
        /// <remarks>Value is only applicable based on the current <see cref="CRC.Settings"/> value.</remarks>
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { Settings.Bits }; }
        }


        /// <summary>
        /// The set of CRC parameters to use when constructing via the default constructor.
        /// </summary>
        /// <remarks>
        /// Defaults to the settings for <see cref="CRCStandards.Standard.CRC32"/>.
        /// </remarks>
        public static CRCSettings DefaultSettings = CRCStandards.Standards[CRCStandards.Standard.CRC32];


        private readonly CRCSettings _Settings;


        /// <summary>
        /// Creates new <see cref="CRC"/> instance.
        /// </summary>
        /// <remarks>
        /// Uses CRC settings set at <see cref="CRC.DefaultSettings"/>.
        /// </remarks>
        public CRC()
            : base(DefaultSettings.Bits)
        {
            _Settings = DefaultSettings;
        }

        /// <summary>
        /// Creates new <see cref="CRC"/> instance.
        /// </summary>
        /// <param name="settings">CRC parameters to use for this instance.</param>
        public CRC(CRCSettings settings)
            : base(settings.Bits)
        {
            _Settings = settings;
        }


        /// <inheritdoc/>
        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (Settings == null)
                throw new ArgumentNullException("Settings");

            if (HashSize != Settings.Bits)
                throw new ArgumentOutOfRangeException("HashSize");


            // Use 64-bit variable regardless of CRC bit length
            UInt64 hash = Settings.InitialValue;

            // Reflect InitialValue if processing as big endian
            if (Settings.ReflectIn)
                hash = hash.ReflectBits(HashSize);


            // Store table reference in local variable to lower overhead.
            var crcTable = Settings.DataDivisionTable;


            // How much hash must be right-shifted to get the most significant byte (HashSize >= 8) or bit (HashSize < 8)
            int mostSignificantShift = HashSize - 8;

            if (HashSize < 8)
                mostSignificantShift = HashSize - 1;


            foreach (var dataByte in data.AsEnumerable())
            {
                if (HashSize >= 8)
                {
                    // Process per byte, treating hash differently based on input endianness
                    if (Settings.ReflectIn)
                        hash = (hash >> 8) ^ crcTable[(byte) hash ^ dataByte];
                    else
                        hash = (hash << 8) ^ crcTable[((byte) (hash >> mostSignificantShift)) ^ dataByte];
                
                } else {
                    // Process per bit, treating hash differently based on input endianness
                    for (int x = 0; x < 8; ++x)
                    {
                        if (Settings.ReflectIn)
                            hash = (hash >> 1) ^ crcTable[(byte) (hash & 1) ^ ((byte) (dataByte >> x) & 1)];
                        else
                            hash = (hash << 1) ^ crcTable[(byte) ((hash >> mostSignificantShift) & 1) ^ ((byte) (dataByte >> (7 - x)) & 1)];
                    }
                }
            }


            // Account for mixed-endianness
            if (Settings.ReflectIn ^ Settings.ReflectOut)
               hash = hash.ReflectBits(HashSize);


            hash ^= Settings.XOrOut;

            return hash.ToBytes(HashSize);
        }

        /// <summary>
        /// Calculates the data-division table for the CRC parameters provided.
        /// </summary>
        /// <param name="settings">CRC parameters to calculate the table for.</param>
        /// <returns>Array of UInt64 values that allows a CRC implementation to look up the result of dividing the index (data) by the polynomial.</returns>
        /// <remarks>
        /// Resulting array contains 256 items if settings.Bits &gt;= 8, or 2 items if settings.Bits &lt; 8.
        /// 
        /// The table accounts for reflecting the index bits to fix the input endianness, 
        ///   but it is not possible completely account for the output endianness if the CRC is mixed-endianness.
        /// </remarks>
        internal static UInt64[] CalculateTable(CRCSettings settings)
        {
            var perBitCount = 8;

            if (settings.Bits < 8)
                perBitCount = 1;


            var crcTable = new UInt64[1 << perBitCount];
            var mostSignificantBit = 1UL << (settings.Bits - 1);


            for (uint x = 0; x < crcTable.Length; ++x)
            {
                UInt64 curValue = x;

                if (perBitCount > 1 && settings.ReflectIn)
                    curValue = curValue.ReflectBits(perBitCount);


                curValue <<= (settings.Bits - perBitCount);


                for (int y = 0; y < perBitCount; ++y)
                {
                    if ((curValue & mostSignificantBit) > 0UL)
                        curValue = (curValue << 1) ^ settings.Polynomial;
                    else
                        curValue <<= 1;
                }


                if (settings.ReflectIn)
                    curValue = curValue.ReflectBits(settings.Bits);


                curValue &= (UInt64.MaxValue >> (64 - settings.Bits));
                
                crcTable[x] = curValue;
            }


            return crcTable;
        }

    }
}
