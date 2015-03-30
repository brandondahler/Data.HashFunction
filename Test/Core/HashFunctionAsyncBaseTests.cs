using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test.Mocks;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core
{
    public class HashFunctionAsyncBaseTests
    {
        [Fact]
        public async void HashFunctionAsyncBase_ComputeHash_Stream_NotReadable_Throws()
        {
            var msMock = new Mock<MemoryStream>();

            msMock.SetupGet(ms => ms.CanRead)
                .Returns(false);

            var hf = new HashFunctionImpl(0);


            Assert.Equal("data",
                (await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await hf.ComputeHashAsync(msMock.Object)))
                .ParamName);
        }
    }
}
