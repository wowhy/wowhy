namespace HyLibrary.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HyLibrary.Reflection;

    public static class StringExtension
    {
        private static readonly char[] codes =
            "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        private static DateTime unix_timestamp = new DateTime(1970, 1, 1);

        /// <summary>
        /// 生成指定长度的随机字符串
        /// 生成字符包括 "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机字符串</returns>
        public static string RandomCode(int length = 4)
        {
            StringBuilder sb = new StringBuilder(length + 1);
            Random rand = new Random((int)(DateTime.Now - unix_timestamp).TotalSeconds);
            for (int i = 0; i < length; i++)
            {
                sb.Append(codes[rand.Next(codes.Length)]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将给定字符串随机打乱顺序
        /// </summary>
        /// <param name="input">System.String对象</param>
        /// <returns>随机顺序的"input"</returns>
        public static string Random(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            Random rand = new Random((int)(DateTime.Now - unix_timestamp).TotalSeconds);
            var chars = input.ToArray();
            int p;
            for (int i = 0; i < chars.Length; i++)
            {
                p = rand.Next(chars.Length);
                char t = chars[i];
                chars[i] = chars[p];
                chars[p] = t;
            }

            return new string(chars);
        }

        public static byte[] ToBytes(this string input, Encoding encoding)
        {
            if (input == null)
                return null;

            return encoding.GetBytes(input);
        }

        public static byte[] ToBytes(this string input)
        {
            return ToBytes(input, Encoding.UTF8);
        }

        public static Type ToType(this string typeName)
        {
            return ReflectionHelper.Instance.GetType(typeName);
        }
    }
}