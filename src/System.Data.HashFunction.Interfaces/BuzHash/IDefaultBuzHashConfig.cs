namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IDefaultBuzHash"/> implementation.
    /// </summary>
    public interface IDefaultBuzHashConfig
    {

        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        int HashSizeInBits { get; }

        /// <summary>
        /// Gets the shift direction.
        /// </summary>
        /// <value>
        /// The shift direction.
        /// </value>
        CircularShiftDirection ShiftDirection { get; }
    }
}