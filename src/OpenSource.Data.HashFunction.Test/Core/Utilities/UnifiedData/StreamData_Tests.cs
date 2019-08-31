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
    public class StreamData_Tests
    {
        #region Constructor

        [Fact]
        public void StreamData_Constructor_Data_IsNull_Throws()
        {
            Assert.Equal(
                "data",
                Assert.Throws<ArgumentNullException>(
                        () => new StreamData(null))
                    .ParamName);
        }

        #endregion


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
            : UnifiedDataAsyncBase_Tests
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
