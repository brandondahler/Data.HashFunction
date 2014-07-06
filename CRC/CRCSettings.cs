using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Storage for all CRC calculation parameters and cache location of the CRC data-division table.
    /// </summary>
    public class CRCSettings
    {
        /// <summary>Length of the CRC output in bits.</summary>
        public int Bits { get; private set; }


        /// <summary>Divisor to use when calculating the CRC.</summary>
        public UInt64 Polynomial { get; private set; }


        /// <summary>Value to initialize the CRC register to before calculating the CRC.</summary>
        public UInt64 InitialValue { get; private set; }


        /// <summary>If true, the CRC calculation processes input as big endian bit order.</summary>
        public bool ReflectIn { get; private set; }


        /// <summary>If true, the CRC calculation processes output as big endian bit order.</summary>
        public bool ReflectOut { get; private set; }


        /// <summary>Value to xor the final CRC value by.</summary>
        public UInt64 XOrOut { get; private set; }


        /// <summary>Lookup table of precalculated divisions of the index divided by the <see cref="CRCSettings.Polynomial"/>.</summary>
        public UInt64[] DataDivisionTable { get { return _DataDivisionTable.Value; } }



        private readonly Lazy<UInt64[]> _DataDivisionTable;



        /// <summary>
        /// Creates new <see cref="CRCSettings"/> instance.  Ensures the settings are complete and valid.
        /// </summary>
        /// <param name="bits">Length of the CRC output in bits, must be in the range [1, 64].</param>
        /// <param name="polynomial">
        /// Divisor to use when calculating the CRC, 
        ///   must not be greater than the maximum unsigned value for a [bits]-length integer.
        /// </param>
        /// <param name="initialValue">
        /// Value to initialize the CRC register to before calculating the CRC,
        ///   must not be greater than the maximum unsigned value for a [bits]-length integer.
        /// </param>
        /// <param name="reflectIn">If true, the CRC calculation processes input as big endian bit order.</param>
        /// <param name="reflectOut">If true, the CRC calculation processes output as big endian bit order.</param>
        /// <param name="xOrOut">
        /// Value to xor the final CRC value by,
        ///   must not be greater than the maximum unsigned value for a [bits]-length integer.
        /// </param>
        public CRCSettings(
            int bits, UInt64 polynomial, UInt64 initialValue, 
            bool reflectIn, bool reflectOut, UInt64 xOrOut)
        {
            if (bits < 1 || bits > 64)
                throw new ArgumentOutOfRangeException("bits", "bitLength must be in the range [1, 64].");

            CheckInput("polynomial",   polynomial,   bits);
            CheckInput("initialValue", initialValue, bits);
            CheckInput("xOrOut", xOrOut, bits);


            Bits = bits;
            InitialValue = initialValue;
            Polynomial = polynomial;

            ReflectIn = reflectIn;
            ReflectOut = reflectOut;
            XOrOut = xOrOut;


            _DataDivisionTable = new Lazy<UInt64[]>(
                new Func<UInt64[]>(() => 
                    CRC.CalculateTable(this)));
        }


        /// <summary>
        /// Calculates the DataDivisionTable property eagerly so that it is not calculated just-in-time.
        /// </summary>
        /// <returns>True if the table was calculated, false if the table has already been calculated.</returns>
        public bool PreCalculateTable()
        {
            if (_DataDivisionTable.IsValueCreated)
                return false;

            var noOp = _DataDivisionTable.Value;
            noOp = null;

            return true;
        }


        /// <summary>
        /// Ensures that the 64-bit inputValue is not greater than the maximum unsigned value for a [bitLength]-length integer.
        /// </summary>
        /// <param name="paramName">Name of the parameter being passed, used in exception that is thrown if the value is invalid.</param>
        /// <param name="inputValue">Value to check.</param>
        /// <param name="bitLength">Expected bit length of the inputValue parameter.</param>
        private static void CheckInput(string paramName, UInt64 inputValue, int bitLength)
        {
            var maxInputValue = UInt64.MaxValue >> (64 - bitLength);

            if (inputValue > maxInputValue)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    string.Format("{0} must not be more than {1} bits in length.",
                        paramName, bitLength));
            }
        }


    }
}
