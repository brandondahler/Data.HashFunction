using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core.Utilities.UnifiedData
{
    internal sealed class ArrayData
        : UnifiedDataBase
    {
        /// <summary>
        /// Length of data provided.
        /// </summary>
        /// <remarks>
        /// Implementors are allowed throw an exception if it is not possible to resolve the length of the data.
        /// </remarks>
        public override long Length { get => _data.Length; }

        
        private readonly byte[] _data;


        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayData"/> class.
        /// </summary>
        /// <param name="data">The data to represent.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/></exception>
        public ArrayData(byte[] data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }



        /// <summary>
        /// Executes an action each time a chunk is read.
        /// </summary>
        /// <param name="action">Function to execute.</param>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        public override void ForEachRead(Action<byte[], int, int> action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            
            cancellationToken.ThrowIfCancellationRequested();


            action(_data, 0, _data.Length);
        }


        /// <summary>
        /// Executes an action one or more times, providing the data read as an array whose length is a multiple of groupSize.  
        /// Optionally runs an action on the final remainder group.
        /// </summary>
        /// <param name="groupSize">Length of the groups passed to the action.</param>
        /// <param name="action">Action to execute for each full group read.</param>
        /// <param name="remainderAction">Action to execute if the final group is less than groupSize.  Null values are allowed.</param>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <remarks>remainderAction will not be run if the length of the data is a multiple of groupSize.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">groupSize;groupSize must be greater than 0.</exception>
        /// <exception cref="ArgumentNullException">action</exception>
        public override void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken)
        {
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(groupSize), $"{nameof(groupSize)} must be greater than 0.");

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

        /// <summary>
        /// Reads all data and converts it to an in-memory array.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while reading the underlying data.</param>
        /// <returns>Array of bytes read from the data provider.</returns>
        public override byte[] ToArray(CancellationToken cancellationToken) =>  _data;
    }
}
