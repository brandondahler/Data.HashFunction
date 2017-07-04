using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core
{
    /// <summary>
    /// Abstract implementation of an IHashFunction.
    /// Provides convenience checks and ensures a default HashSize has been set at construction.
    /// </summary>
    public abstract class HashFunctionAsyncBase 
        : HashFunctionBase, 
            IHashFunctionAsync
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HashFunctionAsyncBase"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase.HashSize" /></param>
        protected HashFunctionAsyncBase(int hashSize)
            : base(hashSize)
        {
            
        }


        /// <exception cref="System.ArgumentException">Stream \data\ must be readable.;data</exception>
        /// <inheritdoc />
        public Task<IHashValue> ComputeHashAsync(Stream data) => ComputeHashAsync(data, CancellationToken.None);

        /// <exception cref="System.ArgumentException">Stream \data\ must be readable.;data</exception>
        /// <inheritdoc />
        public async Task<IHashValue> ComputeHashAsync(Stream data, CancellationToken cancellationToken)
        {
            if (!data.CanRead)
                throw new ArgumentException("Stream \"data\" must be readable.", "data");


            if (!data.CanSeek && RequiresSeekableStream)
                throw new ArgumentException("Seekable stream \"data\" required for this type of hash function.", "data");

            cancellationToken.ThrowIfCancellationRequested();


            return new HashValue(
                await ComputeHashAsyncInternal(new StreamData(data), cancellationToken)
                    .ConfigureAwait(false),
                HashSize);
        }

        /// <summary>
        /// Computes hash value for given stream asynchronously.
        /// </summary>
        /// <param name="data">Data to hash.</param>
        /// <param name="cancellationToken">A cancellation token to observe while calculating the hash value.</param>
        /// <returns>
        /// Hash value of data as byte array.
        /// </returns>
        protected abstract Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken);

    }
}
