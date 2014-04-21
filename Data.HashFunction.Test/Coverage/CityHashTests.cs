using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Coverage
{
    public class CityHashTests
    {
        protected class Test_CityHash
            : CityHash
        {
            public UInt32 CityRotate(UInt32 val, int shift)
            {
                return Rotate(val, shift);
            }

            public UInt64 CityRotate(UInt64 val, int shift)
            {
                return Rotate(val, shift);
            }
        }

        [Fact]
        public void CityHash_Rotate_UInt32_ZeroShift_Correct()
        {
            var ch = new Test_CityHash();

            Assert.Equal((UInt32) 0x5002af3f, ch.CityRotate((UInt32) 0x5002af3f, 0));
        }

        [Fact]
        public void CityHash_Rotate_UInt64_ZeroShift_Correct()
        {
            var ch = new Test_CityHash();

            Assert.Equal((UInt64) 0x2f7393355002af3f, ch.CityRotate((UInt64) 0x2f7393355002af3f, 0));
        }
    }
}
