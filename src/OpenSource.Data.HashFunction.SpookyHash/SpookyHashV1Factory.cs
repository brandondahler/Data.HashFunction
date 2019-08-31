using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0618 // Type or member is obsolete
namespace System.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ISpookyHashV1"/>.
    /// </summary>
    [Obsolete("SpookyHashV1 has known issues, use SpookyHashV2.")]
    public class SpookyHashV1Factory
        : ISpookyHashV1Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static ISpookyHashV1Factory Instance { get; } = new SpookyHashV1Factory();


        private SpookyHashV1Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="ISpookyHashV1"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="ISpookyHashV1"/> instance.</returns>
        public ISpookyHashV1 Create() =>
            Create(new SpookyHashConfig());

        /// <summary>
        /// Creates a new <see cref="ISpookyHashV1"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="ISpookyHashV1"/> instance.</returns>
        public ISpookyHashV1 Create(ISpookyHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new SpookyHashV1_Implementation(config);
        }
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
