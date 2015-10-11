namespace HY.Utitily.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class BytesExtension
    {
        public static MemoryStream ToStream(this byte[] buffer)
        {
            if (buffer == null) return null;
            return new MemoryStream(buffer);
        }

        #region Convert

        public static string ToText(this byte[] buffer)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return Encoding.UTF8.GetString(buffer);
        }

        public static string ToText(this byte[] buffer, Encoding encode)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return encode.GetString(buffer);
        }

        public static string ToText(this byte[] buffer, string encode)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return Encoding.GetEncoding(encode).GetString(buffer);
        }

        public static short ToInt16(this byte[] buffer, int start = 0)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return BitConverter.ToInt16(buffer, start);
        }

        public static int ToInt32(this byte[] buffer, int start = 0)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return BitConverter.ToInt32(buffer, start);
        }

        public static long ToInt64(this byte[] buffer, int start = 0)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return BitConverter.ToInt64(buffer, start);
        }

        public static ushort ToUInt16(this byte[] buffer, int start = 0)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return BitConverter.ToUInt16(buffer, start);
        }

        public static uint ToUInt32(this byte[] buffer, int start = 0)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return BitConverter.ToUInt32(buffer, start);
        }

        public static ulong ToUInt64(this byte[] buffer, int start = 0)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return BitConverter.ToUInt64(buffer, start);
        }

        public static DateTime ToDateTime(this byte[] buffer, int start = 0)
        {
            CodeCheck.NotNull(buffer, "buffer");
            return DateTime.FromBinary(BitConverter.ToInt64(buffer, start));
        }
        #endregion

        public static string ToHexString(this byte[] buffer, bool lower = true)
        {
            CodeCheck.NotNull(buffer, "buffer");
            var builder = new StringBuilder(buffer.Length * 2 + 1);
            var format = lower ? "x2" : "X2";
            for (var i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString(format));
            }

            return builder.ToString();
        }
    }
}