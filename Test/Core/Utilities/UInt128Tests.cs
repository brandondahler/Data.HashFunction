using System;
using System.Collections.Generic;
using System.Data.HashFunction;
using System.Data.HashFunction.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities
{

    public class UInt128Tests
    {

        #region Object Overrides

        #region Equals

        [Fact]
        public void UInt128_Equals_Inequivalent_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };

            var testValues = new object[] {
                null, 
                "", 
                new UInt128() { Low = 2 }, 
                new UInt128 { High = 1 } 
            };

            
            foreach (var testValue in testValues)
            {
                Assert.False(
                    baseValue.Equals(testValue));
            }
        }

        [Fact]
        public void UInt128_Equals_Equivalent_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };

            var testValues = new object[] { 
                baseValue, 
                new UInt128() { High = 1, Low = 2 } 
            };

            foreach (var testValue in testValues)
            {
                Assert.True(
                    baseValue.Equals(testValue));
            }
        }

        #endregion

        #region GetHashCode

        [Fact]
        public void UInt128_GetHashCode_ProducesDistinctValues()
        {
            var values = Enumerable.Range(1, 256)
                .Select(i => new UInt128() { High = 0, Low = (ulong) i });


            var hashCodeSet = new HashSet<int>();


            foreach (var value in values)
            {
                Assert.True(
                    hashCodeSet.Add(
                        value.GetHashCode()));
            }
        }

        [Fact]
        public void UInt128_GetHashCode_ProducesRepeatableValues()
        {
            var values = new[] { 
                new UInt128() { High = 0, Low = 0 },
                new UInt128() { High = 0, Low = 0 },
                new UInt128() { High = 0, Low = 0 },
                new UInt128() { High = 0, Low = 0 },
            };


            foreach (var value1 in values)
            {
                foreach (var value2 in values)
                {
                    Assert.Equal(
                        value1.GetHashCode(),
                        value2.GetHashCode());
                }
            }
        }

        #endregion

        #region ToString

        [Fact]
        public void UInt128_ToString_ContainsHighAndLow()
        {
            var baseValue = new UInt128() { Low = 1, High = 2 };


            var baseValueString = baseValue.ToString();


            Assert.Contains("1", baseValueString);
            Assert.Contains("2", baseValueString);
        }

        #endregion

        #endregion


        #region IComparable

        #region CompareTo

        [Fact]
        public void UInt128_CompareTo_Null_ReturnsGreaterThanZero()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };


            Assert.True(
                baseValue.CompareTo((object) null) > 0);
        }
        
        [Fact]
        public void UInt128_CompareTo_InvalidType_Throws()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };


            Assert.Throws<ArgumentException>(() => 
                baseValue.CompareTo((object) 0));

            Assert.Throws<ArgumentException>(() =>
                baseValue.CompareTo(new object()));
        }

        [Fact]
        public void UInt128_CompareTo_InequivalentLess_ReturnsLessThanZero()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };
            var testValue = (object) new UInt128() { High = 0, Low = 0 };


            Assert.True(
                baseValue.CompareTo(testValue) < 0);
        }

        [Fact]
        public void UInt128_CompareTo_InequivalentGreater_ReturnsGreaterThanZero()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };
            var testValue = (object) new UInt128() { High = 2, Low = 0 };


            Assert.True(
                baseValue.CompareTo(testValue) > 0);
        }

        [Fact]
        public void UInt128_CompareTo_Equivalent_ReturnsZero()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };

            var testValues = new object[] { 
                baseValue,
                new UInt128() { High = 1, Low = 2 }
            };


            foreach (var testValue in testValues)
            {
                Assert.Equal(
                    0,
                    baseValue.CompareTo(testValue));
            }

        }

        #endregion

        #endregion

        #region IComparable<UInt128>

        #region CompareTo

        [Fact]
        public void UInt128_CompareTo_UInt128_InequivalentLess_ReturnsLessThanZero()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };
            var testValue = new UInt128() { High = 0, Low = 0 };


            Assert.True(
                baseValue.CompareTo(testValue) < 0);
        }

        [Fact]
        public void UInt128_CompareTo_UInt128_InequivalentLess_ReturnsGreaterThanZero()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };
            var testValue = new UInt128() { High = 2, Low = 0 };


            Assert.True(
                baseValue.CompareTo(testValue) > 0);
        }

        [Fact]
        public void UInt128_CompareTo_UInt128_Equivalent_ReturnsZero()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };

            var testValues = new[] { 
                baseValue,
                new UInt128() { High = 1, Low = 2 }
            };


            foreach (var testValue in testValues)
            {
                Assert.Equal(
                    0,
                    baseValue.CompareTo(testValue));
            }

        }

        #endregion

        #endregion

        #region IEquatable<UInt128>

        #region Equals

        [Fact]
        public void UInt128_Equals_UInt128_Inequivalent_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };

            var testValues = new[] { 
                new UInt128() { Low = 2 }, 
                new UInt128() { High = 1 },
            };


            foreach (var testValue in testValues)
            {
                Assert.False(
                    baseValue.Equals(testValue));
            }
        }

        [Fact]
        public void UInt128_Equals_UInt128_Equivalent_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };
            
            var testValues = new[] { 
                baseValue, 
                new UInt128() { High = 1, Low = 2 } 
            };


            foreach (var testValue in testValues)
            {
                Assert.True(
                    baseValue.Equals(testValue));
            }
        }

        #endregion

        #endregion


        #region Static Operators

        #region implicit operator UInt128
        
        [Fact]
        public void UInt128_implicit_UInt128_UInt16_ConvertsCorrectly()
        {
            var expected = new UInt128() { High = 0, Low = 1 };
            UInt16 testValue = 1;

            Assert.Equal(
                expected,
                testValue);
        }


        [Fact]
        public void UInt128_implicit_UInt128_UInt32_ConvertsCorrectly()
        {
            var expected = new UInt128() { High = 0, Low = 1 };
            UInt32 testValue = 1;

            Assert.Equal(
                expected,
                testValue);
        }


        [Fact]
        public void UInt128_implicit_UInt128_UInt64_ConvertsCorrectly()
        {
            var expected = new UInt128() { High = 0, Low = 1 };
            UInt64 testValue = 1;

            Assert.Equal(
                expected,
                testValue);
        }

        #endregion


        #region operator ++

        [Fact]
        public void UInt128_IncrementOperator_PostIncrement_Increments()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };
            var expected = new UInt128() { High = 0, Low = 2 };


            Assert.Equal(
                baseValue, 
                baseValue++);

            Assert.Equal(
                expected,
                baseValue);
        }

        [Fact]
        public void UInt128_IncrementOperator_PreIncrement_Increments()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };
            var expected = new UInt128() { High = 0, Low = 2 };


            Assert.Equal(
                expected,
                ++baseValue);

            Assert.Equal(
                expected,
                baseValue);
        }

        #endregion

        #region operator --

        [Fact]
        public void UInt128_DecrementOperator_PostDecrement_Decrements()
        {
            var baseValue = new UInt128() { High = 0, Low = 2 };
            var expected = new UInt128() { High = 0, Low = 1 };


            Assert.Equal(
                baseValue,
                baseValue--);

            Assert.Equal(
                expected,
                baseValue);
        }

        [Fact]
        public void UInt128_DecrementOperator_PreDecrement_Decrements()
        {
            var baseValue = new UInt128() { High = 0, Low = 2 };
            var expected = new UInt128() { High = 0, Low = 1 };


            Assert.Equal(
                expected,
                --baseValue);

            Assert.Equal(
                expected,
                baseValue);
        }

        #endregion


        #region operator ==

        [Fact]
        public void UInt128_EqualOperator_Inequivalent_ReturnsFalse()
        {
            var baseValue = new UInt128() { Low = 1, High = 2 };

            var testValues = new[] {
                new UInt128() { Low = baseValue.Low, High = baseValue.High + 1 },
                new UInt128() { Low = baseValue.Low + 1, High = baseValue.High },
            };


            foreach (var testValue in testValues)
            {
                Assert.False(
                    baseValue == testValue);
            }
        }

        [Fact]
        public void UInt128_EqualOperator_Equivalent_ReturnsTrue()
        {
            var baseValue = new UInt128() { Low = 1, High = 2 };

            var testValues = new[] {
                baseValue,
                new UInt128() { Low = baseValue.Low, High = baseValue.High },
            };


            foreach (var testValue in testValues)
            {
                Assert.True(
                    baseValue == testValue);
            }
        }

        #endregion

        #region operator !=

        [Fact]
        public void UInt128_NotEqualOperator_Equivalent_ReturnsFalse()
        {
            var baseValue = new UInt128() { Low = 1, High = 2 };

            var testValues = new[] {
                baseValue,
                new UInt128() { Low = baseValue.Low, High = baseValue.High },
            };


            foreach (var testValue in testValues)
            {
                Assert.False(
                    baseValue != testValue);
            }
        }

        [Fact]
        public void UInt128_NotEqualOperator_Inequivalent_ReturnsTrue()
        {
            var baseValue = new UInt128() { Low = 1, High = 2 };

            var testValues = new[] {
                new UInt128() { Low = baseValue.Low, High = baseValue.High + 1 },
                new UInt128() { Low = baseValue.Low + 1, High = baseValue.High },
            };


            foreach (var testValue in testValues)
            {
                Assert.True(
                    baseValue != testValue);
            }
        }

        #endregion


        #region operator +

        [Fact]
        public void UInt128_PlusOperator_SimpleLow()
        {
            var baseValue = new UInt128() { Low = 1 };
            var addValue = new UInt128() { Low = 2 };

            var expectedValue = new UInt128() { Low = 3 };


            Assert.Equal(
                expectedValue,
                baseValue + addValue);
        }

        [Fact]
        public void UInt128_PlusOperator_SimpleHigh()
        {
            var baseValue = new UInt128() { High = 1 };
            var addValue = new UInt128() { High = 2 };

            var expectedValue = new UInt128() { High = 3 };


            Assert.Equal(
                expectedValue,
                baseValue + addValue);
        }


        [Fact]
        public void UInt128_PlusOperator_SimpleMixed()
        {
            var baseValue = new UInt128() { High = 1, Low = 2 };
            var addValue = new UInt128() { High = 2, Low = 1 };

            var expectedValue = new UInt128() { High = 3, Low = 3 };


            Assert.Equal(
                expectedValue,
                baseValue + addValue);
        }

        [Fact]
        public void UInt128_PlusOperator_CarryOver()
        {
            var baseValue = new UInt128() { High = 1, Low = UInt64.MaxValue };
            var addValue = new UInt128() { High = 2, Low = 2 };

            var expectedValue = new UInt128() { High = 4, Low = 1 };


            Assert.Equal(
                expectedValue,
                baseValue + addValue);
        }

        [Fact]
        public void UInt128_PlusOperator_Overflow()
        {
            var baseValue = new UInt128() { High = UInt64.MaxValue, Low = 1 };
            var addValue = new UInt128() { High = 2, Low = 2 };

            var expectedValue = new UInt128() { High = 1, Low = 3 };


            Assert.Equal(
                expectedValue,
                baseValue + addValue);
        }

        [Fact]
        public void UInt128_PlusOperator_OverflowAndCarryOver()
        {
            var baseValue = new UInt128() { High = UInt64.MaxValue, Low = 2 };
            var addValue = new UInt128() { High = 2, Low = UInt64.MaxValue };

            var expectedValue = new UInt128() { High = 2, Low = 1 };


            Assert.Equal(
                expectedValue,
                baseValue + addValue);
        }

        #endregion

        #region operator -

        [Fact]
        public void UInt128_MinusOperator_SimpleLow()
        {
            var baseValue = new UInt128() { Low = 3 };
            var subtractValue = new UInt128() { Low = 2 };

            var expectedValue = new UInt128() { Low = 1 };


            Assert.Equal(
                expectedValue,
                baseValue - subtractValue);
        }

        [Fact]
        public void UInt128_MinusOperator_SimpleHigh()
        {
            var baseValue = new UInt128() { High = 3 };
            var subtractValue = new UInt128() { High = 2 };

            var expectedValue = new UInt128() { High = 1 };


            Assert.Equal(
                expectedValue,
                baseValue - subtractValue);
        }


        [Fact]
        public void UInt128_MinusOperator_SimpleMixed()
        {
            var baseValue = new UInt128() { High = 3, Low = 3 };
            var subtractValue = new UInt128() { High = 2, Low = 1 };

            var expectedValue = new UInt128() { High = 1, Low = 2 };


            Assert.Equal(
                expectedValue,
                baseValue - subtractValue);
        }

        [Fact]
        public void UInt128_MinusOperator_BorrowFrom()
        {
            var baseValue = new UInt128() { High = 1, Low = 0};
            var subtractValue = new UInt128() { High = 0, Low = 2 };

            var expectedValue = new UInt128() { High = 0, Low = UInt64.MaxValue - 1 };


            Assert.Equal(
                expectedValue,
                baseValue - subtractValue);
        }

        [Fact]
        public void UInt128_MinusOperator_Underflow()
        {
            var baseValue = new UInt128() { High = 1, Low = 0 };
            var subtractValue = new UInt128() { High = 2, Low = 0 };

            var expectedValue = new UInt128() { High = UInt64.MaxValue, Low = 0 };


            Assert.Equal(
                expectedValue,
                baseValue - subtractValue);
        }

        [Fact]
        public void UInt128_MinusOperator_UnderflowAndBorrowFrom()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };
            var subtractValue = new UInt128() { High = 1, Low = 2 };

            var expectedValue = new UInt128() { High = UInt64.MaxValue - 1, Low = UInt64.MaxValue };


            Assert.Equal(
                expectedValue,
                baseValue - subtractValue);
        }

        #endregion


        #region operator >

        [Fact]
        public void UInt128_GreaterThanOperator_Greater_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };

            var testValues = new[] { 
                new UInt128() { High = 0, Low = 2 },
                new UInt128() { High = 1, Low = 0 },
            };

            
            foreach (var testValue in testValues)
            {
                Assert.True(
                    testValue > baseValue);
            }
        }

        [Fact]
        public void UInt128_GreaterThanOperator_Equal_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };
            var testValue = baseValue;

         
            Assert.False(
                testValue > baseValue);
        }

        [Fact]
        public void UInt128_GreaterThanOperator_Less_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 2, Low = 0 };
            var testValues = new[] {
                new UInt128() { High = 1, Low = 1 },
                new UInt128() { High = 0, Low = 3 }
            };


            foreach (var testValue in testValues)
            {
                Assert.False(
                    testValue > baseValue);
            }
        }

        #endregion

        #region operator >=

        [Fact]
        public void UInt128_GreaterThanOrEqualOperator_Greater_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };

            var testValues = new[] { 
                new UInt128() { High = 0, Low = 2 },
                new UInt128() { High = 1, Low = 0 },
            };


            foreach (var testValue in testValues)
            {
                Assert.True(
                    testValue >= baseValue);
            }
        }

        [Fact]
        public void UInt128_GreaterThanOrEqualOperator_Equal_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };
            var testValue = baseValue;


            Assert.True(
                testValue >= baseValue);
        }

        [Fact]
        public void UInt128_GreaterThanOrEqualOperator_Less_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 2, Low = 0 };
            var testValues = new[] {
                new UInt128() { High = 1, Low = 1 },
                new UInt128() { High = 0, Low = 3 }
            };


            foreach (var testValue in testValues)
            {
                Assert.False(
                    testValue >= baseValue);
            }
        }

        #endregion

        #region operator <

        [Fact]
        public void UInt128_LessThanOperator_Greater_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };

            var testValues = new[] { 
                new UInt128() { High = 0, Low = 2 },
                new UInt128() { High = 1, Low = 0 },
            };


            foreach (var testValue in testValues)
            {
                Assert.False(
                    testValue < baseValue);
            }
        }

        [Fact]
        public void UInt128_LessThanOperator_Equal_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };
            var testValue = baseValue;


            Assert.False(
                testValue < baseValue);
        }

        [Fact]
        public void UInt128_LessThanOperator_Less_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 2, Low = 0 };
            var testValues = new[] {
                new UInt128() { High = 1, Low = 1 },
                new UInt128() { High = 0, Low = 3 }
            };


            foreach (var testValue in testValues)
            {
                Assert.True(
                    testValue < baseValue);
            }
        }

        #endregion

        #region operator <=

        [Fact]
        public void UInt128_LessThanOrEqualOperator_Greater_ReturnsFalse()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };

            var testValues = new[] { 
                new UInt128() { High = 0, Low = 2 },
                new UInt128() { High = 1, Low = 0 },
            };


            foreach (var testValue in testValues)
            {
                Assert.False(
                    testValue <= baseValue);
            }
        }

        [Fact]
        public void UInt128_LessThanOrEqualOperator_Equal_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 0, Low = 1 };
            var testValue = baseValue;


            Assert.True(
                testValue <= baseValue);
        }

        [Fact]
        public void UInt128_LessThanOrEqualOperator_Less_ReturnsTrue()
        {
            var baseValue = new UInt128() { High = 2, Low = 0 };
            var testValues = new[] {
                new UInt128() { High = 1, Low = 1 },
                new UInt128() { High = 0, Low = 3 }
            };


            foreach (var testValue in testValues)
            {
                Assert.True(
                    testValue <= baseValue);
            }
        }

        #endregion

        #endregion

    }
}
