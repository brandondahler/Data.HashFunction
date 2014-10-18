using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities.UnifiedData
{
    /// <summary>
    /// Centralized methodology for accessing data used by Data.HashFunction.
    /// </summary>
    public abstract class UnifiedData
    {
        /// <summary>
        /// Length of data provided.
        /// </summary>
        /// <remarks>
        /// Implementors are allowed throw an exception if it is not possible to resolve the length of the data.
        /// </remarks>
        public abstract long Length { get; }



        /// <inheritdoc cref="ForEachRead(Action{byte[]}, int)" />
        public void ForEachRead(Action<byte[]> action)
        {
            ForEachRead(action, 4096);
        }
        
        /// <summary>
        /// Executes an action each time a chunk is read.
        /// </summary>
        /// <param name="action">Function to execute.</param>
        /// <param name="bufferSize">Suggested size of buffer reads.</param>
        /// <remarks>bufferSize is only a suggestion, there are no guarantees for the length of the array passed to the action.</remarks>
        public abstract void ForEachRead(Action<byte[]> action, int bufferSize);



        /// <inheritdoc cref="ForEachReadAsync(Action{byte[]}, int)" />
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task ForEachReadAsync(Action<byte[]> action)
        {
            return ForEachReadAsync(action, 4096);
        }

        /// <inheritdoc cref="ForEachRead(Action{byte[]}, int)" />
        /// <returns>Task representing the asynchronous operation.</returns>
        public abstract Task ForEachReadAsync(Action<byte[]> action, int bufferSize);



        /// <summary>
        /// Executes an action for each group of data that is read.
        /// </summary>
        /// <param name="groupSize">Length of the data passed to the action.</param>
        /// <param name="action">Action to execute for each full group read.</param>
        /// <param name="remainderAction">Action to execute if the final group is less than groupSize.  Null values are allowed.</param>
        /// <remarks>remainderAction will not be run if the length of the data is a multiple of groupSize.</remarks>
        public abstract void ForEachGroup(int groupSize, Action<byte[]> action, Action<byte[]> remainderAction);
        
        /// <inheritdoc cref="ForEachGroup(int, Action{byte[]}, Action{byte[]})" />
        /// <returns>Task representing the asynchronous operation.</returns>
        public abstract Task ForEachGroupAsync(int groupSize, Action<byte[]> action, Action<byte[]> remainderAction);



        /// <summary>
        /// Reads all data and converts it to an in-memory array.
        /// </summary>
        /// <returns>Array of bytes read from the data provider.</returns>
        public abstract byte[] ToArray();
        
        /// <inheritdoc cref="ToArray()" />
        public abstract Task<byte[]> ToArrayAsync();
    }
}
