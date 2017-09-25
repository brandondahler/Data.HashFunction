using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Pearson
{
    public class PearsonConfig
        : IPearsonConfig
    {
        public int HashSizeInBits { get; set; } = 8;

        public IReadOnlyList<byte> Table { get; set; } = null;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IPearsonConfig Clone() => 
            new PearsonConfig() {
                HashSizeInBits = HashSizeInBits,
                Table = Table?.ToArray()
            };
    }
}
