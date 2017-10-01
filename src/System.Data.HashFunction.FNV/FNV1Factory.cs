using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    public class FNV1Factory
        : IFNV1Factory
    {
        public static IFNV1Factory Instance { get; } = new FNV1Factory();


        private FNV1Factory()
        {

        }


        public IFNV1 Create()
        {
            return Create(
                FNVConfig.GetPredefinedConfig(64));
        }

        public IFNV1 Create(IFNVConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new FNV1_Implementation(config);
        }
    }
}
