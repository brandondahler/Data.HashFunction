using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Test._Mocks;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Core
{
    public class HashFunctionBase_Tests
    {
        [Fact]
        public void HashFunctionBase_ComputeHash_ByteArray_IsNull_Throws()
        {
            var hashFunction = new HashFunctionImpl();

            Assert.Equal("data",
                Assert.Throws<ArgumentNullException>(() =>
                    hashFunction.ComputeHash((byte[]) null))
                .ParamName);
        }

    }
}
