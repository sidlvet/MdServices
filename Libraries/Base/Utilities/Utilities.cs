using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MdServices.Base
{
    /// <summary>
    /// Conversion metrics utilities
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Generate data size string. Will return a pretty string of bytes, KiB, MiB, GiB, TiB based on the given bytes.
        /// </summary>
        /// <param name="b">Data size in bytes</param>
        /// <returns>String with data size representation</returns>
        public static string GenerateDataSize(double b)
        {
            var sb = new StringBuilder();

            long bytes = (long)b;
            long absBytes = Math.Abs(bytes);

            if (absBytes >= (1024L * 1024L * 1024L * 1024L))
            {
                long tb = bytes / (1024L * 1024L * 1024L * 1024L);
                long gb = (bytes % (1024L * 1024L * 1024L * 1024L)) / (1024 * 1024 * 1024);
                sb.Append(tb);
                sb.Append('.');
                sb.Append((gb < 100) ? "0" : "");
                sb.Append((gb < 10) ? "0" : "");
                sb.Append(gb);
                sb.Append(" TiB");
            }
            else if (absBytes >= (1024 * 1024 * 1024))
            {
                long gb = bytes / (1024 * 1024 * 1024);
                long mb = (bytes % (1024 * 1024 * 1024)) / (1024 * 1024);
                sb.Append(gb);
                sb.Append('.');
                sb.Append((mb < 100) ? "0" : "");
                sb.Append((mb < 10) ? "0" : "");
                sb.Append(mb);
                sb.Append(" GiB");
            }
            else if (absBytes >= (1024 * 1024))
            {
                long mb = bytes / (1024 * 1024);
                long kb = (bytes % (1024 * 1024)) / 1024;
                sb.Append(mb);
                sb.Append('.');
                sb.Append((kb < 100) ? "0" : "");
                sb.Append((kb < 10) ? "0" : "");
                sb.Append(kb);
                sb.Append(" MiB");
            }
            else if (absBytes >= 1024)
            {
                long kb = bytes / 1024;
                bytes = bytes % 1024;
                sb.Append(kb);
                sb.Append('.');
                sb.Append((bytes < 100) ? "0" : "");
                sb.Append((bytes < 10) ? "0" : "");
                sb.Append(bytes);
                sb.Append(" KiB");
            }
            else
            {
                sb.Append(bytes);
                sb.Append(" bytes");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generate time period string. Will return a pretty string of ns, mcs, ms, s, m, h based on the given nanoseconds.
        /// </summary>
        /// <param name="ms">Milliseconds</param>
        /// <returns>String with time period representation</returns>
        public static string GenerateTimePeriod(double ms)
        {
            var sb = new StringBuilder();

            long nanoseconds = (long)(ms * 1000.0 * 1000.0);
            long absNanoseconds = Math.Abs(nanoseconds);

            if (absNanoseconds >= (60 * 60 * 1000000000L))
            {
                long hours = nanoseconds / (60 * 60 * 1000000000L);
                long minutes = ((nanoseconds % (60 * 60 * 1000000000L)) / 1000000000) / 60;
                long seconds = ((nanoseconds % (60 * 60 * 1000000000L)) / 1000000000) % 60;
                long milliseconds = ((nanoseconds % (60 * 60 * 1000000000L)) % 1000000000) / 1000000;
                sb.Append(hours);
                sb.Append(':');
                sb.Append((minutes < 10) ? "0" : "");
                sb.Append(minutes);
                sb.Append(':');
                sb.Append((seconds < 10) ? "0" : "");
                sb.Append(seconds);
                sb.Append('.');
                sb.Append((milliseconds < 100) ? "0" : "");
                sb.Append((milliseconds < 10) ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" h");
            }
            else if (absNanoseconds >= (60 * 1000000000L))
            {
                long minutes = nanoseconds / (60 * 1000000000L);
                long seconds = (nanoseconds % (60 * 1000000000L)) / 1000000000;
                long milliseconds = ((nanoseconds % (60 * 1000000000L)) % 1000000000) / 1000000;
                sb.Append(minutes);
                sb.Append(':');
                sb.Append((seconds < 10) ? "0" : "");
                sb.Append(seconds);
                sb.Append('.');
                sb.Append((milliseconds < 100) ? "0" : "");
                sb.Append((milliseconds < 10) ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" m");
            }
            else if (absNanoseconds >= 1000000000)
            {
                long seconds = nanoseconds / 1000000000;
                long milliseconds = (nanoseconds % 1000000000) / 1000000;
                sb.Append(seconds);
                sb.Append('.');
                sb.Append((milliseconds < 100) ? "0" : "");
                sb.Append((milliseconds < 10) ? "0" : "");
                sb.Append(milliseconds);
                sb.Append(" s");
            }
            else if (absNanoseconds >= 1000000)
            {
                long milliseconds = nanoseconds / 1000000;
                long microseconds = (nanoseconds % 1000000) / 1000;
                sb.Append(milliseconds);
                sb.Append('.');
                sb.Append((microseconds < 100) ? "0" : "");
                sb.Append((microseconds < 10) ? "0" : "");
                sb.Append(microseconds);
                sb.Append(" ms");
            }
            else if (absNanoseconds >= 1000)
            {
                long microseconds = nanoseconds / 1000;
                nanoseconds = nanoseconds % 1000;
                sb.Append(microseconds);
                sb.Append('.');
                sb.Append((nanoseconds < 100) ? "0" : "");
                sb.Append((nanoseconds < 10) ? "0" : "");
                sb.Append(nanoseconds);
                sb.Append(" mcs");
            }
            else
            {
                sb.Append(nanoseconds);
                sb.Append(" ns");
            }

            return sb.ToString();
        }

        public static Stream StringToStream(string json)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            return new MemoryStream(byteArray);
        }

        public static byte[] Compress<T>(T setting)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            byte[] compressedBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream compressedStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    serializer.WriteObject(compressedStream, setting);
                }

                compressedBytes = memoryStream.ToArray();
            }

            return compressedBytes;
        }

        public static T Decompress<T>(Stream compressedStream)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            T restoredData;
            using (GZipStream decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress, true))
            {
                restoredData = (T)serializer.ReadObject(decompressedStream);
            }

            return restoredData;
        }

        public static string Hex(byte[] bytes, int bytesPerLine = 16)
        {
            if (bytes == null) return "<null>";
            int bytesLength = bytes.Length;

            char[] HexChars = "0123456789ABCDEF".ToCharArray();

            int firstHexColumn =
                  8                   // 8 characters for the address
                + 3;                  // 3 spaces

            int firstCharColumn = firstHexColumn
                + bytesPerLine * 3       // - 2 digit for the hexadecimal value and 1 space
                + (bytesPerLine - 1) / 8 // - 1 extra space every 8 characters from the 9th
                + 2;                  // 2 spaces 

            int lineLength = firstCharColumn
                + bytesPerLine           // - characters to show the ascii value
                + Environment.NewLine.Length; // Carriage return and line feed (should normally be 2)

            char[] line = (new String(' ', lineLength - Environment.NewLine.Length) + Environment.NewLine).ToCharArray();
            int expectedLines = (bytesLength + bytesPerLine - 1) / bytesPerLine;
            StringBuilder result = new StringBuilder(expectedLines * lineLength);

            for (int i = 0; i < bytesLength; i += bytesPerLine)
            {
                line[0] = HexChars[(i >> 28) & 0xF];
                line[1] = HexChars[(i >> 24) & 0xF];
                line[2] = HexChars[(i >> 20) & 0xF];
                line[3] = HexChars[(i >> 16) & 0xF];
                line[4] = HexChars[(i >> 12) & 0xF];
                line[5] = HexChars[(i >> 8) & 0xF];
                line[6] = HexChars[(i >> 4) & 0xF];
                line[7] = HexChars[(i >> 0) & 0xF];

                int hexColumn = firstHexColumn;
                int charColumn = firstCharColumn;

                for (int j = 0; j < bytesPerLine; j++)
                {
                    if (j > 0 && (j & 7) == 0) hexColumn++;
                    if (i + j >= bytesLength)
                    {
                        line[hexColumn] = ' ';
                        line[hexColumn + 1] = ' ';
                        line[charColumn] = ' ';
                    }
                    else
                    {
                        byte b = bytes[i + j];
                        line[hexColumn] = HexChars[(b >> 4) & 0xF];
                        line[hexColumn + 1] = HexChars[b & 0xF];
                        line[charColumn] = (b < 32 ? '·' : (char)b);
                    }
                    hexColumn += 3;
                    charColumn++;
                }
                result.Append(line);
            }
            return result.ToString();
        }
    }
}
