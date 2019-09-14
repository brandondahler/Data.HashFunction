using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FNV;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FNV
{
    public class FNV1Base_Tests
    {
        private abstract class FNV1Impl
            : FNV1Base
        {
            private FNV1Impl(IFNVConfig config) 
                : base(config)
            {

            }

            public static UInt32[] _ExtendedMultiply(IReadOnlyList<UInt32> operand1, IReadOnlyList<UInt32> operand2, int hashSizeInBytes) =>
                ExtendedMultiply(operand1, operand2, hashSizeInBytes);

        }


        [Fact]
        public void FNV1Base_ExtendedMultiply_WorksConversly()
        {
            var x = new UInt32[] { 65536, 1024 };
            var y = new UInt32[] { 524288, 65536, 1024, 8 };

            var expectedValue = new UInt32[] { 0, 536870920, 134217729, 1572864 };


            Assert.Equal(expectedValue, FNV1Impl._ExtendedMultiply(x, y, 16));
            Assert.Equal(expectedValue, FNV1Impl._ExtendedMultiply(y, x, 16));

        }


    }
}
