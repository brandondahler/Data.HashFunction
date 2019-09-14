using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Implementation of SpookyHash V1 or SpookyHash V2 as specified at http://burtleburtle.net/bob/hash/spooky.html.
    /// </summary>
    public interface ISpookyHash
        : IHashFunctionAsync
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        ISpookyHashConfig Config { get; }

    }
}
