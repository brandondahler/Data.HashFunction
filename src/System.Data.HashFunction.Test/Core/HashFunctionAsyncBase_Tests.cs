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
        public async void HashFunctionAsyncBase_ComputeHash_Stream_NotReadable_Throws()
        {
            var msMock = new Mock<MemoryStream>();

            msMock.SetupGet(ms => ms.CanRead)
                .Returns(false);

            var hf = new HashFunctionImpl();


            Assert.Equal("data",
                (await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await hf.ComputeHashAsync(msMock.Object)))
                .ParamName);
        }
    }
}
