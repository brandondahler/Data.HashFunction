using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities
{
    public class StreamExtensionsTests
    {
        #region Stream.AsEnumerable

        public class AsEnumerableTests
        {

            [Fact]
            public void Stream_AsEnumerable_Works()
            {
                var r = new Random();

                var randomArray = new byte[8096];
                r.NextBytes(randomArray);


                using (var ms = new MemoryStream(randomArray))
                    Assert.Equal(randomArray, ms.AsEnumerable());
            }

            [Fact]
            public void Stream_AsEnumerable_WithBufferSize_Works()
            {
                var r = new Random();

                var randomArray = new byte[8096];
                r.NextBytes(randomArray);


                using (var testStream = new MemoryStream(randomArray))
                    Assert.Equal(randomArray, testStream.AsEnumerable(r.Next(0, 8096)));
            }

            [Fact]
            public void Stream_AsEnumerable_NullSource_Throws()
            {
                Stream testStream = null;

                Assert.Equal("source", 
                    Assert.Throws<ArgumentNullException>(() =>
                        testStream.AsEnumerable()
                            .GetEnumerator()
                            .MoveNext())
                    .ParamName);
            }

            [Fact]
            public void Stream_AsEnumerable_UnreadableSource_Throws()
            {
                var unreadableSourceMock = new Mock<MemoryStream>();

                unreadableSourceMock.SetupGet(us => us.CanRead)
                    .Returns(false);


                var testStream = unreadableSourceMock.Object;

                Assert.Equal("source",
                    Assert.Throws<ArgumentException>(() => 
                        ((Stream) unreadableSourceMock.Object).AsEnumerable()
                            .GetEnumerator()
                            .MoveNext())
                    .ParamName);
            }

            [Fact]
            public void Stream_AsEnumerable_InvalidBufferSize_Throws()
            {
                foreach (var invalidBufferSizes in new[] { 0, -1, -65536, int.MinValue })
                {
                    using (var testStream = new MemoryStream())
                    {
                        Assert.Equal("bufferSize",
                            Assert.Throws<ArgumentOutOfRangeException>(() =>
                               testStream.AsEnumerable(invalidBufferSizes)
                                    .GetEnumerator()
                                    .MoveNext())
                            .ParamName);
                    }
                }
            }

        }

        #endregion

        #region Stream.AsGroupedStreamData()

        public class AsGroupedStreamDataTests
        {
            [Fact]
            public void Stream_AsGroupedStreamData_Works()
            {
                var r = new Random();

                var testArray = new byte[8096];
                r.NextBytes(testArray);


                using (var testStream = new MemoryStream(testArray))
                {
                    var groupedStreamData = testStream.AsGroupedStreamData(1);


                    Assert.NotNull(groupedStreamData);
                    Assert.Equal(
                        testArray.AsEnumerable(),
                        groupedStreamData.SelectMany(a => a));


                    Assert.Empty(groupedStreamData.Remainder);
                }

            }

            [Fact]
            public void Stream_AsGroupedStreamData_NullSource_Throws()
            {
                Stream testStream = null;

                Assert.Equal("source", 
                    Assert.Throws<ArgumentNullException>(() => 
                        testStream.AsGroupedStreamData(1))
                    .ParamName);
            }

            [Fact]
            public void Stream_AsGroupedStreamData_UnreadableSource_Throws()
            {
                var unreadableSourceMock = new Mock<MemoryStream>();

                unreadableSourceMock.SetupGet(us => us.CanRead)
                    .Returns(false);


                var testStream = unreadableSourceMock.Object;

                Assert.Equal("source",
                    Assert.Throws<ArgumentException>(() =>
                        testStream.AsGroupedStreamData(1))
                    .ParamName);
            }

            [Fact]
            public void Stream_AsGroupedStreamData_InvalidBufferSize_Throws()
            {
                foreach (var invalidGroupSize in new[] { 0, -1, -65536, int.MinValue })
                {
                    using (var testStream = new MemoryStream())
                    {
                        Assert.Equal("groupSize",
                            Assert.Throws<ArgumentOutOfRangeException>(() =>
                                testStream.AsGroupedStreamData(invalidGroupSize))
                            .ParamName);
                    }
                }
            }
        }

        #endregion

        #region Stream.GroupedStreamData

        public class GroupedStreamDataTests
        {

            [Fact]
            public void GroupedStreamData_Works()
            {
                var r = new Random();

                var testArray = new byte[8096];
                r.NextBytes(testArray);

                
                foreach (var groupSize in Enumerable.Range(1, 32).Select(i => i * 256))
                {
                    using (var testStream = new MemoryStream(testArray))
                {
                        var groupedStreamData = new StreamExtensions.GroupedStreamData(testStream, groupSize);

                        var groups = groupedStreamData.ToArray();
                        var remainder = groupedStreamData.Remainder;


                        Assert.Equal(
                            testArray.Length / groupSize,
                            groups.Length);

                        Assert.Equal(
                            testArray.Length % groupSize,
                            remainder.Length);


                        for (int x = 0; x < groups.Length; ++x)
                        {
                            for (int y = 0; y < groups[x].Length; ++y)
                            {
                                Assert.Equal(
                                    testArray[(x * groupSize) + y],
                                    groups[x][y]);
                            }
                        }

                        for (int x = 0; x < remainder.Length; ++x)
                        {
                            Assert.Equal(
                                testArray[(groups.Length * groupSize) + x],
                                remainder[x]);
                        }
                    }
                }
            }


            [Fact]
            public void GroupedStreamData_Constructor_NullSource_Throws()
            {
                Stream testStream = null;

                Assert.Equal("source",
                    Assert.Throws<ArgumentNullException>(() =>
                        new StreamExtensions.GroupedStreamData(testStream, 1))
                    .ParamName);
            }

            [Fact]
            public void GroupedStreamData_Constructor_UnreadableSource_Throws()
            {
                var unreadableSourceMock = new Mock<MemoryStream>();

                unreadableSourceMock.SetupGet(us => us.CanRead)
                    .Returns(false);


                var testStream = unreadableSourceMock.Object;

                Assert.Equal("source",
                    Assert.Throws<ArgumentException>(() =>
                        new StreamExtensions.GroupedStreamData(testStream, 1))
                    .ParamName);
            }

            [Fact]
            public void GroupedStreamData_Constructor_InvalidBufferSize_Throws()
            {
                foreach (var invalidGroupSize in new[] { 0, -1, -65536, int.MinValue })
                {
                    using (var testStream = new MemoryStream())
                    {
                        Assert.Equal("groupSize",
                            Assert.Throws<ArgumentOutOfRangeException>(() =>
                                new StreamExtensions.GroupedStreamData(testStream, invalidGroupSize))
                            .ParamName);
                    }
                }
            }


            [Fact]
            public void GroupedStreamData_Remainder_NotEnumerated_Throws()
            {
                using (var testStream = new MemoryStream())
                {
                    var groupedStreamData = new StreamExtensions.GroupedStreamData(testStream, 1);

                    Assert.Throws<InvalidOperationException>(() =>
                        groupedStreamData.Remainder);
                }
            }

            [Fact]
            public void GroupedStreamData_GetEnumerator_IEnumerables_Equal()
            {
                var r = new Random();

                var testArray = new byte[8096];
                r.NextBytes(testArray);


                using (var testStream = new MemoryStream(testArray))
                {
                    var groupedStreamData = new StreamExtensions.GroupedStreamData(testStream, 1);


                    var expected = groupedStreamData.ToList();

                    testStream.Seek(0, SeekOrigin.Begin);
                    

                    var actual = new List<byte[]>();

                    foreach (var value in ((IEnumerable) groupedStreamData))
                        actual.Add((byte[]) value);

                    
                    Assert.Equal(
                        expected, 
                        actual);
                }
            }

        }

        #endregion

    }
}
