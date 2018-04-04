﻿using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash.FarmHashSharp;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash.FarmHashSharp
{
    public class FarmHashSharpHash32_Implementation_Tests
    {

        #region Constructor

        [Fact]
        public void FarmHashSharpHash32_Implementation_Constructor_Works()
        {
            GC.KeepAlive(
                new FarmHashSharpHash32_Implementation());
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void FarmHashSharpHash32_Implementation_HashSizeInBits_Is32()
        {
            var farmHash = new FarmHashSharpHash32_Implementation();

            Assert.Equal(32, farmHash.HashSizeInBits);
        }

        #endregion



        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IFarmHashSharpHash32>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar.Take(3), 0x6b5025e3),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(17), 0xe3e27892),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x6482ed0d),
                };

            protected override IFarmHashSharpHash32 CreateHashFunction(int hashSize) =>
                new FarmHashSharpHash32_Implementation();
        }
    }
}
