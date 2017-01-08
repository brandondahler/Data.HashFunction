using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Pearson
{
    public class PearsonBaseTests
    {
        [Fact]
        public void PearsonBase_Constructor_T_Null_Throws()
        {
            Assert.Equal("t",
                Assert.Throws<ArgumentNullException>(() => 
                    GC.KeepAlive(new PearsonBase_T_Null()))
                .ParamName);
        }

        [Fact]
        public void PearsonBase_Constructor_T_Small_Throws()
        {
            Assert.Equal("t",
                Assert.Throws<ArgumentException>(() =>
                    GC.KeepAlive(new PearsonBase_T_Small()))
                .ParamName);
        }

        [Fact]
        public void PearsonBase_Constructor_T_NonDistinct_Throws()
        {
            Assert.Equal("t",
                Assert.Throws<ArgumentException>(() =>
                    GC.KeepAlive(new PearsonBase_T_NonDistinct()))
                .ParamName);
        }




        private class PearsonBase_T_Null
            : PearsonBase
        {
            public PearsonBase_T_Null()
                : base(null)
            {

            }
        }

        private class PearsonBase_T_Small
            : PearsonBase
        {
            public PearsonBase_T_Small()
                : base(new byte[255])
            {

            }
        }

        private class PearsonBase_T_NonDistinct
            : PearsonBase
        {
#if !NET40
            private static readonly IReadOnlyList<byte> NonDistinct_T =
#else
            private static readonly IList<byte> NonDistinct_T =
#endif
                Enumerable.Range(0, 255)
                    .Select(i => (byte) i)
                    .Concat(new byte[] { 0 })
                    .ToList();

            public PearsonBase_T_NonDistinct()
                : base(NonDistinct_T)
            {

            }
        }
    }
}
