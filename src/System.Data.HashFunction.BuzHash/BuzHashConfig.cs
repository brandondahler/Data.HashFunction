using System.Collections.Generic;
using System.Linq;

namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IDefaultBuzHash"/> implementation.
    /// </summary>
    /// <seealso cref="IBuzHashConfig" />
    public class BuzHashConfig
        : IBuzHashConfig
    {
        public IReadOnlyList<UInt64> Rtab { get; set; } = null;


        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        /// <remarks>
        /// Defaults to <c>64</c>.
        /// </remarks>
        public int HashSizeInBits { get; set; } = 64;

        /// <summary>
        /// Gets the seed value.
        /// </summary>
        /// <value>
        /// The seed value.
        /// </value>
        public UInt64 Seed { get; set; } = 0UL;

        /// <summary>
        /// Gets the shift direction.
        /// </summary>
        /// <value>
        /// The shift direction.
        /// </value>
        /// <remarks>
        /// Defaults to <c>CircularShiftDirection.Left</c>
        /// </remarks>
        public CircularShiftDirection ShiftDirection { get; set; } = CircularShiftDirection.Left;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IBuzHashConfig Clone() =>
            new BuzHashConfig() {
                Rtab = Rtab?.ToArray(),
                HashSizeInBits = HashSizeInBits,
                Seed = Seed,
                ShiftDirection = ShiftDirection
            };
    }
}