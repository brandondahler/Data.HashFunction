using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Utilities.UnifiedData
{
    internal class ArrayData
        : UnifiedData
    {
        /// <inheritdoc />
        public override long Length { get { return _Data.LongLength; } }

        
        protected readonly byte[] _Data;


        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayData"/> class.
        /// </summary>
        /// <param name="data">The data to represent.</param>
        public ArrayData(byte[] data)
        {
            _Data = data;
            BufferSize = (
                _Data.Length > 0 ?
                _Data.Length :
                1);
        }



        /// <inheritdoc />
        public override void ForEachRead(Action<byte[], int, int> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");


            action(_Data, 0, _Data.Length);
        }



            /// <inheritdoc />
        public override void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "bufferSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");


            var remainderLength = _Data.Length % groupSize;

            if (_Data.Length - remainderLength > 0)
                action(_Data, 0, _Data.Length - remainderLength);

            if (remainderAction != null)
            {
                if (remainderLength > 0)
                    remainderAction(_Data, _Data.Length - remainderLength, remainderLength);
            }
        }




        /// <inheritdoc />
        public override byte[] ToArray()
        {
            return _Data;
        }


    }
}
