using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core.Utilities.UnifiedData
{
    internal sealed class ArrayData
        : UnifiedData
    {
        /// <inheritdoc />
        public override long Length { get => _data.Length; }

        
        private readonly byte[] _data;


        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayData"/> class.
        /// </summary>
        /// <param name="data">The data to represent.</param>
        public ArrayData(byte[] data)
        {
            _data = data;
        }



        /// <inheritdoc />
        public override void ForEachRead(Action<byte[], int, int> action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            
            cancellationToken.ThrowIfCancellationRequested();


            action(_data, 0, _data.Length);
        }


        /// <inheritdoc />
        public override void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(groupSize), "bufferSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException(nameof(action));


            cancellationToken.ThrowIfCancellationRequested();


            var remainderLength = _data.Length % groupSize;

            if (_data.Length - remainderLength > 0)
                action(_data, 0, _data.Length - remainderLength);

            
            if (remainderAction != null && remainderLength > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                remainderAction(_data, _data.Length - remainderLength, remainderLength);
            }
        }

        /// <inheritdoc />
        public override byte[] ToArray(CancellationToken cancellationToken)
        {
            return _data;
        }
    }
}
