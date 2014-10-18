using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }



        /// <inheritdoc />
        public override void ForEachRead(Action<byte[]> action, int bufferSize)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize", "bufferSize must be greater than 0.");


            action(_Data);
        }

        /// <inheritdoc />
        public override Task ForEachReadAsync(Action<byte[]> action, int bufferSize = 4096)
        {
            try
            {
                ForEachRead(action, bufferSize);

                return Task.FromResult(true);

            } catch (Exception ex) {
                throw new AggregateException(new[] { ex });
            }

        }



        /// <inheritdoc />
        public override void ForEachGroup(int groupSize, Action<byte[]> action, Action<byte[]> remainderAction)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException("groupSize", "bufferSize must be greater than 0.");

            if (action == null)
                throw new ArgumentNullException("action");


            for (var x = 0; x <= (_Data.Length - groupSize); x += groupSize)
            {
                var dataBytes = new byte[groupSize];
                Array.Copy(_Data, x, dataBytes, 0, groupSize);

                action(dataBytes);
            }

            if (remainderAction != null)
            {
                var remainingBytes = _Data.Length % groupSize;

                if (remainingBytes > 0)
                {
                    var dataBytes = new byte[remainingBytes];
                    Array.Copy(_Data, _Data.Length - remainingBytes, dataBytes, 0, remainingBytes);

                    remainderAction(dataBytes);
                }
            }
        }

        /// <inheritdoc />
        public override Task ForEachGroupAsync(int groupSize, Action<byte[]> action, Action<byte[]> remainderAction)
        {
            try
            {
                ForEachGroup(groupSize, action, remainderAction);

                return Task.FromResult(true);   

            } catch (Exception ex) {
                throw new AggregateException(new[] { ex });
            }
        }



        /// <inheritdoc />
        public override byte[] ToArray()
        {
            return _Data;
        }

        /// <inheritdoc />
        public override Task<byte[]> ToArrayAsync()
        {
            return Task.FromResult(
                ToArray());
        }

        
    }
}
