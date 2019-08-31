using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Test._Mocks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.Test
{
    public abstract class IHashFunctionAsync_TestBase<IHashFunctionAsyncT>
        : IHashFunction_TestBase<IHashFunctionAsyncT>
        where IHashFunctionAsyncT : class, IHashFunctionAsync
    {
        [Fact]
        public async void IHashFunctionAsync_ComputeHashAsync_Stream_Seekable_MatchesKnownValues()
        {
            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);

                using (var ms = new SlowAsyncStream(new MemoryStream(knownValue.TestValue)))
                {
                    var hashResults = await hf.ComputeHashAsync(ms);

                    Assert.Equal(
                        new HashValue(knownValue.ExpectedValue.Take((hf.HashSizeInBits + 7) / 8), hf.HashSizeInBits),
                        hashResults);
                }
            }
        }


        [Fact]
        public async void IHashFunctionAsync_ComputeHashAsync_Stream_Seekable_MatchesKnownValues_SlowStream()
        {
            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);


                using (var ms = new SlowAsyncStream(new MemoryStream(knownValue.TestValue)))
                {
                    var hashResults = await hf.ComputeHashAsync(ms);

                    Assert.Equal(
                        new HashValue(knownValue.ExpectedValue.Take((hf.HashSizeInBits + 7) / 8), hf.HashSizeInBits),
                        hashResults);
                }
            }
        }
    }
}
