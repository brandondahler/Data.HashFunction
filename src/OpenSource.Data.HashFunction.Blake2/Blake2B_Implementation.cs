using System;
using OpenSource.Data.HashFunction.Blake2.Utilities;
using OpenSource.Data.HashFunction.Core;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.Blake2
{
	internal partial class Blake2B_Implementation
		: StreamableHashFunctionBase,
            IBlake2B
    {
        public IBlake2BConfig Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;



        private readonly IBlake2BConfig _config;

        private readonly uint _originalKeyLength;
        private readonly byte[] _key;
        private readonly byte[] _salt;
        private readonly byte[] _personalization;


		private const int MinHashSizeBits = 8;
		private const int MaxHashSizeBits = 512;
		private const int DefaultHashSizeBits = 512;

		private const int MaxKeySizeBytes = 64;
		private const int SaltSizeBytes = 16;
		private const int PersonalizationSizeBytes = 16;

		private const int BlockSizeBytes = 128;

        private const UInt64 _iv1 = 0x6A09E667F3BCC908UL;
		private const UInt64 _iv2 = 0xBB67AE8584CAA73BUL;
        private const UInt64 _iv3 = 0x3C6EF372FE94F82BUL;
        private const UInt64 _iv4 = 0xA54FF53A5F1D36F1UL;
        private const UInt64 _iv5 = 0x510E527FADE682D1UL;
        private const UInt64 _iv6 = 0x9B05688C2B3E6C1FUL;
        private const UInt64 _iv7 = 0x1F83D9ABFB41BD6BUL;
        private const UInt64 _iv8 = 0x5BE0CD19137E2179UL;




        private class InternalState
        {
            public int BufferFilled = 0;
            public readonly byte[] Buffer = new byte[128];

            public readonly UInt64[] H = new UInt64[8];
            public UInt128 Counter = new UInt128();
            public readonly UInt64[] FinalizationFlags = new UInt64[2];


        }



        /// <summary>
        /// Initializes an instance of the <see cref="Blake2B_Implementation"/> class with the provided
        /// <paramref name="config"/>.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IBlake2BConfig.HashSizeInBits"/>;Expected: <see cref="MinHashSizeBits"/> >= <paramref name="config"/>.<see cref="IBlake2BConfig.HashSizeInBits"/> &lt;= <see cref="MaxHashSizeBits"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IBlake2BConfig.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IBlake2BConfig.HashSizeInBits"/> must be a multiple of 8.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IBlake2BConfig.Key"/>;Expected: <paramref name="config"/>.<see cref="IBlake2BConfig.Key"/>.Count &lt;= <see cref="MaxKeySizeBytes"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IBlake2BConfig.Salt"/>;Expected: <paramref name="config"/>.<see cref="IBlake2BConfig.Salt"/>.Count == <see cref="SaltSizeBytes"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IBlake2BConfig.Personalization"/>;Expected: <paramref name="config"/>.<see cref="IBlake2BConfig.Personalization"/>.Count == <see cref="PersonalizationSizeBytes"/></exception>
        public Blake2B_Implementation(IBlake2BConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();



            if (_config.HashSizeInBits < MinHashSizeBits || _config.HashSizeInBits > MaxHashSizeBits)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"Expected: {MinHashSizeBits} >= {nameof(config)}.{nameof(config.HashSizeInBits)} <= {MaxHashSizeBits}");

            if (_config.HashSizeInBits % 8 != 0)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{ nameof(config) }.{ nameof(config.HashSizeInBits)} must be a multiple of 8.");



            var keyArray = (_config.Key ?? new byte[0]).ToArray();
            var saltArray = (_config.Salt ?? new byte[16]).ToArray();
            var personalizationArray = (_config.Personalization ?? new byte[16]).ToArray();

			if (keyArray.Length > MaxKeySizeBytes)
				throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Key)}", _config.Key, $"Expected: {nameof(config)}.{nameof(config.Key)}.Count <= {MaxKeySizeBytes}");

			if (saltArray.Length != SaltSizeBytes)
				throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Salt)}", _config.Salt, $"Expected: {nameof(config)}.{nameof(config.Salt)}.Count == {SaltSizeBytes}");

			if (personalizationArray.Length != PersonalizationSizeBytes)
				throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Personalization)}", _config.Personalization, $"Expected: {nameof(config)}.{nameof(config.Personalization)}.Count == {PersonalizationSizeBytes}");



            _originalKeyLength = (uint) keyArray.Length;

            _key = new byte[128];
            Array.Copy(keyArray, _key, keyArray.Length);

			_salt = saltArray;
			_personalization = personalizationArray;
		}


        public override IHashFunctionBlockTransformer CreateBlockTransformer()
        {
            var blockTransformer = new BlockTransformer(_config.HashSizeInBits, _originalKeyLength, _salt, _personalization);

            if (_originalKeyLength > 0)
                blockTransformer.TransformBytes(_key);
            
            return blockTransformer;
        }

        private class BlockTransformer
            : HashFunctionBlockTransformerBase<BlockTransformer>
        {
            private int _hashSizeInBits;
            private uint _originalKeyLength;

            private UInt64 _a;
            private UInt64 _b;
            private UInt64 _c;
            private UInt64 _d;
            private UInt64 _e;
            private UInt64 _f;
            private UInt64 _g;
            private UInt64 _h;

            private UInt128 _counter = new UInt128(0);
            private byte[] _delayedInputBuffer = null;


            public BlockTransformer()
                : base(inputBlockSize: 128)
            {

            }

            public BlockTransformer(int hashSizeInBits, uint originalKeyLength, byte[] salt, byte[] personalization)
                : this()
            {
                _hashSizeInBits = hashSizeInBits;
                _originalKeyLength = originalKeyLength;


                _a = _iv1;
                _b = _iv2;
                _c = _iv3;
                _d = _iv4;
                _e = _iv5;
                _f = _iv6;
                _g = _iv7;
                _h = _iv8;


                _a ^= 0x01010000U |
                    ((uint) hashSizeInBits / 8) |
                    (originalKeyLength << 8);


                _e ^= BitConverter.ToUInt64(salt, 0);
                _f ^= BitConverter.ToUInt64(salt, 8);

                _g ^= BitConverter.ToUInt64(personalization, 0);
                _h ^= BitConverter.ToUInt64(personalization, 8);
            }


            protected override void CopyStateTo(BlockTransformer other)
            {
                base.CopyStateTo(other);

                other._hashSizeInBits = _hashSizeInBits;
                other._originalKeyLength = _originalKeyLength;

                other._a = _a;
                other._b = _b;
                other._c = _c;
                other._d = _d;
                other._e = _e;
                other._f = _f;
                other._g = _g;
                other._h = _h;

                other._counter = _counter;
                other._delayedInputBuffer = _delayedInputBuffer;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataOffset = data.Offset;
                var dataCount = data.Count;


                var tempA = _a;
                var tempB = _b;
                var tempC = _c;
                var tempD = _d;
                var tempE = _e;
                var tempF = _f;
                var tempG = _g;
                var tempH = _h;

                var tempCounter = _counter;


                // Process _delayedInputBuffer
                if (_delayedInputBuffer != null)
                {
                    tempCounter += new UInt128(128);

                    Compress(
                        ref tempA, ref tempB, ref tempC, ref tempD, ref tempE, ref tempF, ref tempG, ref tempH,
                        tempCounter,
                        _delayedInputBuffer, 0,
                        false);
                }

                // Delay the last 128 bytes of input
                {
                    _delayedInputBuffer = new byte[128];
                    Array.Copy(dataArray, dataOffset + dataCount - 128, _delayedInputBuffer, 0, 128);

                    dataCount -= 128;
                }

                // Process all non-delayed input
                if (dataCount > 0)
                {
                    var endOffset = dataOffset + dataCount;

                    for (var currentOffset = dataOffset; currentOffset < endOffset; currentOffset += 128)
                    {
                        tempCounter += new UInt128(128);

                        Compress(
                            ref tempA, ref tempB, ref tempC, ref tempD, ref tempE, ref tempF, ref tempG, ref tempH,
                            tempCounter,
                            dataArray, currentOffset, 
                            false);
                    }
                }

                _a = tempA;
                _b = tempB;
                _c = tempC;
                _d = tempD;
                _e = tempE;
                _f = tempF;
                _g = tempG;
                _h = tempH;

                _counter = tempCounter;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                var remainder = FinalizeInputBuffer;
                var remainderCount = (remainder?.Length).GetValueOrDefault();

                var tempA = _a;
                var tempB = _b;
                var tempC = _c;
                var tempD = _d;
                var tempE = _e;
                var tempF = _f;
                var tempG = _g;
                var tempH = _h;

                var tempCounter = _counter;


                if (_delayedInputBuffer != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();


                    tempCounter += new UInt128(128);

                    Compress(
                        ref tempA, ref tempB, ref tempC, ref tempD, ref tempE, ref tempF, ref tempG, ref tempH,
                        tempCounter,
                        _delayedInputBuffer, 0,
                        remainderCount == 0);
                }


                if (remainderCount > 0 || _delayedInputBuffer == null)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var finalBuffer = new byte[128];

                    if (remainderCount > 0)
                        Array.Copy(remainder, 0, finalBuffer, 0, remainderCount);


                    tempCounter += new UInt128((UInt64) remainderCount);

                    Compress(
                        ref tempA, ref tempB, ref tempC, ref tempD, ref tempE, ref tempF, ref tempG, ref tempH,
                        tempCounter,
                        finalBuffer, 0,
                        true);
                }


                var hashValueBytes = BitConverter.GetBytes(tempA)
                    .Concat(BitConverter.GetBytes(tempB))
                    .Concat(BitConverter.GetBytes(tempC))
                    .Concat(BitConverter.GetBytes(tempD))
                    .Concat(BitConverter.GetBytes(tempE))
                    .Concat(BitConverter.GetBytes(tempF))
                    .Concat(BitConverter.GetBytes(tempG))
                    .Concat(BitConverter.GetBytes(tempH))
                    .Take(_hashSizeInBits / 8)
                    .ToArray();


                return new HashValue(hashValueBytes, _hashSizeInBits);
            }



            private static void Compress(
                ref UInt64 a, ref UInt64 b, ref UInt64 c, ref UInt64 d, ref UInt64 e, ref UInt64 f, ref UInt64 g, ref UInt64 h, 
                UInt128 counter,
                byte[] dataArray, int dataOffset,
                bool isFinal)
            {
                var reinterpretedData = new UInt64[16];
                Buffer.BlockCopy(dataArray, dataOffset, reinterpretedData, 0, BlockSizeBytes);


                var v = new UInt64[16] {
                    a, b, c, d, e, f, g, h,
                    _iv1, _iv2, _iv3, _iv4, _iv5, _iv6, _iv7, _iv8
                };


                v[12] ^= counter.Low;
                v[13] ^= counter.High;

                if (isFinal)
                    v[14] ^= UInt64.MaxValue;


                ComputeRounds(v, reinterpretedData);


                //Finalization
                a ^= v[0] ^ v[8];
                b ^= v[1] ^ v[9];
                c ^= v[2] ^ v[10];
                d ^= v[3] ^ v[11];
                e ^= v[4] ^ v[12];
                f ^= v[5] ^ v[13];
                g ^= v[6] ^ v[14];
                h ^= v[7] ^ v[15];
            }

        }

	}
}
