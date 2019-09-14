using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.CRC
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ICRC"/>.
    /// </summary>
    public interface ICRCFactory
    {

        /// <summary>
        /// Creates a new <see cref="ICRC"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="ICRC"/> instance.</returns>
        ICRC Create();


        /// <summary>
        /// Creates a new <see cref="ICRC"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>A <see cref="ICRC"/> instance.</returns>
        ICRC Create(ICRCConfig config);
    }
}
