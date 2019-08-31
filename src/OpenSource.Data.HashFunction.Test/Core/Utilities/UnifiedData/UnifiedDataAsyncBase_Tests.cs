using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core.Utilities;
using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Core.Utilities.UnifiedData
{
    using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
    using System.Threading;

    public abstract class UnifiedDataAsyncBase_Tests
        : UnifiedDataBase_Tests
    {
        
        #region ForEachReadAsync

        [Fact]
        public async void UnifiedDataAsync_ForEachReadAsync_NullAction_Throws()
        {
            var testData = CreateTestDataAsync(0);

            Assert.Equal("action",
                (await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await testData.ForEachReadAsync(null, CancellationToken.None)))
                .ParamName);
        }

        [Fact]
        public async void UnifiedDataAsync_ForEachReadAsync_Works()
        {
            var validBufferSizes = new[] {
                1, 2, 4, 8, 9, 16, 32, 64, 128, 4096,
                short.MaxValue, ushort.MaxValue, 0x10000, 0x80000
            };


            foreach (var validBufferSize in validBufferSizes)
            {
                var testData = CreateTestDataAsync(validBufferSize);
                var lengthRead = 0;


                testData.BufferSize = validBufferSize;

                await testData.ForEachReadAsync(
                    (a, b, c) => {
                        lengthRead += c;
                    }, 
                    CancellationToken.None);

                Assert.Equal(validBufferSize, lengthRead);
            }
        }

        #endregion

        #region ForEachGroupAsync

        [Fact]
        public async void UnifiedDataAsync_ForEachGroupAsync_InvalidGroupSize_Throws()
        {
            var testData = CreateTestDataAsync(0);

            var invalidGroupSizes = new[] {
                int.MinValue, short.MinValue, -1, 0
            };


            foreach (var invalidGroupSize in invalidGroupSizes)
            {
                Assert.Equal("groupSize",
                    (await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                        await testData.ForEachGroupAsync(invalidGroupSize, (a, b, c) => { }, (a, b, c) => { }, CancellationToken.None)))
                    .ParamName);
            }
        }

        [Fact]
        public async void UnifiedDataAsync_ForEachGroupAsync_NullAction_Throws()
        {
            var testData = CreateTestDataAsync(0);

            Assert.Equal("action",
                (await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await testData.ForEachGroupAsync(1, null, (a, b, c) => { }, CancellationToken.None)))
                .ParamName);            
        }

        [Fact]
        public async void UnifiedDataAsync_ForEachGroupAsync_NullRemainder_DoesNotThrow()
        {
            var testData = CreateTestDataAsync(0);


            await testData.ForEachGroupAsync(1, (a, b, c) => { }, null, CancellationToken.None);
        }

        [Fact]
        public async void UnifiedDataAsync_ForEachGroupAsync_Works()
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
                    var testData = CreateTestDataAsync(dataLength);


                    await testData.ForEachGroupAsync(validGroupSize, 
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

                        var testData = CreateTestDataAsync(groupLength + remainderLength);


                        var remainderCalls = 0;

                        await testData.ForEachGroupAsync(validGroupSize, 
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
        
        #region ToArrayAsync

        [Fact]
        public async void UnifiedDataAsync_ToArrayAsync_Works()
        {
            var testLengths = new[] {
                0, 1, 2, 4, 8, 16, 32, 64,
                short.MaxValue, ushort.MaxValue, 0x10000, 0x80000
            };


            foreach (var testLength in testLengths)
            {
                var testData = CreateTestDataAsync(testLength);

                Assert.Equal(testLength,
                    (await testData.ToArrayAsync(CancellationToken.None))
                        .Length);
            }
        }

        #endregion

        
        protected override IUnifiedData CreateTestData(int length)
        {
            return CreateTestDataAsync(length);
        }

        protected abstract IUnifiedDataAsync CreateTestDataAsync(int length);

    }
}
