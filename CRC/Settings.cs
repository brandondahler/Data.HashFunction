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
        /// Storage for all CRC calculation parameters and cache location of the CRC data-division table.
        /// </summary>
        public class Setting
        {
            /// <summary>
            /// Length of the produced CRC value, in bits.
            /// </summary>
            /// <value>
            /// The length of the produced CRC value, in bits
            /// </value>
            public int Bits { get; private set; }


            /// <summary>
            /// Divisor to use when calculating the CRC.
            /// </summary>
            /// <value>
            /// The divisor that will be used when calculating the CRC value.
            /// </value>
            public UInt64 Polynomial { get; private set; }


            /// <summary>
            /// Value to initialize the CRC register to before calculating the CRC.
            /// </summary>
            /// <value>
            /// The value that will be used to initialize the CRC register before the calculation of the CRC value.
            /// </value>
            public UInt64 InitialValue { get; private set; }


            /// <summary>
            /// If true, the CRC calculation processes input as big endian bit order.
            /// </summary>
            /// <value>
            ///   <c>true</c> if the input should be processed in big endian bit order; otherwise, <c>false</c>.
            /// </value>
            public bool ReflectIn { get; private set; }


            /// <summary>
            /// If true, the CRC calculation processes the output as big endian bit order.
            /// </summary>
            /// <value>
            ///   <c>true</c> if the CRC calculation processes the output as big endian bit order; otherwise, <c>false</c>.
            /// </value>
            public bool ReflectOut { get; private set; }


            /// <summary>
            /// Value to xor with the final CRC value.
            /// </summary>
            /// <value>
            /// The value to xor with the final CRC value.
            /// </value>
            public UInt64 XOrOut { get; private set; }


            /// <summary>
            /// Lookup table of precalculated divisions of the index divided by the <see cref="Polynomial" />.
            /// </summary>
            /// <value>
            /// The precalculated table giving the result of dividing the items index by the <see cref="Polynomial" />.
            /// </value>
#if NET45
            public IReadOnlyList<UInt64> DataDivisionTable { get { return _DataDivisionTable.Value; } }
#else
            public IList<UInt64> DataDivisionTable { get { return _DataDivisionTable.Value; } }
#endif



            private readonly Lazy<UInt64[]> _DataDivisionTable;




            /// <summary>
            /// Initializes a new instance of the <see cref="Setting"/> class.
            /// </summary>
            /// <param name="bits"><inheritdoc cref="Bits" /></param>
            /// <param name="polynomial"><inheritdoc cref="Polynomial" /></param>
            /// <param name="initialValue"><inheritdoc cref="InitialValue" /></param>
            /// <param name="reflectIn"><inheritdoc cref="ReflectIn" /></param>
            /// <param name="reflectOut"><inheritdoc cref="ReflectOut" /></param>
            /// <param name="xOrOut"><inheritdoc cref="XOrOut" /></param>
            /// <exception cref="System.ArgumentOutOfRangeException">bits;bitLength must be in the range [1, 64].</exception>
            public Setting(
                int bits, UInt64 polynomial, UInt64 initialValue,
                bool reflectIn, bool reflectOut, UInt64 xOrOut)
            {
                if (bits < 1 || bits > 64)
                    throw new ArgumentOutOfRangeException("bits", "bitLength must be in the range [1, 64].");

                CheckInput("polynomial", polynomial, bits);
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
            /// <returns>
            /// True if the table was calculated, false if the table has already been calculated.
            /// </returns>
            public bool PreCalculateTable()
            {
                if (_DataDivisionTable.IsValueCreated)
                    return false;

                GC.KeepAlive(
                    _DataDivisionTable.Value);

                return true;
            }


            /// <summary>
            /// Ensures that the 64-bit inputValue is not greater than the maximum unsigned value for a [bitLength]-length integer.
            /// </summary>
            /// <param name="paramName">Name of the parameter being passed, used in exception that is thrown if the value is invalid.</param>
            /// <param name="inputValue">Value to check.</param>
            /// <param name="bitLength">Expected bit length of the inputValue parameter.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
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
}
