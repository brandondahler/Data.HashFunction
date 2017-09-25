using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    public class FNV1aFactory
        : IFNV1aFactory
    {
        public static IFNV1aFactory Instance { get; } = new FNV1aFactory();


        private FNV1aFactory()
        {

        }

        public IFNV1a Create()
        {
            return Create(new FNVConfig());
        }

        public IFNV1a Create(IFNVConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new FNV1a_Implementation(config);
        }
    }
}
