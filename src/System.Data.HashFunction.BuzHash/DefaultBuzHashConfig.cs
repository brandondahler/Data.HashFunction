namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IDefaultBuzHash"/> implementation.
    /// </summary>
    /// <seealso cref="IDefaultBuzHashConfig" />
    public class DefaultBuzHashConfig
        : IDefaultBuzHashConfig
    {
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
        /// Gets the shift direction.
        /// </summary>
        /// <value>
        /// The shift direction.
        /// </value>
        /// <remarks>
        /// Defaults to <c>CircularShiftDirection.Left</c>
        /// </remarks>
        public CircularShiftDirection ShiftDirection { get; set; } = CircularShiftDirection.Left;
    }
}