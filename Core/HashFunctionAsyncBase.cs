using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Abstract implementation of an IHashFunction.
    /// Provides convenience checks and ensures a default HashSize has been set at construction.
    /// </summary>
    public abstract class HashFunctionAsyncBase 
        : HashFunctionBase, IHashFunctionAsync
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
        public async Task<byte[]> ComputeHashAsync(Stream data)
        {
            if (!data.CanRead)
                throw new ArgumentException("Stream \"data\" must be readable.", "data");


            if (!data.CanSeek && RequiresSeekableStream)
            {
                byte[] buffer;

                using (var ms = new MemoryStream())
                {
                    await data.CopyToAsync(ms)
                        .ConfigureAwait(false);

                    buffer = ms.ToArray();
                }

                // Use non-async because all of the data is in-memory
                return ComputeHashInternal(
                    new ArrayData(buffer));
            }


            return await ComputeHashAsyncInternal(new StreamData(data))
                .ConfigureAwait(false);
        }


        /// <summary>
        /// Computes hash value for given stream asynchronously.
        /// </summary>
        /// <param name="data">Data to hash.</param>
        /// <returns>
        /// Hash value of data as byte array.
        /// </returns>
        protected abstract Task<byte[]> ComputeHashAsyncInternal(UnifiedData data);
    }
}
