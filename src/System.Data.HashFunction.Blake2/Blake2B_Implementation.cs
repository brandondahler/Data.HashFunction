using System;
using System.Collections.Generic;
using System.Data.HashFunction.Blake2.Utilities;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Blake2
{
	internal partial class Blake2B_Implementation
		: HashFunctionAsyncBase,
            IBlake2B
    {
        public IBlake2BConfig Config => _config.Clone();


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

        private static readonly UInt64[] IVs = new[] { 
            0x6A09E667F3BCC908UL,
		    0xBB67AE8584CAA73BUL,
            0x3C6EF372FE94F82BUL,
            0xA54FF53A5F1D36F1UL,
            0x510E527FADE682D1UL,
            0x9B05688C2B3E6C1FUL,
            0x1F83D9ABFB41BD6BUL,
            0x5BE0CD19137E2179UL
        };


        private class InternalState
        {
            public int BufferFilled = 0;
            public readonly byte[] Buffer = new byte[128];

            public readonly UInt64[] H = new UInt64[8];
            public UInt128 Counter = new UInt128();
            public readonly UInt64[] FinalizationFlags = new UInt64[2];


            public InternalState(int hashSize, uint originalKeyLength, byte[] salt, byte[] personalization)
            {
                Array.Copy(IVs, H, IVs.Length);

                H[0] ^= 0x01010000U |
                    ((uint) hashSize / 8) |
                    (originalKeyLength << 8);


                H[4] ^= BitConverter.ToUInt64(salt, 0);
                H[5] ^= BitConverter.ToUInt64(salt, 8);

                H[6] ^= BitConverter.ToUInt64(personalization, 0);
                H[7] ^= BitConverter.ToUInt64(personalization, 8);
            }
        }



		/// <summary>
		/// Initializes an instance of the <see cref="Blake2B_Implementation"/> class with the provided
		/// <paramref name="hashSize"/>. If not null, the <paramref name="key"/>, 
		/// <paramref name="salt"/>, and <paramref name="personalization"/> arguments will be 
		/// applied to the hashing algorithm. 
		/// 
		/// The <paramref name="hashSize"/> must be 8 bits at the least and 512 bits at most, and 
		/// the size in bits must be a multiple of 8.
		/// The <paramref name="key"/> parameter must be a byte sequence of at most 64 bytes.
		/// The <paramref name="salt"/> parameter must be a byte sequence of exactly 16 bytes.
		/// The <paramref name="personalization"/> parameter must be a byte sequence of exactly 16 
		/// bytes.
		/// </summary>
		/// <param name="key">Key to seed the hash with</param>
		/// <param name="salt">Salt to seed the hash with</param>
		/// <param name="personalization">Personalization to seed the hash with</param>
		/// <param name="hashSize">Hash size to use for the output</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Either the provided <paramref name="hashSize"/>, <paramref name="key"/> length, 
		/// <paramref name="salt"/> length, or <paramref name="personalization"/> length is invalid.
		/// </exception>
        public Blake2B_Implementation(IBlake2BConfig config)
			: base((config?.HashSizeInBits).GetValueOrDefault())
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            

            if (config.HashSizeInBits < MinHashSizeBits || config.HashSizeInBits > MaxHashSizeBits)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", config.HashSizeInBits, $"Expected: {MinHashSizeBits} >= {nameof(config)}.{nameof(config.HashSizeInBits)} <= {MaxHashSizeBits}");

			if (config.HashSizeInBits % 8 != 0)
				throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", config.HashSizeInBits, $"{ nameof(config) }.{ nameof(config.HashSizeInBits)} must be a multiple of 8.");



            _config = config.Clone();


            var keyArray = (config.Key ?? new byte[0]).ToArray();
            var saltArray = (config.Salt ?? new byte[16]).ToArray();
            var personalizationArray = (config.Personalization ?? new byte[16]).ToArray();

			if (keyArray.Length > MaxKeySizeBytes)
				throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Key)}", config.Key, $"Expected: {nameof(config)}.{nameof(config.Key)}.Count <= {MaxKeySizeBytes}");

			if (saltArray.Length != SaltSizeBytes)
				throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Salt)}", config.Key, $"Expected: {nameof(config)}.{nameof(config.Salt)}.Count == {SaltSizeBytes}");

			if (personalizationArray.Length != PersonalizationSizeBytes)
				throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Personalization)}", config.Personalization, $"Expected: {nameof(config)}.{nameof(config.Personalization)}.Count == {PersonalizationSizeBytes}");



            _originalKeyLength = (uint) keyArray.Length;

            _key = new byte[128];
            Array.Copy(keyArray, _key, keyArray.Length);

			_salt = saltArray;
			_personalization = personalizationArray;
		}


		/// <inheritdoc />
		protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
		{
            var internalState = new InternalState(_config.HashSizeInBits, _originalKeyLength, _salt, _personalization);


			if (_originalKeyLength > 0)
				ProcessBytes(internalState, _key, 0, _key.Length);

            data.ForEachGroup(
                BlockSizeBytes,
                (array, start, count) => ProcessBytes(internalState, array, start, count),
                (array, start, count) => ProcessBytes(internalState, array, start, count),
                cancellationToken);

            return Final(_config.HashSizeInBits, internalState);
		}



		/// <inheritdoc />
		protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
		{
            var internalState = new InternalState(_config.HashSizeInBits, _originalKeyLength, _salt, _personalization);


            if (_originalKeyLength > 0)
                ProcessBytes(internalState, _key, 0, _key.Length);

			await data.ForEachGroupAsync(
                    BlockSizeBytes,
                    (array, start, count) => ProcessBytes(internalState, array, start, count),
                    (array, start, count) => ProcessBytes(internalState, array, start, count),
                    cancellationToken)
                .ConfigureAwait(false);

			return Final(_config.HashSizeInBits, internalState);
		}


        private void ProcessBytes(InternalState internalState, byte[] array, int start, int count)
        {
			int bufferRemaining = BlockSizeBytes - internalState.BufferFilled;

			if (internalState.BufferFilled > 0 && count > bufferRemaining)
			{
				Array.Copy(
                    array, start, 
                    internalState.Buffer, internalState.BufferFilled, 
                    bufferRemaining);


				internalState.Counter += new UInt128(BlockSizeBytes);
				
                Compress(internalState, internalState.Buffer, 0);

				start += bufferRemaining;
				count -= bufferRemaining;
				internalState.BufferFilled = 0;
			}

			while (count > BlockSizeBytes)
			{
				internalState.Counter += new UInt128(BlockSizeBytes);
				
				Compress(internalState, array, start);
				
                start += BlockSizeBytes;
				count -= BlockSizeBytes;
			}

			if (count > 0)
			{
				Array.Copy(
                    array, start, 
                    internalState.Buffer, internalState.BufferFilled, 
                    count);

				internalState.BufferFilled += count;
			}
		}


		private byte[] Final(int hashSize, InternalState internalState)
		{
			//Last compression
			internalState.Counter += new UInt128((UInt32) internalState.BufferFilled);
			internalState.FinalizationFlags[0] = UInt64.MaxValue;

			for (int i = internalState.BufferFilled; i < internalState.Buffer.Length; i++)
				internalState.Buffer[i] = 0;

			Compress(internalState, internalState.Buffer, 0);


			byte[] hash = new byte[64];

            for (int i = 0; i < 8; ++i)
            {
                Array.Copy(
                    BitConverter.GetBytes(internalState.H[i]), 0, 
                    hash, i * 8, 
                    8);
            }


            if (hash.Length != hashSize / 8)
			{
                var result = new byte[hashSize / 8];

				Array.Copy(
                    hash, 
                    result, result.Length);


				return result;
			}

			return hash;
		}

		private static void Compress(InternalState internalState, byte[] block, int start)
		{
            var m = new UInt64[16];
		    Buffer.BlockCopy(block, start, m, 0, BlockSizeBytes);


            var v = new UInt64[16];
            Array.Copy(internalState.H, v, internalState.H.Length);
            Array.Copy(IVs, 0, v, 8, IVs.Length);

			v[12] ^= internalState.Counter.Low;
			v[13] ^= internalState.Counter.High;
			v[14] ^= internalState.FinalizationFlags[0];
			v[15] ^= internalState.FinalizationFlags[1];


            ComputeRounds(v, m);

			//Finalization
			internalState.H[0] ^= v[0] ^ v[8];
			internalState.H[1] ^= v[1] ^ v[9];
			internalState.H[2] ^= v[2] ^ v[10];
			internalState.H[3] ^= v[3] ^ v[11];
			internalState.H[4] ^= v[4] ^ v[12];
			internalState.H[5] ^= v[5] ^ v[13];
			internalState.H[6] ^= v[6] ^ v[14];
			internalState.H[7] ^= v[7] ^ v[15];
		}
	}
}
