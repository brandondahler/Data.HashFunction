using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.CRC
{
    /// <summary>
    /// Defines a configuration for a <see cref="ICRC"/> implementation.
    /// </summary>
    public interface ICRCConfig
    {
        /// <summary>
        /// Length of the produced CRC value, in bits.
        /// </summary>
        /// <value>
        /// The length of the produced CRC value, in bits
        /// </value>
        int HashSizeInBits { get; }


        /// <summary>
        /// Divisor to use when calculating the CRC.
        /// </summary>
        /// <value>
        /// The divisor that will be used when calculating the CRC value.
        /// </value>
        UInt64 Polynomial { get; }


        /// <summary>
        /// Value to initialize the CRC register to before calculating the CRC.
        /// </summary>
        /// <value>
        /// The value that will be used to initialize the CRC register before the calculation of the CRC value.
        /// </value>
        UInt64 InitialValue { get; }


        /// <summary>
        /// If true, the CRC calculation processes input as big endian bit order.
        /// </summary>
        /// <value>
        /// <c>true</c> if the input should be processed in big endian bit order; otherwise, <c>false</c>.
        /// </value>
        bool ReflectIn { get; }


        /// <summary>
        /// If true, the CRC calculation processes the output as big endian bit order.
        /// </summary>
        /// <value>
        /// <c>true</c> if the CRC calculation processes the output as big endian bit order; otherwise, <c>false</c>.
        /// </value>
        bool ReflectOut { get; }


        /// <summary>
        /// Value to xor with the final CRC value.
        /// </summary>
        /// <value>
        /// The value to xor with the final CRC value.
        /// </value>
        UInt64 XOrOut { get; }

        
        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        ICRCConfig Clone();
    }
}
