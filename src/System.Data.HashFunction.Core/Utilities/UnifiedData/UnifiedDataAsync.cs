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
    internal abstract class UnifiedDataAsync
        : UnifiedData,
            IUnifiedDataAsync
    {
        /// <inheritdoc cref="UnifiedData.ForEachRead(Action{byte[], int, int}, CancellationToken)" />
        /// <returns>Task representing the asynchronous operation.</returns>
        public abstract Task ForEachReadAsync(Action<byte[], int, int> action, CancellationToken cancellatoinToken);

        /// <inheritdoc cref="UnifiedData.ForEachGroup(int, Action{byte[], int, int}, Action{byte[], int, int}, CancellationToken)" />
        /// <returns>Task representing the asynchronous operation.</returns>
        public abstract Task ForEachGroupAsync(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken);

        /// <inheritdoc cref="UnifiedData.ToArray(CancellationToken)" />
        /// <returns>A task that returns an array of bytes read from the data provider.</returns>
        public abstract Task<byte[]> ToArrayAsync(CancellationToken cancellationToken);
    }
}
