using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IDefaultBuzHash"/>.
    /// </summary>
    public interface IDefaultBuzHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IDefaultBuzHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IDefaultBuzHash"/> instance.</returns>
        IDefaultBuzHash Create();


        /// <summary>
        /// Creates a new <see cref="IDefaultBuzHash"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>A <see cref="IDefaultBuzHash"/> instance.</returns>
        IDefaultBuzHash Create(IDefaultBuzHashConfig config);
    }
}
