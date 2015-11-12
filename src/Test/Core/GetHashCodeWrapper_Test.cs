using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core
{
    public class GetHashCodeWrapper_Test
    {

        [Fact]
        public void GetHashCodeWrapper_HashSize_32()
        {
            var getHashCodeWrapper = new GetHashCodeWrapper();
            Assert.Equal(32, getHashCodeWrapper.HashSize);
        }

        [Fact]
        public void GetHashCodeWrapper_CalculateHash_Null_Throws()
        {
            var getHashCodeWrapper = new GetHashCodeWrapper();

            Assert.Equal(
                "object",
                Assert.Throws<ArgumentNullException>(() => 
                        getHashCodeWrapper.CalculateHash(null))
                    .ParamName);
        }


        [Fact]
        public void GetHashCodeWrapper_CalculateHash_MatchesGetHashCode()
        {
            var getHashCodeWrapper = new GetHashCodeWrapper();
            var @object = new object();

            Assert.Equal(
                BitConverter.GetBytes(@object.GetHashCode()),
                getHashCodeWrapper.CalculateHash(@object));
        }

    }
}
