using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Coverage
{
    public class HashFunctionBaseTests
    {
        public class Test_HashFunctionBase 
            : HashFunctionBase
        {
            public override IEnumerable<int> ValidHashSizes
            {
                get { return new[] { 0 }; }
            }

            public Test_HashFunctionBase()
                : base(0)
            {

            }

            public override byte[] ComputeHash(byte[] data)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void HashFunctionBase_InvalidHashSize_Throws()
        {
            var hfb = new Test_HashFunctionBase();

            Assert.Throws<ArgumentOutOfRangeException>(() => hfb.HashSize = 1);
        }
    }
}
