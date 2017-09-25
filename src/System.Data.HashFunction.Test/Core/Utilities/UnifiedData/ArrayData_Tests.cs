using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test._Mocks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core.Utilities.UnifiedData
{
    using System.Data.HashFunction.Core.Utilities.UnifiedData;
    using System.Threading;

    public class ArrayData_Tests
    {
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
