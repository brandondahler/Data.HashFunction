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
        public void HashFunctionAsyncBase_ComputeHash_Stream_NotReadable_Throws()
        {
            var msMock = new Mock<MemoryStream>();

            msMock.SetupGet(ms => ms.CanRead)
                .Returns(false);

            var hf = new HashFunctionImpl();


            var aggregateException =
                Assert.Throws<AggregateException>(() =>
                    hf.ComputeHashAsync(msMock.Object).Wait());

            var resultingException =
                Assert.Single(aggregateException.InnerExceptions);


            Assert.Equal("data",
                Assert.IsType<ArgumentException>(
                    resultingException)
                .ParamName);
        }
    }
}
