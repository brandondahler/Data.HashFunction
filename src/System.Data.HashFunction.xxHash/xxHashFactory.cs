using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.xxHash
{
    public class xxHashFactory
        : IxxHashFactory
    {
        public static IxxHashFactory Instance { get; } = new xxHashFactory();


        private xxHashFactory()
        {

        }


        public IxxHash Create() =>
            Create(new xxHashConfig());

        public IxxHash Create(IxxHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new xxHash_Implementation(config);
        }
    }
}
