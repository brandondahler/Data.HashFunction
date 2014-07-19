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
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 0 }; }
        }


        public HashFunctionBaseImpl()
            : base(0)
        {

        }


        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (HashSize != 0)
                throw new ArgumentOutOfRangeException("HashSize");

            return new byte[0];
        }
    }
}
