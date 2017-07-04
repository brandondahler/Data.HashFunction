using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core.Utilities.UnifiedData
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnifiedDataAsync
        : IUnifiedData
    {
        /// <inheritdoc cref="IUnifiedData.ForEachRead(Action{byte[], int, int}, CancellationToken)" />
        /// <returns>Task representing the asynchronous operation.</returns>
        Task ForEachReadAsync(Action<byte[], int, int> action, CancellationToken cancellationToken);

        /// <inheritdoc cref="IUnifiedData.ForEachGroup(int, Action{byte[], int, int}, Action{byte[], int, int}, CancellationToken)" />
        /// <returns>Task representing the asynchronous operation.</returns>
        Task ForEachGroupAsync(int groupSize, Action<byte[], int, int> action, Action<byte[], int, int> remainderAction, CancellationToken cancellationToken);

        /// <inheritdoc cref="IUnifiedData.ToArray(CancellationToken)" />
        Task<byte[]> ToArrayAsync(CancellationToken cancellationToken);

    }
}