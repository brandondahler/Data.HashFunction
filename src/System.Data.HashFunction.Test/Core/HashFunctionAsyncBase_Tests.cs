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
    public class HashFunctionAsyncBase_Tests
    {
        [Fact]
        public async Task HashFunctionBase_ComputeHashAsync_Stream_IsNull_Throws()
        {
            var hashFunction = new HashFunctionImpl();

            Assert.Equal("data",
                (await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await hashFunction.ComputeHashAsync(null)))
                .ParamName);
        }

        [Fact]
        public async Task HashFunctionAsyncBase_ComputeHashAsync_Stream_NotReadable_Throws()
        {
            var memoryStreamMock = new Mock<MemoryStream>();

            memoryStreamMock.SetupGet(ms => ms.CanRead)
                .Returns(false);

            var hashFunction = new HashFunctionImpl();


            Assert.Equal("data",
                (await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await hashFunction.ComputeHashAsync(memoryStreamMock.Object)))
                .ParamName);
        }
    }
}
