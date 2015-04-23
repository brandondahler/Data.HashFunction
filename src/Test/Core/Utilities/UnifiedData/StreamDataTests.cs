using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.UnifiedData
{
    using System.Data.HashFunction.Utilities.UnifiedData;
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


        public class UnifiedDataTests_StreamData
            : UnifiedDataTests
        {
            protected override UnifiedData CreateTestData(int length)
            {
                var r = new Random();

                var data = new byte[length];
                r.NextBytes(data);

                return new StreamData(new MemoryStream(data));
            }
        }
    }
}
