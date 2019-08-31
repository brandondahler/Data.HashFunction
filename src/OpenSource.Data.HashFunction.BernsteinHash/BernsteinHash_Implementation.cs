using System.Data.HashFunction.BernsteinHash;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.BernsteinHash
{
    internal class BernsteinHash_Implementation
        : HashFunctionAsyncBase,
            IBernsteinHash
    {
        public override int HashSizeInBits { get; } = 32;
        

        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            UInt32 h = 0;

            data.ForEachRead(
                (dataBytes, position, length) => {
                    ProcessBytes(ref h, dataBytes, position, length);
                },
                cancellationToken);
            
            return BitConverter.GetBytes(h);
        }
        
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            UInt32 h = 0;

            await data.ForEachReadAsync(
                    (dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    },
                    cancellationToken)
                .ConfigureAwait(false);

            return BitConverter.GetBytes(h);
        }


        private static void ProcessBytes(ref UInt32 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (33 * h) + dataBytes[x];
        }
    }
}
