using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Coverage
{
    public class FNVTests
    {
        [Fact]
        public void FNVBase_Constructor_InvalidDefaultHashSize_Throws()
        {
            var fnvMock = new Mock<FNV1>() { CallBase = true };
            fnvMock.SetupGet(f => f.ValidHashSizes)
                .Returns(new[] { -1 });
            
            // Not possible to cover 100%
            var moqException = Assert.Throws<TargetInvocationException>(() => fnvMock.Object);
                        
            Assert.IsType<InvalidOperationException>(moqException.InnerException);
        }
    }
}
