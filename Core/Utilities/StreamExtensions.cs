﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities
{
    /// <summary>
    /// Static class to provide Stream extension functions.
    /// </summary>
    internal static class StreamExtensions
    {
        /// <summary>
        /// Reads data from stream into buffer array and yields each byte.
        /// </summary>
        /// <param name="source">Stream to read data from.</param>
        /// <param name="bufferSize">Size of buffer array.</param>
        /// <returns>
        /// Yields each byte in stream until the end of the stream is reached.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">source;source must be a non-null, readable Stream instance.</exception>
        /// <exception cref="System.ArgumentException">source must be a readable Stream instance.;source</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">bufferSize;bufferSize must be greater than or equal to 1.</exception>
        public static IEnumerable<byte> AsEnumerable(this Stream source, int bufferSize = 4096)
        {
            if (source == null)
                throw new ArgumentNullException("source", "source must be a non-null, readable Stream instance.");

            if (source.CanRead == false)
                throw new ArgumentException("source must be a readable Stream instance.", "source");


            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize", "bufferSize must be greater than or equal to 1.");



            using (var br = new BinaryReader(source, Encoding.UTF8, true))
            {
                var blocks = new byte[bufferSize];
                int bytesRead;

                while ((bytesRead = source.Read(blocks, 0, bufferSize)) > 0)
                {
                    for (int x = 0; x < bytesRead; ++x)
                        yield return blocks[x];
                }
            }
        }

        /// <summary>
        /// Wrapper for returning a new GroupedStreamData instance.
        /// </summary>
        /// <param name="source">Stream to read data from.</param>
        /// <param name="groupSize">Size of grouped arrays.</param>
        /// <returns>
        /// GroupedStremaData instance.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">source;source must be a non-null, readable Stream instance.</exception>
        /// <exception cref="System.ArgumentException">source must be a readable Stream instance.;source</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">groupSize;groupSize must be greater than or equal to 1.</exception>
        public static GroupedStreamData AsGroupedStreamData(this Stream source, int groupSize)
        {
            if (source == null)
                throw new ArgumentNullException("source", "source must be a non-null, readable Stream instance.");

            if (source.CanRead == false)
                throw new ArgumentException("source must be a readable Stream instance.", "source");


            if (groupSize < 0)
                throw new ArgumentOutOfRangeException("groupSize", "groupSize must be greater than or equal to 1.");



            return new GroupedStreamData(source, groupSize);
        }

        /// <summary>
        /// Allows yielding each group as it is read from the stream and handles saving of the remainder bytes.
        /// </summary>
        public class GroupedStreamData : IEnumerable<byte[]>
        {
            /// <summary>
            /// Byte array containing partial data from the end of the stream or an empty array.
            /// </summary>
            /// <value>
            /// The remainder bytes.
            /// </value>
            /// <exception cref="System.InvalidOperationException">Remainder property is not valid until GroupedStreamData has been enumerated.</exception>
            /// <remarks>
            /// Throws an exception if the <see cref="GroupedStreamData" /> has not yet been enumerated.
            /// </remarks>
            public byte[] Remainder 
            { 
                get 
                {
                    if (_Remainder == null)
                        throw new InvalidOperationException("Remainder property is not valid until GroupedStreamData has been enumerated.");
                    
                    return _Remainder; 
                } 
            }


            private byte[] _Remainder = null;
            
            private readonly Stream _source;
            private readonly int _groupSize;


            /// <summary>
            /// Initializes a new instance of the <see cref="GroupedStreamData"/> class.
            /// </summary>
            /// <param name="source">Stream to read data from.</param>
            /// <param name="groupSize">Size of grouped arrays.</param>
            /// <exception cref="System.ArgumentNullException">source;source must be a non-null, readable Stream instance.</exception>
            /// <exception cref="System.ArgumentException">source must be a readable Stream instance.;source</exception>
            /// <exception cref="System.ArgumentOutOfRangeException">groupSize;groupSize must be greater than or equal to 1.</exception>
            public GroupedStreamData(Stream source, int groupSize)
            {
                if (source == null)
                    throw new ArgumentNullException("source", "source must be a non-null, readable Stream instance.");

                if (source.CanRead == false)
                    throw new ArgumentException("source must be a readable Stream instance.", "source");


                if (groupSize <= 0)
                    throw new ArgumentOutOfRangeException("groupSize", "groupSize must be greater than or equal to 1.");


                _source = source;
                _groupSize = groupSize;
            }


            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<byte[]> GetEnumerator()
            {
                return EnumerateGroups().GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }


            private IEnumerable<byte[]> EnumerateGroups()
            {
                using (var br = new BinaryReader(_source, Encoding.UTF8, true))
                {
                    byte[] blocks;

                    while ((blocks = br.ReadBytes(_groupSize)).Length == _groupSize)
                        yield return blocks;

                    // Set remainder to last partial block **or** an empty array
                    _Remainder = blocks;
                }
            }
        }
    }
}