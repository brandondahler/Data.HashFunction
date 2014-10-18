using Moq;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Data.HashFunction.Test.Mocks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;

namespace System.Data.HashFunction.Test
{
    public abstract class IHashFunctionAsyncTests<IHashFunctionAsyncT>
        : IHashFunctionTests<IHashFunctionAsyncT>
        where IHashFunctionAsyncT : class, IHashFunctionAsync
    {


        [Fact]
        public void IHashFunction_ComputeHashAsync_Stream_InvalidHashSize_Throws()
        {
            var hashSizes = KnownValues.Select(kv => kv.HashSize)
                .Distinct();

            // Ignore if hash function does not seem to have a configurable hashSize constructor.
            foreach (var hashSize in hashSizes)
            {
                Mock<IHashFunctionAsyncT> hashFunctionMock = CreateHashFunctionMock(hashSize);
                hashFunctionMock.CallBase = true;

                hashFunctionMock
                    .SetupGet(hf => hf.HashSize)
                    .Returns(-1);


                var hashFunction = hashFunctionMock.Object;

                using (var ms = new SlowAsyncStream(new MemoryStream()))
                {
                    var aggregateException = 
                        Assert.Throws<AggregateException>(() =>
                            hashFunction.ComputeHashAsync(ms).Wait());

                    var resultingException = 
                        Assert.Single(aggregateException.InnerExceptions);

                    Assert.Contains("HashSize",
                        Assert.IsType<InvalidOperationException>(
                            resultingException)
                        .Message);
                }
            }
        }


        [Fact]
        public void IHashFunctionAsync_ComputeHashAsync_Stream_Seekable_MatchesKnownValues()
        {
            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);

                using (var ms = new SlowAsyncStream(new MemoryStream(knownValue.TestValue)))
                {
                    var hashResults = hf.ComputeHashAsync(ms).Result;

                    Assert.Equal(
                        knownValue.ExpectedValue.Take((hf.HashSize + 7) / 8),
                        hashResults);
                }
            }
        }

        [Fact]
        public void IHashFunctionAsync_ComputeHashAsync_Stream_NonSeekable_MatchesKnownValues()
        {
            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);

                using (var ms = new SlowAsyncStream(new NonSeekableMemoryStream(knownValue.TestValue)))
                {
                    var hashResults = hf.ComputeHashAsync(ms).Result;

                    Assert.Equal(
                        knownValue.ExpectedValue.Take((hf.HashSize + 7) / 8),
                        hashResults);
                }
            }
        }


        [Fact]
        public void IHashFunctionAsync_ComputeHashAsync_Stream_Seekable_MatchesKnownValues_SlowStream()
        {
            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);


                using (var ms = new SlowAsyncStream(new MemoryStream(knownValue.TestValue)))
                {
                    var hashResults = hf.ComputeHashAsync(ms).Result;

                    Assert.Equal(
                        knownValue.ExpectedValue.Take((hf.HashSize + 7) / 8),
                        hashResults);
                }
            }
        }
    }
}
