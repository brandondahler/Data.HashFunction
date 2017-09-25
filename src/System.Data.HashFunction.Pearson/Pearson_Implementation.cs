using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Pearson
{
    /// <summary>
    /// Abstract implementation of Pearson hashing as specified at http://en.wikipedia.org/wiki/Pearson_hashing and
    ///   http://cs.mwsu.edu/~griffin/courses/2133/downloads/Spring11/p677-pearson.pdf.
    /// </summary>
    internal class Pearson_Implementation
        : HashFunctionAsyncBase,
            IPearson
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public IPearsonConfig Config => _config.Clone();


        private readonly IPearsonConfig _config;



        /// <summary>
        /// Initializes a new instance of the <see cref="Pearson_Implementation"/> class.
        /// </summary>
        /// <param name="t"><inheritdoc cref="T" /></param>
        /// <exception cref="System.ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="System.ArgumentException"><paramref name="config"/>.<see cref="IPearsonConfig.Table"/> must be non-null.<see cref="IPearsonConfig.Table"/></exception>
        /// <exception cref="System.ArgumentException"><paramref name="config"/>.<see cref="IPearsonConfig.Table"/> must be a permutation of [<c>0</c>, <c>255</c>].;<paramref name="config"/>.<see cref="IPearsonConfig.Table"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IPearsonConfig.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IPearsonConfig.HashSizeInBits"/> must be a positive multiple of <c>8</c>.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public Pearson_Implementation(IPearsonConfig config)
            : base((config?.HashSizeInBits).GetValueOrDefault())
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (_config.Table == null)
                throw new ArgumentException($"{nameof(config)}.{nameof(config.Table)} must be non-null.", $"{nameof(config)}.{nameof(config.Table)}");

            if (_config.Table.Count != 256 || _config.Table.Distinct().Count() != 256)
                throw new ArgumentException($"{nameof(config)}.{nameof(config.Table)} must be a permutation of [0, 255].", $"{nameof(config)}.{nameof(config.Table)}");


            if (_config.HashSizeInBits <= 0 || _config.HashSizeInBits % 8 != 0)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be a positive integer that is divisible by 8.");
        }



        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            var h = new byte[HashSizeInBits / 8];
            bool firstByte = true;

            data.ForEachRead(
                (dataBytes, position, length) => {
                    ProcessBytes(ref h, ref firstByte, dataBytes, position, length);
                },
                cancellationToken);

            return h;
        }
        
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            var h = new byte[HashSizeInBits / 8];
            bool firstByte = true;

            await data.ForEachReadAsync(
                    (dataBytes, position, length) => {
                        ProcessBytes(ref h, ref firstByte, dataBytes, position, length);
                    },
                    cancellationToken)
                .ConfigureAwait(false);

            return h;
        }


        private void ProcessBytes(ref byte[] h, ref bool firstByte, byte[] dataBytes, int position, int length)
        {
            var table = _config.Table;

            for (var x = position; x < position + length; ++x)
            {
                for (int y = 0; y < HashSizeInBits / 8; ++y)
                {
                    if (!firstByte)
                        h[y] = table[h[y] ^ dataBytes[x]];
                    else
                        h[y] = table[(dataBytes[x] + y) & 0xff];
                }

                firstByte = false;
            }
        }
    }
}
