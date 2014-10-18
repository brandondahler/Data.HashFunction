using System;
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
        public static byte[] ReadBytes(this Stream source, int count)
        {
            var data = new byte[count];

            int bytesRead;
            var position = 0;

            while ((bytesRead = source.Read(data, position, count - position)) > 0)
            {
                position += bytesRead;

                // Read total requested
                if (position == count)
                    return data;
            }

            // Stream reached end before filling position
            var partialData = new byte[position];

            if (position > 0)
                Array.Copy(data, partialData, position);


            return partialData;
        }

        public static async Task<byte[]> ReadBytesAsync(this Stream source, int count)
        {
            var data = new byte[count];

            int bytesRead;
            var position = 0;

            while ((bytesRead = await source.ReadAsync(data, position, count - position).ConfigureAwait(false)) > 0)
            {
                position += bytesRead;

                // Read total requested
                if (position == count)
                    return data;
            }

            // Stream reached end before filling position
            var partialData = new byte[position];
            Array.Copy(data, partialData, position);

            return partialData;
        }
    }
}
