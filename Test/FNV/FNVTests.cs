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
        public void FNVBase_Constructor_InvalidDefaultHashSize_Throws()
        {
            var fnvMock = new Mock<FNV1>() { CallBase = true };
            fnvMock.SetupGet(f => f.ValidHashSizes)
                .Returns(new[] { -1 });
            

            var moqException = Assert.Throws<TargetInvocationException>(() => fnvMock.Object);
                        
            Assert.IsType<InvalidOperationException>(moqException.InnerException);
        }


        [Fact]
        public void FNVBase_ComputeHash_InvalidPrimeSize_Throws()
        {
            var fnvPrimeOffsetMock = new Mock<FNVPrimeOffset>(64, new BigInteger(0), new BigInteger(0)) {
                CallBase = true
            };

            fnvPrimeOffsetMock.SetupGet(fpo => fpo.Prime)
                .Returns(new UInt32[] { 0 });

            var fnvMock = new Mock<FNV1> { CallBase = true };
            fnvMock.Protected()
                .SetupGet<IDictionary<int, FNVPrimeOffset>>("HashParameters")
                .Returns(new Dictionary<int, FNVPrimeOffset>() {
                    {64, fnvPrimeOffsetMock.Object}
                });

            var moqException = Assert.Throws<TargetInvocationException>(() => fnvMock.Object);

           
            Assert.Equal("HashParameters[64].Prime",
                Assert.IsType<ArgumentException>(moqException.InnerException)
                    .ParamName);
        }

        [Fact]
        public void FNVBase_ComputeHash_InvalidOffsetSize_Throws()
        {
            var fnvPrimeOffsetMock = new Mock<FNVPrimeOffset>(64, new BigInteger(0), new BigInteger(0)) { 
                CallBase = true 
            };

            fnvPrimeOffsetMock.SetupGet(fpo => fpo.Offset)
                .Returns(new UInt32[] { 0 });

            var fnvMock = new Mock<FNV1> { CallBase = true };
            fnvMock.Protected()
                .SetupGet<IDictionary<int, FNVPrimeOffset>>("HashParameters")
                .Returns(new Dictionary<int, FNVPrimeOffset>() {
                    {64, fnvPrimeOffsetMock.Object}
                });


            var moqException = Assert.Throws<TargetInvocationException>(() => fnvMock.Object);

            Assert.Equal("HashParameters[64].Offset",
                Assert.IsType<ArgumentException>(moqException.InnerException)
                    .ParamName);
        }
    }
}
