using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test.Mocks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Xunit;
using System.Data.HashFunction.Utilities.UnifiedData;

namespace System.Data.HashFunction.Test.Blake2
{
    public class Blake2B_Tests
    {

        #region Constructor

        #region HashSize

        [Fact]
        public void Blake2B_Constructor_InvalidHashSize_Throws()
        {
            var invalidHashSizes = new[] { -1, 0, 7, 9, 10, 11, 12, 13, 14, 15, 513, 520 };

            foreach (var invalidHashSize in invalidHashSizes)
            {
                Assert.Equal(
                    "hashSize",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                            new Blake2B(invalidHashSize))
                        .ParamName);
            }
        }

        [Fact]
        public void Blake2B_Constructor_ValidHashSize_Works()
        {
            // 8, 16, 24, 32, ..., 488, 496, 504, 512
            var validHashSizes = Enumerable.Range(1, 64)
                .Select(i => i * 8);

            foreach (var validHashSize in validHashSizes)
            {
                new Blake2B(validHashSize);
            }
        }

        #endregion

        #region Key

        [Fact]
        public void Blake2B_Constructor_InvalidKeyLength_Throws()
        {
            Assert.Equal("key",
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                        new Blake2B(key: new byte[65]))
                    .ParamName);
        }

        [Fact]
        public void Blake2B_Constructor_ValidKeyLength_Works()
        {
            // 0, 1, ..., 63, 64
            var validKeyLengths = Enumerable.Range(0, 65);

            foreach (var validKeyLength in validKeyLengths)
            {
                new Blake2B(key: new byte[validKeyLength]);
            }
        }

        #endregion

        #region Salt

        [Fact]
        public void Blake2B_Constructor_InvalidSaltLength_Throws()
        {
            var invalidSaltLengths = new[] { 0, 15, 17, 32, 64 };

            foreach (var invalidSaltLength in invalidSaltLengths)
            {
                Assert.Equal("salt",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                            new Blake2B(salt: new byte[invalidSaltLength]))
                        .ParamName);
            }
        }

        [Fact]
        public void Blake2B_Constructor_ValidSaltLength_Works()
        {
            new Blake2B(salt: new byte[16]);
        }

        #endregion

        #region Personalization

        [Fact]
        public void Blake2B_Constructor_InvalidPersonalizationLength_Throws()
        {
            var invalidPersonalizationLengths = new[] { 0, 15, 17, 32, 64 };

            foreach (var invalidPersonalizationLength in invalidPersonalizationLengths)
            {
                Assert.Equal("personalization",
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                            new Blake2B(personalization: new byte[invalidPersonalizationLength]))
                        .ParamName);
            }
        }

        [Fact]
        public void Blake2B_Constructor_ValidPersonalizationLength_Works()
        {
            new Blake2B(personalization: new byte[16]);
        }

        #endregion

        #endregion

    }
}
