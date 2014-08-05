using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Test.Mocks
{
    public class HashFunctionBaseImpl
            : HashFunctionBase
    {
        public HashFunctionBaseImpl()
            : base(0)
        {

        }


        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (HashSize != 0)
                throw new InvalidOperationException("HashSize set to an invalid value.");

            return new byte[0];
        }
    }
}
