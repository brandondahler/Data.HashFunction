﻿using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    using Farmhash = global::Farmhash.Sharp.Farmhash;

    /// <summary>
    /// Data.HashFunction implementation of <see cref="IFarmHashSharpHash32"/> via FarmHash.Sharp's Hash32 method.
    /// </summary>
    internal class FarmHashSharpHash32_Implementation
        : HashFunctionAsyncBase,
            IFarmHashSharpHash32
    {
        public override int HashSizeInBits { get; } = 32;


        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            var dataArray = data.ToArray(cancellationToken);

            return BitConverter.GetBytes(
                Farmhash.Hash32(dataArray, dataArray.Length));
        }

        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            var dataArray = await data.ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            return BitConverter.GetBytes(
                Farmhash.Hash32(dataArray, dataArray.Length));
        }
    }
}
