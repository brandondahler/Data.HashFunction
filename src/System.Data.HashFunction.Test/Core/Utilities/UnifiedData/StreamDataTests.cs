using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.UnifiedData
{
    using System.Data.HashFunction.Core.Utilities.UnifiedData;
    public class StreamDataTests
    {
        [Fact]
        public void StreamData_Dispose_Works()
        {
            var memoryStream = new MemoryStream();
            var streamData = new StreamData(memoryStream);


            Assert.True(memoryStream.CanRead);

            streamData.Dispose();

            Assert.False(memoryStream.CanRead);
        }


        public class UnifiedDataAsyncTests_StreamData
            : UnifiedDataAsyncTests
        {
            protected override IUnifiedDataAsync CreateTestDataAsync(int length)
            {
                var r = new Random();

                var data = new byte[length];
                r.NextBytes(data);

                return new StreamData(new MemoryStream(data));
            }
        }
    }
}
