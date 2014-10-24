using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Test.Mocks
{
    internal class ArrayData_ThrowNonAsync
        : ArrayData
    {
        public static readonly InvalidOperationException ExceptionToThrow = new InvalidOperationException("Mock Exception");


        public ArrayData_ThrowNonAsync(byte[] data)
            : base(data)
        {

        }

        public override void ForEachRead(Action<byte[], int, int> action)
        {
            throw ExceptionToThrow;
        }

        public override void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction)
        {
            throw ExceptionToThrow;
        }

        public override byte[] ToArray()
        {
            throw ExceptionToThrow;
        }
    }
}
