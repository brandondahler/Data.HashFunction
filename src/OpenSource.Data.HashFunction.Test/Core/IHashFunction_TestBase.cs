using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test._Mocks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Test._Utilities;

namespace System.Data.HashFunction.Test
{
    public abstract class IHashFunction_TestBase<IHashFunctionT>
        where IHashFunctionT : class, IHashFunction
    {
        protected abstract IEnumerable<KnownValue> KnownValues { get; }
        

        [Fact]
        public void IHashFunction_ComputeHash_ByteArray_MatchesKnownValues()
        {

            foreach (var knownValue in KnownValues)
            {
                var hf = CreateHashFunction(knownValue.HashSize);
                var hashResults = hf.ComputeHash(knownValue.TestValue);

                Assert.Equal(
                    new HashValue(knownValue.ExpectedValue.Take((hf.HashSizeInBits + 7) / 8), hf.HashSizeInBits), 
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
                        new HashValue(knownValue.ExpectedValue.Take((hf.HashSizeInBits + 7) / 8), hf.HashSizeInBits), 
                        hashResults);
                }
            }
        }



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
                : this(hashSize, utf8Value.ToBytes(), ToBytes(expectedValue, 32)) { }

            public KnownValue(int hashSize, string utf8Value, UInt64 expectedValue)
                : this(hashSize, utf8Value.ToBytes(), ToBytes(expectedValue, 64)) { }


            public KnownValue(int hashSize, IEnumerable<byte> value, string expectedValue)
                : this(hashSize, value, expectedValue.HexToBytes()) { }

            public KnownValue(int hashSize, IEnumerable<byte> value, UInt32 expectedValue)
                : this(hashSize, value, ToBytes(expectedValue, 32)) { }

            public KnownValue(int hashSize, IEnumerable<byte> value, UInt64 expectedValue)
                : this(hashSize, value, ToBytes(expectedValue, 64)) { }
        }


        protected abstract IHashFunctionT CreateHashFunction(int hashSize);


        private static byte[] ToBytes(UInt64 value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 64)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength but be in the range [1, 64].");


            value &= (UInt64.MaxValue >> (64 - bitLength));


            var valueBytes = new byte[(bitLength + 7) / 8];

            for (int x = 0; x < valueBytes.Length; ++x)
            {
                valueBytes[x] = (byte)value;
                value >>= 8;
            }

            return valueBytes;
        }

    }
}
