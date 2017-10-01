using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Pearson
{
    public class PearsonConfig
        : IPearsonConfig
    {
        public IReadOnlyList<byte> Table { get; set; } = null;

        public int HashSizeInBits { get; set; } = 8;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IPearsonConfig Clone() => 
            new PearsonConfig() {
                Table = Table?.ToArray(),
                HashSizeInBits = HashSizeInBits
            };
    }
}
