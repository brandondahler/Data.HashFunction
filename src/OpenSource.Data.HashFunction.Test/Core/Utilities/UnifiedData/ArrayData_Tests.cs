using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Test._Mocks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Core.Utilities.UnifiedData
{
    using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
    using System.Threading;

    public class ArrayData_Tests
    {
        #region Constructor

        [Fact]
        public void ArrayData_Constructor_Data_IsNull_Throws()
        {
            Assert.Equal(
                "data",
                Assert.Throws<ArgumentNullException>(
                        () => new ArrayData(null))
                    .ParamName);
        }

        #endregion

        public class UnifiedDataTests_ArrayData
            : UnifiedDataBase_Tests
        {
            protected override IUnifiedData CreateTestData(int length)
            {
                var r = new Random();

                var data = new byte[length];
                r.NextBytes(data);

                return new ArrayData(data);
            }
        }

    }

}
