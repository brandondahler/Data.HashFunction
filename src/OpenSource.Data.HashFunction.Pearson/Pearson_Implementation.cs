﻿using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Pearson
{
    /// <summary>
    /// Implementation of Pearson hashing as specified at http://en.wikipedia.org/wiki/Pearson_hashing and
    ///   http://cs.mwsu.edu/~griffin/courses/2133/downloads/Spring11/p677-pearson.pdf.
    /// </summary>
    internal class Pearson_Implementation
        : StreamableHashFunctionBase,
            IPearson
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public IPearsonConfig Config => _config.Clone();
        

        public override int HashSizeInBits => _config.HashSizeInBits;



        private readonly IPearsonConfig _config;



        /// <summary>
        /// Initializes a new instance of the <see cref="Pearson_Implementation"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="System.ArgumentException"><paramref name="config"/>.<see cref="IPearsonConfig.Table"/> must be non-null.;<paramref name="config"/>.<see cref="IPearsonConfig.Table"/></exception>
        /// <exception cref="System.ArgumentException"><paramref name="config"/>.<see cref="IPearsonConfig.Table"/> must be a permutation of [<c>0</c>, <c>255</c>].;<paramref name="config"/>.<see cref="IPearsonConfig.Table"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IPearsonConfig.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IPearsonConfig.HashSizeInBits"/> must be a positive multiple of <c>8</c>.</exception>
        public Pearson_Implementation(IPearsonConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (_config.Table == null)
                throw new ArgumentException($"{nameof(config)}.{nameof(config.Table)} must be non-null.", $"{nameof(config)}.{nameof(config.Table)}");

            if (_config.Table.Count != 256 || _config.Table.Distinct().Count() != 256)
                throw new ArgumentException($"{nameof(config)}.{nameof(config.Table)} must be a permutation of [0, 255].", $"{nameof(config)}.{nameof(config.Table)}");


            if (_config.HashSizeInBits <= 0 || _config.HashSizeInBits % 8 != 0)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be a positive integer that is divisible by 8.");
        }



        public override IHashFunctionBlockTransformer CreateBlockTransformer()
        {
            return new BlockTransformer(_config);
        }

        private class BlockTransformer
            : HashFunctionBlockTransformerBase<BlockTransformer>
        {
            private IReadOnlyList<byte> _table;

            private bool _anyBytesProcessed;
            private byte[] _hashValue;

            public BlockTransformer()
            {

            }

            public BlockTransformer(IPearsonConfig config)
                : this()
            {
                _table = config.Table;

                _anyBytesProcessed = false;
                _hashValue = new byte[config.HashSizeInBits / 8];
            }

            protected override void CopyStateTo(BlockTransformer other)
            {
                base.CopyStateTo(other);

                other._table = _table;

                other._anyBytesProcessed = false;

                // _hashValue
                {
                    other._hashValue = new byte[_hashValue.Length];

                    Array.Copy(_hashValue, other._hashValue, _hashValue.Length);
                }
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempAnyBytesProcessed = _anyBytesProcessed;

                var tempTable = _table;
                var tempHashValueLength = tempHashValue.Length;


                for (var currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                {
                    for (int y = 0; y < tempHashValueLength; ++y)
                    {
                        if (tempAnyBytesProcessed)
                            tempHashValue[y] = tempTable[tempHashValue[y] ^ dataArray[currentOffset]];
                        else
                            tempHashValue[y] = tempTable[(dataArray[currentOffset] + y) & 0xff];
                    }

                    tempAnyBytesProcessed = true;
                }

                _anyBytesProcessed = tempAnyBytesProcessed;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                return new HashValue(_hashValue, _hashValue.Length * 8);
            }
        }
    }
}
