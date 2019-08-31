using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test._Mocks;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core
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

        [Fact]
        public void HashFunctionBase_ComputeHash_Stream_IsNull_Throws()
        {
            var hashFunction = new HashFunctionImpl();

            Assert.Equal("data",
                Assert.Throws<ArgumentNullException>(() =>
                    hashFunction.ComputeHash((Stream) null))
                .ParamName);
        }

        [Fact]
        public void HashFunctionBase_ComputeHash_Stream_NotReadable_Throws()
        {
            var memoryStreamMock = new Mock<MemoryStream>();

            memoryStreamMock.SetupGet(ms => ms.CanRead)
                .Returns(false);

            var hashFunction = new HashFunctionImpl();

            Assert.Equal("data",
                Assert.Throws<ArgumentException>(() =>
                    hashFunction.ComputeHash(memoryStreamMock.Object))
                .ParamName);
        }
    }
}
