using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.FNV
{
    public class FNVBaseTests
    {
        [Fact]
        public void FNVBase_Constructor_InvalidPrimeSize_Throws()
        {
            var fnvPrimeOffsetMock = new Mock<FNVPrimeOffset>(224, new BigInteger(0), new BigInteger(0)) {
                CallBase = true
            };

            fnvPrimeOffsetMock.SetupGet(fpo => fpo.Prime)
                .Returns(new UInt32[] { 0 });


            FNV1.HashParameters[224] = fnvPrimeOffsetMock.Object;

            Assert.Equal("HashParameters[224].Prime",
                Assert.Throws<ArgumentException>(() =>
                    new FNV1(224))
                .ParamName);

            (FNV1.HashParameters as IDictionary<int, FNVPrimeOffset>).Remove(224);
        }

        [Fact]
        public void FNVBase_Constructor_InvalidOffsetSize_Throws()
        {
            var fnvPrimeOffsetMock = new Mock<FNVPrimeOffset>(224, new BigInteger(0), new BigInteger(0))
            { 
                CallBase = true 
            };

            fnvPrimeOffsetMock.SetupGet(fpo => fpo.Offset)
                .Returns(new UInt32[] { 0 });


            FNV1.HashParameters[224] = fnvPrimeOffsetMock.Object;

            Assert.Equal("HashParameters[224].Offset",
                Assert.Throws<ArgumentException>(() => 
                    new FNV1(224))
                .ParamName);

            FNV1.HashParameters.Remove(224);
        }
    }
}
