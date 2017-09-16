using System.Linq;

namespace System.Data.HashFunction.Blake2
{
    /// <summary>
    /// Defines a configuration for a Blake2B hash function implementation.
    /// </summary>
    /// <seealso cref="IBlake2BConfig" />
    public class Blake2BConfig
        : IBlake2BConfig
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        /// <remarks>
        /// Defaults to <c>512</c>.
        /// </remarks>
        public int HashSizeInBits { get; set; } = 512;

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        public byte[] Key { get; set; }

        /// <summary>
        /// Gets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        public byte[] Salt { get; set; }

        /// <summary>
        /// Gets the personalization sequence.
        /// </summary>
        /// <value>
        /// The personalization sequence.
        /// </value>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        public byte[] Personalization { get; set; }
    }
}