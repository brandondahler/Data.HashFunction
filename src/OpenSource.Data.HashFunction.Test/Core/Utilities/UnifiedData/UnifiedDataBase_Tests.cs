using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.UnifiedData
{
    using System.Data.HashFunction.Core.Utilities.UnifiedData;
    using System.Threading;

    public abstract class UnifiedDataBase_Tests
    {
        [Fact]
        public void UnifiedData_Length_Works()
        {
            var testLengths = new[] {
                0, 1, 2, 4, 8, 16, 32, 64,
                short.MaxValue, ushort.MaxValue, 0x10000, 0x80000
            };

            foreach (var testLength in testLengths)
            {
                var data = CreateTestData(testLength);

                Assert.Equal(testLength, data.Length);
            }
        }

        #region BufferSize

        [Fact]
        public void UnifiedData_BufferSize_InvalidValue_Throws()
        {
            var testData = CreateTestData(0);

            var invalidBufferSizes = new[] {
                int.MinValue, short.MinValue, -1, 0
            };


            foreach (var invalidBufferSize in invalidBufferSizes)
            {
                Assert.Equal("value",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        testData.BufferSize = invalidBufferSize)
                    .ParamName);
            }
        }

        [Fact]
        public void UnifiedData_BufferSize_ValidValue_Works()
        {
            var testData = CreateTestData(0);

            var validBufferSizes = new[] {
                1, 2, 4, 8, 9, 16, 32, 64, 128, 4096,
                short.MaxValue, ushort.MaxValue, 0x10000, 0x80000
            };


            foreach (var validBufferSize in validBufferSizes)
            {
                testData.BufferSize = validBufferSize;

                Assert.Equal(validBufferSize, testData.BufferSize);
            }
        }

        #endregion


        #region ForEachRead

        [Fact]
        public void UnifiedData_ForEachRead_NullAction_Throws()
        {
            var testData = CreateTestData(0);

            Assert.Equal("action",
                Assert.Throws<ArgumentNullException>(() =>
                    testData.ForEachRead(null, CancellationToken.None))
                .ParamName);
        }

        [Fact]
        public void UnifiedData_ForEachRead_Works()
        {
            var validBufferSizes = new[] {
                1, 2, 4, 8, 9, 16, 32, 64, 128, 4096,
                short.MaxValue, ushort.MaxValue, 0x10000, 0x80000
            };


            foreach (var validBufferSize in validBufferSizes)
            {
                var testData = CreateTestData(validBufferSize);
                var lengthRead = 0;


                testData.BufferSize = validBufferSize;

                testData.ForEachRead(
                    (a, b, c) => { 
                        lengthRead += c;
                    },
                    CancellationToken.None);

                Assert.Equal(validBufferSize, lengthRead);
            }
        }

        #endregion

        #region ForEachGroup

        [Fact]
        public void UnifiedData_ForEachGroup_InvalidGroupSize_Throws()
        {
            var testData = CreateTestData(0);

            var invalidGroupSizes = new[] {
                int.MinValue, short.MinValue, -1, 0
            };


            foreach (var invalidGroupSize in invalidGroupSizes)
            {
                Assert.Equal("groupSize",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        testData.ForEachGroup(invalidGroupSize, (a, b, c) => { }, (a, b, c) => { }, CancellationToken.None))
                    .ParamName);
            }
        }

        [Fact]
        public void UnifiedData_ForEachGroup_NullAction_Throws()
        {
            var testData = CreateTestData(0);

            Assert.Equal("action",
                Assert.Throws<ArgumentNullException>(() =>
                    testData.ForEachGroup(1, (Action<byte[], int, int>) null, (a, b, c) => { }, CancellationToken.None))
                .ParamName);
        }

        [Fact]
        public void UnifiedData_ForEachGroup_NullRemainder_DoesNotThrow()
        {
            var testData = CreateTestData(0);


            testData.ForEachGroup(1, (a, b, c) => { }, null, CancellationToken.None);
        }

        [Fact]
        public void UnifiedData_ForEachGroup_Works()
        {
            var r = new Random();

            var validGroupSizes = new[] {
                1, 2, 4, 8, 16, 32, 64, 
                short.MaxValue, ushort.MaxValue, 0x10000, 0x80000
            };

            
            foreach (var validGroupSize in validGroupSizes)
            {
                // Test correct functioning when value is a multiple of groupSize
                for (int x = 0; x < 10; ++x)
                {
                    var dataLength = validGroupSize * (r.Next(0, 20) + 1);
                    var testData = CreateTestData(dataLength);


                    testData.ForEachGroup(validGroupSize, 
                        (dataBytes, position, length) => {
                            dataLength -= length;
                        },
                        (remainder, position, length) => {
                            // Since it is a multiple, it should never be called.
                            Assert.True(false);
                        }, 
                        CancellationToken.None);

                    Assert.Equal(0, dataLength);
                }

                if (validGroupSize > 1)
                {
                    // Test correct functioning when value is not a multiple of groupSize
                    for (int x = 0; x < 10; ++x)
                    {
                        var groupLength = validGroupSize * (r.Next(0, 20) + 1);
                        var remainderLength = r.Next(1, validGroupSize);

                        var testData = CreateTestData(groupLength + remainderLength);


                        var remainderCalls = 0;

                        testData.ForEachGroup(validGroupSize, 
                            (dataBytes, position, length) => {
                                groupLength -= length;
                            },
                            (remainder, position, length) => {
                                remainderLength -= length;
                                ++remainderCalls;
                            }, 
                            CancellationToken.None);

                        Assert.Equal(0, groupLength);
                        Assert.Equal(0, remainderLength);

                        Assert.Equal(1, remainderCalls);
                    }
                }
            }
        }

        #endregion
        
        #region ToArray

        [Fact]
        public void UnifiedData_ToArray_Works()
        {
            var testLengths = new[] {
                0, 1, 2, 4, 8, 16, 32, 64,
                short.MaxValue, ushort.MaxValue, 0x10000, 0x80000
            };


            foreach (var testLength in testLengths)
            {
                var testData = CreateTestData(testLength);

                Assert.Equal(testLength, 
                    testData.ToArray(CancellationToken.None)
                        .Length);
            }
        }

        #endregion


        protected abstract IUnifiedData CreateTestData(int length);
    }
}
