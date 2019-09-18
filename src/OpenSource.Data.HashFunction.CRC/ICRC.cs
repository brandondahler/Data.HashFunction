using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.CRC
{
    /// <summary>
    /// Implementation of the cyclic redundancy check error-detecting code as defined at http://en.wikipedia.org/wiki/Cyclic_redundancy_check.
    /// </summary>
    public interface ICRC
        : IStreamableHashFunction
    {


        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        ICRCConfig Config { get; }

    }
}
