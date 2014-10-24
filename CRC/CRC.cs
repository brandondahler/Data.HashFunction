using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of the cyclic redundancy check error-detecting code as defined at http://en.wikipedia.org/wiki/Cyclic_redundancy_check.
    /// This implementation is generalized to encompass all possible CRC parameters from 1 to 64 bits.
    /// </summary>
    public partial class CRC
        : HashFunctionAsyncBase
    {
        /// <summary>
        /// The CRC parameters to use when calculating the hash value.
        /// </summary>
        /// <value>
        /// The CRC parameters that will be used to calculate hash values.
        /// </value>
        public virtual Setting Settings
        {
            get { return _Settings; }
        }


        /// <summary>
        /// The set of CRC parameters to use when constructing <see cref="CRC" /> via the default constructor.
        /// </summary>
        /// <value>
        /// The current CRC parameters that will be used when a new <see cref="CRC" /> instance is created via its default constructor.
        /// </value>
        /// <exception cref="System.ArgumentNullException">value</exception>
        /// <remarks>
        /// Defaults to the settings for <see cref="CRCStandards.CRC32" />.
        /// </remarks>
        public static Setting DefaultSettings 
        {
            get { return _DefaultSettings; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _DefaultSettings = value;  
            } 
        }
        
        
        private readonly Setting _Settings;

        private static Setting _DefaultSettings;


        /// <remarks>Uses CRC settings set at <see cref="CRC.DefaultSettings"/>.</remarks>
        /// <inheritdoc cref="CRC(Setting)" />
        public CRC()
            : this(DefaultSettings)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CRC"/> class.
        /// </summary>
        /// <param name="settings"><inheritdoc cref="Settings" /></param>
        /// <exception cref="System.ArgumentNullException">settings</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public CRC(Setting settings)
            : base(settings != null ? settings.Bits : -1)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            _Settings = settings;
        }


        /// <summary>
        /// Initializes static, dependent fields of the <see cref="CRC"/> class.
        /// </summary>
        static CRC()
        {
            _DefaultSettings = Standards[Standard.CRC32];
        }



        /// <exception cref="System.InvalidOperationException">
        /// Settings set to an invalid value.
        /// or
        /// HashSize set to an invalid value.
        /// </exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            if (Settings == null)
                throw new InvalidOperationException("Settings set to an invalid value.");

            if (HashSize != Settings.Bits)
                throw new InvalidOperationException("HashSize set to an invalid value.");


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


            data.ForEachRead((dataBytes, position, length) => {
                ProcessBytes(ref hash, crcTable, mostSignificantShift, dataBytes, position, length);
            });


            // Account for mixed-endianness
            if (Settings.ReflectIn ^ Settings.ReflectOut)
               hash = hash.ReflectBits(HashSize);


            hash ^= Settings.XOrOut;

            return hash.ToBytes(HashSize);
        }

        /// <exception cref="System.InvalidOperationException">
        /// Settings set to an invalid value.
        /// or
        /// HashSize set to an invalid value.
        /// </exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            if (Settings == null)
                throw new InvalidOperationException("Settings set to an invalid value.");

            if (HashSize != Settings.Bits)
                throw new InvalidOperationException("HashSize set to an invalid value.");


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


            await data.ForEachReadAsync((dataBytes, position, length) => {
                ProcessBytes(ref hash, crcTable, mostSignificantShift, dataBytes, position, length);
            }).ConfigureAwait(false);


            // Account for mixed-endianness
            if (Settings.ReflectIn ^ Settings.ReflectOut)
               hash = hash.ReflectBits(HashSize);


            hash ^= Settings.XOrOut;

            return hash.ToBytes(HashSize);
        }

        private void ProcessBytes(ref UInt64 hash, IReadOnlyList<UInt64> crcTable, int mostSignificantShift, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
            {
                if (HashSize >= 8)
                {
                    // Process per byte, treating hash differently based on input endianness
                    if (Settings.ReflectIn)
                        hash = (hash >> 8) ^ crcTable[(byte) hash ^ dataBytes[x]];
                    else
                        hash = (hash << 8) ^ crcTable[((byte) (hash >> mostSignificantShift)) ^ dataBytes[x]];

                } else {
                    // Process per bit, treating hash differently based on input endianness
                    for (int y = 0; y < 8; ++y)
                    {
                        if (Settings.ReflectIn)
                            hash = (hash >> 1) ^ crcTable[(byte) (hash & 1) ^ ((byte) (dataBytes[x] >> y) & 1)];
                        else
                            hash =  (hash << 1) ^ crcTable[(byte) ((hash >> mostSignificantShift) & 1) ^ ((byte) (dataBytes[x] >> (7 - y)) & 1)];
                    }

                }
            }
        }

        /// <summary>
        /// Calculates the data-division table for the CRC parameters provided.
        /// </summary>
        /// <param name="settings">CRC parameters to calculate the table for.</param>
        /// <returns>
        /// Array of UInt64 values that allows a CRC implementation to look up the result
        /// of dividing the index (data) by the polynomial.
        /// </returns>
        /// <remarks>
        /// Resulting array contains 256 items if settings.Bits &gt;= 8, or 2 items if settings.Bits &lt; 8.
        /// The table accounts for reflecting the index bits to fix the input endianness,
        /// but it is not possible completely account for the output endianness if the CRC is mixed-endianness.
        /// </remarks>
        internal static UInt64[] CalculateTable(Setting settings)
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
