using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.CityHash_Tests
{
    using System.Data.HashFunction;

    public class UInt128Tests
    {
        public class CityHash_UInt128
            : CityHash
        {
            [Fact]
            public void UInt128_Equals_Works()
            {
                UInt128_Equality_Test(
                    (a, b) => 
                        a.Equals(b));


                var baseValue = new UInt128();
                var falseTestValues = new object[] { null, "" };

                foreach (var falseTestValue in falseTestValues)
                {
                    Assert.False(
                        baseValue.Equals(falseTestValue));
                }
            }

            [Fact]
            public void UInt128_GetHashCode_Works()
            {
                var r = new Random();
                var valueSet = new HashSet<UInt128>();

                for (int x = 0; x < 5000; ++x)
                {
                    var testValueBytes = new byte[16];
                    r.NextBytes(testValueBytes);


                    Assert.True(
                        valueSet.Add(
                            new UInt128() {
                                Low = BitConverter.ToUInt64(testValueBytes, 0),
                                High = BitConverter.ToUInt64(testValueBytes, 8),
                            }));
                }
            }

            [Fact]
            public void UInt128_EqualOperator_Works()
            {
                UInt128_Equality_Test(
                    (a, b) =>
                        a == b);
            }

            [Fact]
            public void UInt128_NotEqualOperator_Works()
            {
                UInt128_Equality_Test(
                    (a, b) =>
                        !(a != b));
            }




            protected void UInt128_Equality_Test(Func<UInt128, UInt128, bool> equalityFunction)
            {
                var baseValue = new UInt128() { Low = 43590321, High = 12083290 };

                var falseTestValues = new[] { 
                    new UInt128() { Low = UInt64.MinValue, High = UInt64.MinValue },
                    
                    new UInt128() { Low = UInt64.MinValue, High = baseValue.High },
                    new UInt128() { Low = baseValue.Low - 1, High = baseValue.High },
                    new UInt128() { Low = baseValue.Low + 1, High = baseValue.High },
                    new UInt128() { Low = UInt64.MaxValue, High = baseValue.High },

                    new UInt128() { Low = baseValue.Low, High = UInt64.MinValue },
                    new UInt128() { Low = baseValue.Low, High = baseValue.High - 1 },
                    new UInt128() { Low = baseValue.Low, High = baseValue.High + 1 },
                    new UInt128() { Low = baseValue.Low, High = UInt64.MaxValue },

                    new UInt128() { Low = UInt64.MaxValue, High = UInt64.MaxValue },
                };


                foreach (var falseTestValue in falseTestValues)
                {
                    Assert.False(
                        equalityFunction(baseValue, falseTestValue));
                }



                var trueTestValues = new[] {
                    baseValue,
                    new UInt128() { Low = baseValue.Low, High = baseValue.High },
                };


                foreach (var trueTestValue in trueTestValues)
                {
                    Assert.True(
                        equalityFunction(baseValue, trueTestValue));
                }
            }

        }
    }
}
