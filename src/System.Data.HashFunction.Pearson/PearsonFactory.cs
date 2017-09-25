using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Pearson
{
    public class PearsonFactory
        : IPearsonFactory
    {
        public static IPearsonFactory Instance { get; } = new PearsonFactory();


        private PearsonFactory()
        {

        }


        public IPearson Create() =>
            Create(new WikipediaPearsonConfig());

        public IPearson Create(IPearsonConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new Pearson_Implementation(config);
        }
    }
}
