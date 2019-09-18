using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Pearson
{
    /// <summary>
    /// Implementation of Pearson hashing as specified at http://en.wikipedia.org/wiki/Pearson_hashing and
    ///   http://cs.mwsu.edu/~griffin/courses/2133/downloads/Spring11/p677-pearson.pdf.
    /// </summary>
    public interface IPearson
        : IStreamableHashFunction
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
