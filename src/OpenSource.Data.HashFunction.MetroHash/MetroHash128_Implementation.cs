using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Implementation of MetroHash128 as specified at https://github.com/jandrewrogers/MetroHash.
    /// 
    /// "
    /// MetroHash is a set of state-of-the-art hash functions for non-cryptographic use cases. 
    /// They are notable for being algorithmically generated in addition to their exceptional performance. 
    /// The set of published hash functions may be expanded in the future, 
    /// having been selected from a very large set of hash functions that have been constructed this way.
    /// "
    /// </summary>
    /// <seealso cref="IMetroHash128" />
    /// <seealso cref="IMetroHash" />
    /// <seealso cref="IHashFunctionAsync" />
    internal class MetroHash128_Implementation
        : HashFunctionAsyncBase,
            IMetroHash128
    {
        private const UInt64 k0 = 0xC83A91E1;
        private const UInt64 k1 = 0x8648DBDB;
        private const UInt64 k2 = 0x7BDEC03B;
        private const UInt64 k3 = 0x2F5870A5;
        


        public IMetroHashConfig Config => _config.Clone();
        public override int HashSizeInBits { get; } = 128;


        private readonly IMetroHashConfig _config;

        public MetroHash128_Implementation(IMetroHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();
        }


        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            var internalState = new InternalState(_config.Seed);

            data.ForEachGroup(
                32,
                internalState.ProcessGroup,
                internalState.ProcessRemainder,
                cancellationToken);

            return internalState.GetResult();
        }

        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {

            var internalState = new InternalState(_config.Seed);

            await data.ForEachGroupAsync(
                    32,
                    internalState.ProcessGroup,
                    internalState.ProcessRemainder,
                    cancellationToken)
                .ConfigureAwait(false);

            return internalState.GetResult();
        }


        #region InternalState

        private class InternalState
        {
            private UInt64[] _workingValues;

            private bool _anyFullGroupProcessed = false;
            private bool _remainderProcessed = false;
            private bool _postFullGroupExecuted = false;

            private bool _resultFetched = false;

            public InternalState(UInt64 seed)
            {
                _workingValues = new[] {
                    (seed - k0) * k3,
                    (seed + k1) * k2,
                    (seed + k0) * k2,
                    (seed - k1) * k3
                };
            }

            public void ProcessGroup(byte[] array, int start, int count)
            {
                Debug.Assert(count % 32 == 0, "Expected count to be multiple of 32.");

                EnsureRemainderNotProcessed();
                EnsureResultsNotFetched();


                _anyFullGroupProcessed = true;


                for (var x = 0; x < count; x += 32)
                {
                    _workingValues[0] += BitConverter.ToUInt64(array, start + x) * k0;
                    _workingValues[0] = RotateRight(_workingValues[0], 29) + _workingValues[2];

                    _workingValues[1] += BitConverter.ToUInt64(array, start + x + 8) * k1;
                    _workingValues[1] = RotateRight(_workingValues[1], 29) + _workingValues[3];

                    _workingValues[2] += BitConverter.ToUInt64(array, start + x + 16) * k2;
                    _workingValues[2] = RotateRight(_workingValues[2], 29) + _workingValues[0];

                    _workingValues[3] += BitConverter.ToUInt64(array, start + x + 24) * k3;
                    _workingValues[3] = RotateRight(_workingValues[3], 29) + _workingValues[1];
                }
            }

            private void ExecutePostFullGroup()
            {
                Debug.Assert(!_postFullGroupExecuted, "Post full group already executed.");


                _postFullGroupExecuted = true;


                if (_anyFullGroupProcessed)
                {
                    _workingValues[2] ^= RotateRight(((_workingValues[0] + _workingValues[3]) * k0) + _workingValues[1], 21) * k1;
                    _workingValues[3] ^= RotateRight(((_workingValues[1] + _workingValues[2]) * k1) + _workingValues[0], 21) * k0;
                    _workingValues[0] ^= RotateRight(((_workingValues[0] + _workingValues[2]) * k0) + _workingValues[3], 21) * k1;
                    _workingValues[1] ^= RotateRight(((_workingValues[1] + _workingValues[3]) * k1) + _workingValues[2], 21) * k0;
                }
            }

            public void ProcessRemainder(byte[] array, int start, int count)
            {
                Debug.Assert(count < 32, "Expected count to be less than 32.");

                EnsureRemainderNotProcessed();
                EnsureResultsNotFetched();


                _remainderProcessed = true;

                ExecutePostFullGroup();


                if (count >= 16)
                {
                    _workingValues[0] += BitConverter.ToUInt64(array, start) * k2;
                    _workingValues[0] = RotateRight(_workingValues[0], 33) * k3;

                    _workingValues[1] += BitConverter.ToUInt64(array, start + 8) * k2;
                    _workingValues[1] = RotateRight(_workingValues[1], 33) * k3;

                    _workingValues[0] ^= RotateRight((_workingValues[0] * k2) + _workingValues[1], 45) * k1;
                    _workingValues[1] ^= RotateRight((_workingValues[1] * k3) + _workingValues[0], 45) * k0;

                    start += 16;
                    count -= 16;
                }

                if (count >= 8)
                {
                    _workingValues[0] += BitConverter.ToUInt64(array, start) * k2;
                    _workingValues[0] = RotateRight(_workingValues[0], 33) * k3;
                    _workingValues[0] ^= RotateRight((_workingValues[0] * k2) + _workingValues[1], 27) * k1;

                    start += 8;
                    count -= 8;
                }

                if (count >= 4)
                {
                    _workingValues[1] += BitConverter.ToUInt32(array, start) * k2;
                    _workingValues[1] = RotateRight(_workingValues[1], 33) * k3;
                    _workingValues[1] ^= RotateRight((_workingValues[1] * k3) + _workingValues[0], 46) * k0;

                    start += 4;
                    count -= 4;
                }

                if (count >= 2)
                {
                    _workingValues[0] += BitConverter.ToUInt16(array, start) * k2;
                    _workingValues[0] = RotateRight(_workingValues[0], 33) * k3;
                    _workingValues[0] ^= RotateRight((_workingValues[0] * k2) + _workingValues[1], 22) * k1;

                    start += 2;
                    count -= 2;
                }

                if (count >= 1)
                {
                    _workingValues[1] += array[start] * k2;
                    _workingValues[1] = RotateRight(_workingValues[1], 33) * k3;
                    _workingValues[1] ^= RotateRight((_workingValues[1] * k3) + _workingValues[0], 58) * k0;
                }
            }

            public byte[] GetResult()
            {
                EnsureResultsNotFetched();

                _resultFetched = true;


                if (!_postFullGroupExecuted)
                    ExecutePostFullGroup();



                _workingValues[0] += RotateRight((_workingValues[0] * k0) + _workingValues[1], 13);
                _workingValues[1] += RotateRight((_workingValues[1] * k1) + _workingValues[0], 37);
                _workingValues[0] += RotateRight((_workingValues[0] * k2) + _workingValues[1], 13);
                _workingValues[1] += RotateRight((_workingValues[1] * k3) + _workingValues[0], 37);


                var result = new byte[16];

                Array.Copy(BitConverter.GetBytes(_workingValues[0]), 0, result, 0, 8);
                Array.Copy(BitConverter.GetBytes(_workingValues[1]), 0, result, 8, 8);

                return result;
            }


            private void EnsureRemainderNotProcessed() => Debug.Assert(!_remainderProcessed, "Remainder already processed.");
            private void EnsureResultsNotFetched() => Debug.Assert(!_resultFetched, "Results already fetched.");
        }

        #endregion

        #region RotateRight

        private static UInt64 RotateRight(UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand >> shiftCount) |
                (operand << (64 - shiftCount));
        }

        #endregion

    }
}
