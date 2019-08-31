using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Blake2
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IBlake2B"/>.
    /// </summary>
    public interface IBlake2BFactory
    {
        /// <summary>
        /// Creates a new <see cref="IBlake2B"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IBlake2B"/> instance.</returns>
        IBlake2B Create();


        /// <summary>
        /// Creates a new <see cref="IBlake2B"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>A <see cref="IBlake2B"/> instance.</returns>
        IBlake2B Create(IBlake2BConfig config);

    }
}
