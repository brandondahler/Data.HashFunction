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
    public abstract class IHashFunctionTests<IHashFunctionT>
        where IHashFunctionT : class, IHashFunction
    {
        protected abstract IEnumerable<KnownValue> KnownValues { get; }

        private readonly TimeSpan SpeedTestTarget = new TimeSpan(0, 0, 1);
        //private readonly int SpeedTestMaxSizes = 6;


        [Fact]
        public void IHashFunction_Constructor_InvalidHashSize_Throws()
        {
            // Ignore if hash function does not seem to have a configurable hashSize constructor.
            if (KnownValues.Select(kv => kv.HashSize).Distinct().Count() <= 1)
                return;



            Exception resultingException = null;
            
            try
            {
                GC.KeepAlive(
                    CreateHashFunction(-1));
            } catch (Exception e) {
                resultingException = e;
            }

            Assert.NotNull(resultingException);


            if (resultingException is TargetInvocationException)
                resultingException = resultingException.InnerException;


            Assert.Equal("hashSize",
                Assert.IsType<ArgumentOutOfRangeException>(
                    resultingException)
                .ParamName);
        }

        [Fact]
        public void IHashFunction_ComputeHash_ByteArray_InvalidHashSize_Throws()
        {
            var hashSizes = KnownValues.Select(kv => kv.HashSize)
                .Distinct();

            // Ignore if hash function does not seem to have a configurable hashSize constructor.
            foreach (var hashSize in hashSizes)
            {
                Mock<IHashFunctionT> hashFunctionMock = CreateHashFunctionMock(hashSize);
                hashFunctionMock.CallBase = true;

                hashFunctionMock
                    .SetupGet(hf => hf.HashSize)
                    .Returns(-1);


                var hashFunction = hashFunctionMock.Object;

                Assert.Contains("HashSize",
                    Assert.Throws<InvalidOperationException>(() =>
                        hashFunction.ComputeHash(new byte[0]))
                    .Message);
            }
        }

        [Fact]
        public void IHashFunction_ComputeHash_ByteArray_MatchesKnownValues()
        {

            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);
                var hashResults = hf.ComputeHash(knownValue.TestValue);

                Assert.Equal(
                        knownValue.ExpectedValue.Take((hf.HashSize + 7) / 8), 
                        hashResults);
            }
        }

        [Fact]
        public void IHashFunction_ComputeHash_Stream_Seekable_MatchesKnownValues()
        {
            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);

                using (var ms = new MemoryStream(knownValue.TestValue))
                {
                    var hashResults = hf.ComputeHash(ms);

                    Assert.Equal(
                        knownValue.ExpectedValue.Take((hf.HashSize + 7) / 8), 
                        hashResults);
                }
            }
        }

        [Fact]
        public void IHashFunction_ComputeHash_Stream_NonSeekable_MatchesKnownValues()
        {
            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);

                var msMock = new Mock<MemoryStream>(knownValue.TestValue) { CallBase = true };

                msMock.SetupGet(ms => ms.CanSeek)
                    .Returns(false);

                msMock.SetupGet(ms => ms.Length)
                    .Throws(new NotSupportedException("Seeking not supported.  Hash function likely needs RequiresSeekableStream override."));


                using (var ms = msMock.Object)
                {
                    var hashResults = hf.ComputeHash(ms);

                    Assert.Equal(
                        knownValue.ExpectedValue.Take((hf.HashSize + 7) / 8), 
                        hashResults);
                }
            }
        }

        //[Fact]
        //[Fact(Skip = "SpeedTest is for relative benchmarking only.")]
        //public void IHashFunction_ComputeHash_ByteArray_SpeedTest_ByteArray()
        //{
        //    var hf = CreateHashFunction();

        //    var kvBytes = new[] {
        //        TestConstants.Empty,
        //        TestConstants.FooBar,
        //        TestConstants.LoremIpsum,
        //        TestConstants.RandomShort,
        //        TestConstants.RandomLong
        //    };

        //    var testHashSizes = hf.ValidHashSizes;

        //    if (testHashSizes.Count() > SpeedTestMaxSizes)
        //    {
        //        var takeStep = (int) Math.Ceiling((double)hf.ValidHashSizes.Count() / (SpeedTestMaxSizes - 1));

        //        testHashSizes = testHashSizes.TakeEvery(takeStep)
        //            .Concat(hf.ValidHashSizes.Last());
        //    }

        //    foreach (var hashSize in testHashSizes)
        //    {
        //        hf.HashSize = hashSize;

        //        long testCount = 1000;
        //        var sw = new Stopwatch();
                
        //        int tries;
        //        for (tries = 0; tries < 10; ++tries)
        //        {
        //            sw.Restart();

        //            Parallel.For(0, testCount, (x) => {
        //                hf.ComputeHash(kvBytes[x % kvBytes.Length]);
        //            });

        //            sw.Stop();

        //            if (sw.Elapsed.Ticks >= (SpeedTestTarget.Ticks * 0.9))
        //                break;

        //            var testCountMultiplier = ((double)SpeedTestTarget.Ticks / sw.Elapsed.Ticks);

        //            if (testCountMultiplier < 1.5d)
        //                testCountMultiplier = 1.5d;

        //            testCount = (long) (testCount * testCountMultiplier);
        //        }


        //        Console.WriteLine("{0} bits - {1:N3} hashes/sec ({2:N} in {3}ms, {4} tries)", 
        //            hashSize,
        //            (testCount / (sw.ElapsedMilliseconds / 1000.0d)),
        //            testCount, 
        //            sw.ElapsedMilliseconds,
        //            tries);
        //    }
        //}




        protected class KnownValue
        {
            public readonly int HashSize;
            public readonly byte[] TestValue;
            public readonly byte[] ExpectedValue;
            

            public KnownValue(int hashSize, IEnumerable<byte> testValue, IEnumerable<byte> expectedValue)
            {
                TestValue = testValue.ToArray();
                ExpectedValue = expectedValue.ToArray();
                HashSize = hashSize;
            }


            public KnownValue(int hashSize, string utf8Value, string expectedValue)
                : this(hashSize, utf8Value.ToBytes(), expectedValue.HexToBytes()) { }

            public KnownValue(int hashSize, string utf8Value, UInt32 expectedValue)
                : this(hashSize, utf8Value.ToBytes(), expectedValue.ToBytes(32)) { }

            public KnownValue(int hashSize, string utf8Value, UInt64 expectedValue)
                : this(hashSize, utf8Value.ToBytes(), expectedValue.ToBytes(64)) { }


            public KnownValue(int hashSize, IEnumerable<byte> value, string expectedValue)
                : this(hashSize, value, expectedValue.HexToBytes()) { }

            public KnownValue(int hashSize, IEnumerable<byte> value, UInt32 expectedValue)
                : this(hashSize, value, expectedValue.ToBytes(32)) { }

            public KnownValue(int hashSize, IEnumerable<byte> value, UInt64 expectedValue)
                : this(hashSize, value, expectedValue.ToBytes(64)) { }
        }


        protected abstract IHashFunctionT CreateHashFunction(int hashSize);

        protected abstract Mock<IHashFunctionT> CreateHashFunctionMock(int hashSize);

    }



    internal sealed class TestConstants
    {
        // Constant values available for KnownValues to use.
        public static readonly byte[] Empty = new byte[0];
        public static readonly byte[] FooBar = "foobar".ToBytes();

        public static readonly byte[] LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.  Ut ornare aliquam mauris, at volutpat massa.  Phasellus pulvinar purus eu venenatis commodo.".ToBytes();

        public static readonly byte[] RandomShort = "55d0e01ec669dc69".HexToBytes();
        public static readonly byte[] RandomLong = "1122eeba86d52989b26b0efd2be8d091d3ad307b771ff8d1208104f9aa40b12ab057a0d78656ba037e475178c159bf3ee64dcd279610d64bb7888a97211884c7a894378263135124720ef6ef560da6c85fb491cb732b331e89bcb00e7daef271e127483e91b189ceeaf2f6711394e2eca07fb4db62c5a8fd8195ae3b39da63".HexToBytes();
    }
}
