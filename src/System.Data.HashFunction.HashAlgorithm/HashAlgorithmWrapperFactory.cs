using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.HashAlgorithm
{
    public class HashAlgorithmWrapperFactory
        : IHashAlgorithmWrapperFactory
    {
        public static IHashAlgorithmWrapperFactory Instance { get; } = new HashAlgorithmWrapperFactory();


        private HashAlgorithmWrapperFactory()
        {

        }


        public IHashAlgorithmWrapper Create(IHashAlgorithmWrapperConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new HashAlgorithmWrapper_Implementation(config);
        }
    }
}
