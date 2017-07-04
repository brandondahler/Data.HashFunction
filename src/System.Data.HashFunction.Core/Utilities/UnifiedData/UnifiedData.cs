using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core.Utilities.UnifiedData
{
    /// <summary>
    /// Centralized methodology for accessing data used by Data.HashFunction.
    /// </summary>
    internal abstract class UnifiedData
        : IUnifiedData
    {
        /// <inheritdoc />
        public abstract long Length { get; }

        /// <inheritdoc />
        public virtual int BufferSize 
        {
            get { return _BufferSize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", "value must be greater than 0");

                _BufferSize = value;
            }
        }


        private int _BufferSize = 4096;



        /// <inheritdoc />
        public abstract void ForEachRead(Action<byte[], int, int> action, CancellationToken cancellationToken);

        /// <inheritdoc />
        public abstract void ForEachGroup(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken);


        /// <inheritdoc />
        public abstract byte[] ToArray(CancellationToken cancellationToken);
    }
}
