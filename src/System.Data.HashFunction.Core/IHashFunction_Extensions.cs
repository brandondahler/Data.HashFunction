using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Numerics;
#if NET45
using System.Runtime.Serialization.Formatters.Binary;
#endif
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Static class to provide extension functions for IHashFunction instances.
    /// </summary>
    public static class IHashFunction_Extensions
    {
        
        #region ComputeHash

        #region Sugar

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, bool data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, byte data)
        {
            return hashFunction.ComputeHash(
                new[] { data });
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, char data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, double data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, float data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, int data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, long data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, sbyte data)
        {
            return hashFunction.ComputeHash(
                new[] { (byte)data });
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, short data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        /// <remarks>
        /// UTF-8 encoding used to convert string to bytes.
        /// </remarks>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, string data)
        {
            return hashFunction.ComputeHash(
                Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, uint data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, ulong data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, ushort data)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data));
        }

#if NET45
        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <typeparam name="ModelT">Type of data to be hashed.</typeparam>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        /// <remarks>
        /// <see cref="BinaryFormatter"/> is used to turn given data into a byte array.
        /// </remarks>
        public static IHashValue ComputeHash<ModelT>(this IHashFunction hashFunction, ModelT data)
        {
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, data);

                return hashFunction.ComputeHash(
                    memoryStream.ToArray());
            }
        }
#endif

        #endregion

        #region Sugar with desiredSize

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, bool data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, byte data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                new[] { data },
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, char data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, double data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, float data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, int data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, long data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, sbyte data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                new[] { (byte)data },
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, short data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        /// <remarks>
        /// UTF-8 encoding used to convert string to bytes.
        /// </remarks>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, string data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                Encoding.UTF8.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, uint data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, ulong data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, ushort data, int desiredHashSize)
        {
            return hashFunction.ComputeHash(
                BitConverter.GetBytes(data),
                desiredHashSize);
        }

#if NET45

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <typeparam name="ModelT">Type of data to be hashed.</typeparam>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        /// <remarks>
        /// <see cref="BinaryFormatter"/> is used to turn given data into a byte array.
        /// </remarks>
        public static IHashValue ComputeHash<ModelT>(this IHashFunction hashFunction, ModelT data, int desiredHashSize)
        {
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, data);

                return hashFunction.ComputeHash(
                    memoryStream.ToArray(),
                    desiredHashSize);
            }
        }

#endif

        /// <summary>
        /// Computes hash value for given data.
        /// </summary>
        /// <param name="hashFunction">Hash function to use.</param>
        /// <param name="data">Data to be hashed.</param>
        /// <param name="desiredHashSize">Desired size of resulting hash, in bits.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public static IHashValue ComputeHash(this IHashFunction hashFunction, byte[] data, int desiredHashSize)
        {
            var hash = new BigInteger();
            var desiredHashBytes = (desiredHashSize + 7) / 8;

            var seededData = new byte[data.Length + 4];
            Array.Copy(data, 0, seededData, 4, data.Length);

            var hashesNeeded = (desiredHashSize + (hashFunction.HashSize - 1)) / hashFunction.HashSize;

            // Compute as many hashes as needed
            for (int x = 0; x < Math.Max(hashesNeeded, 1); ++x)
            {
                byte[] currentData;

                if (x != 0)
                {
                    Array.Copy(BitConverter.GetBytes(x), seededData, 4);
                    currentData = seededData;

                }
                else
                {
                    // Use original data for first 
                    currentData = data;
                }


                var elementHash = new BigInteger(
                    hashFunction.ComputeHash(currentData)
                        .Hash
                        .Concat(new[] { (byte) 0 })
                        .ToArray());

                hash |= elementHash << (x * hashFunction.HashSize);
            }


            // XOr-fold the extra bits
            if (hashesNeeded * hashFunction.HashSize != desiredHashSize)
            {
                var mask = ((new BigInteger(1) << desiredHashSize) - 1);

                hash = (hash ^ (hash >> desiredHashSize)) & mask;
            }


            // Convert to array that contains desiredHashSize bits
            var hashBytes = hash.ToByteArray();

            // Account for missing or extra bytes.
            if (hashBytes.Length != desiredHashBytes)
            {
                var buffer = new byte[desiredHashBytes];
                Array.Copy(hashBytes, buffer, Math.Min(hashBytes.Length, desiredHashBytes));

                hashBytes = buffer;
            }

            return new HashValue(hashBytes, desiredHashSize);
        }

        #endregion

        #endregion

    }
}
