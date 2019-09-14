using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.Linq;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Core.Utilities
{
    public class HashValue_Tests
    {

        #region Constructor

        [Fact]
        public void HashValue_Constructor_ValidParameters_Works()
        {
            // Enumerable
            GC.KeepAlive(
                new HashValue(Enumerable.Range(0, 1).Select(i => (byte)i), 8));

            // Non-enumerable
            GC.KeepAlive(
                new HashValue(new byte[1], 8));
        }

        [Fact]
        public void HashValue_Constructor_CoerceToArray_WorksAsExpected()
        {
            var underlyingHashValues = new IEnumerable<byte>[] {
                Enumerable.Range(1, 2).Select(i => (byte) i),
                Enumerable.Range(1, 2).Concat(new[] { 0 }).Select(i => (byte) i),
                new List<byte>() { 1, 2 },
                new List<byte>() { 1, 2, 0 },
                new byte[] { 1, 2 },
                new byte[] { 1, 2, 0 }
            };

            foreach (var underlyingHashValue in underlyingHashValues)
            {
                var hashValues = new[] {
                    new HashValue(underlyingHashValue, 8),
                    new HashValue(underlyingHashValue, 9),
                    new HashValue(underlyingHashValue, 10),
                    new HashValue(underlyingHashValue, 16),
                    new HashValue(underlyingHashValue, 24)
                };

                Assert.Equal(new byte[] { 1 }, hashValues[0].Hash);
                Assert.Equal(new byte[] { 1, 0 }, hashValues[1].Hash);
                Assert.Equal(new byte[] { 1, 2 }, hashValues[2].Hash);
                Assert.Equal(new byte[] { 1, 2 }, hashValues[3].Hash);
                Assert.Equal(new byte[] { 1, 2, 0 }, hashValues[4].Hash);
            }
        }


        #region Hash

        [Fact]
        public void HashValue_Constructor_Hash_IsNull_Throws()
        {
            Assert.Equal(
                "hash",
                Assert.Throws<ArgumentNullException>(() =>
                        new HashValue(null, 8))
                    .ParamName);
        }

        #endregion

        #region BitLength

        [Fact]
        public void HashValue_Constructor_BitLength_IsInvalid_Throws()
        {
            var invalidBitLengths = new[] { int.MinValue, -1, 0 };

            foreach (var invalidBitLength in invalidBitLengths)
            {
                Assert.Equal(
                    "bitLength",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                            new HashValue(new byte[1], invalidBitLength))
                        .ParamName);
            }
        }

        #endregion

        #endregion

        #region Hash

        [Fact]
        public void HashValue_Hash_IsCopyOfConstructorValue()
        {
            // Underlying Enumerable
            {
                var enumerableValue = Enumerable.Range(2, 1).Select(i => (byte)i);

                var hashValue = new HashValue(enumerableValue, 8);

                Assert.NotStrictEqual(enumerableValue, hashValue.Hash);
                Assert.Equal(enumerableValue, hashValue.Hash);
            }

            // Underlying Array
            {
                var arrayValue = new byte[] { 2 };

                var hashValue = new HashValue(arrayValue, 8);

                Assert.NotStrictEqual(arrayValue, hashValue.Hash);
                Assert.Equal(arrayValue, hashValue.Hash);
            }
        }

        #endregion

        #region BitLength

        [Fact]
        public void HashValue_BitLength_IsSameAsConstructorValue()
        {
            var validBitLengths = Enumerable.Range(1, 16);

            foreach (var validBitLength in validBitLengths)
            {
                var hashValue = new HashValue(new byte[2], validBitLength);

                Assert.Equal(validBitLength, hashValue.BitLength);
            }
        }

        #endregion


        #region AsBase64String

        [Fact]
        public void HashValue_AsBase64String_ExpectedValues()
        {
            var knownValues = new[] {
                new { HashValue = new HashValue(Encoding.ASCII.GetBytes("f"), 8), Base64String = "Zg==" },
                new { HashValue = new HashValue(Encoding.ASCII.GetBytes("fo"), 16), Base64String = "Zm8=" },
                new { HashValue = new HashValue(Encoding.ASCII.GetBytes("foo"), 24), Base64String = "Zm9v" },
                new { HashValue = new HashValue(Encoding.ASCII.GetBytes("foob"), 32), Base64String = "Zm9vYg==" },
                new { HashValue = new HashValue(Encoding.ASCII.GetBytes("fooba"), 40), Base64String = "Zm9vYmE=" },
                new { HashValue = new HashValue(Encoding.ASCII.GetBytes("foobar"), 48), Base64String = "Zm9vYmFy" },
            };

            foreach (var knownValue in knownValues)
            {
                Assert.Equal(
                    knownValue.Base64String,
                    knownValue.HashValue.AsBase64String());
            }
        }

        #endregion

        #region AsBitArray

        [Fact]
        public void HashValue_AsBitArray_ExpectedValues()
        {
            var hashValue = new HashValue(new byte[] { 173 }, 8);
            var bitArray = hashValue.AsBitArray();

            Assert.True(bitArray[0]);
            Assert.False(bitArray[1]);
            Assert.True(bitArray[2]);
            Assert.True(bitArray[3]);
            Assert.False(bitArray[4]);
            Assert.True(bitArray[5]);
            Assert.False(bitArray[6]);
            Assert.True(bitArray[7]);
        }

        #endregion

        #region AsHexString

        [Fact]
        public void HashValue_AsHexString_ExpectedValue()
        {
            var hashValue = new HashValue(new byte[] { 173, 0, 255 }, 24);

            Assert.Equal(
                "ad00ff",
                hashValue.AsHexString());

            Assert.Equal(
                "ad00ff",
                hashValue.AsHexString(false));

            Assert.Equal(
                "AD00FF",
                hashValue.AsHexString(true));
        }
        #endregion

        #region GetHashCode

        [Fact]
        public void HashValue_GetHashCode_ExpectedValues()
        {
            var knownValues = new[] {
                new { HashValue = new HashValue(new byte[] { 0 }, 4), HashCode = 16213 },
                new { HashValue = new HashValue(new byte[] { 0 }, 8), HashCode = 16089 },
                new { HashValue = new HashValue(new byte[] { 0, 0 }, 12), HashCode = 494915 },
                new { HashValue = new HashValue(new byte[] { 0, 0 }, 16), HashCode = 521823 },
            };

            foreach (var knownValue in knownValues)
            {
                Assert.Equal(
                    knownValue.HashCode,
                    knownValue.HashValue.GetHashCode());
            }
        }

        #endregion

        #region Equals

        [Fact]
        public void HashValue_Equals_Works()
        {
            {
                var hashValue = new HashValue(new byte[] { 173, 0, 255 }, 24);

                Assert.False(hashValue.Equals(null));
                Assert.False(hashValue.Equals((object) null));
                Assert.False(hashValue.Equals("abc"));


                var mockValue = Mock.Of<IHashValue>(hv => hv.BitLength == 24 && hv.Hash == new byte[] { 173, 0, 255 });

                Assert.True(hashValue.Equals(mockValue));
                Assert.True(hashValue.Equals((object) mockValue));
            }

            {
                var hashValue = new HashValue(new byte[] { 173, 0, 254 }, 24);
                var mockValue = Mock.Of<IHashValue>(hv => hv.BitLength == 24 && hv.Hash == new byte[] { 173, 0, 255 });

                Assert.False(hashValue.Equals(mockValue));
            }

            {
                var hashValue = new HashValue(new byte[] { 173, 0, 254 }, 23);
                var mockValue = Mock.Of<IHashValue>(hv => hv.BitLength == 24 && hv.Hash == new byte[] { 173, 0, 254 });

                Assert.False(hashValue.Equals(mockValue));
            }

            {
                var hashValue = new HashValue(new byte[] { 173, 0, 255 }, 24);
                var mockValue = Mock.Of<IHashValue>(hv => hv.BitLength == 23 && hv.Hash == new byte[] { 173, 0, 127 });

                Assert.False(hashValue.Equals(mockValue));
            }
        }

        #endregion

    }
}
