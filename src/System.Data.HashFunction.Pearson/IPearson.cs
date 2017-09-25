using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Pearson
{
    public interface IPearson
        : IHashFunctionAsync
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IPearsonConfig Config { get; }

    }
}
