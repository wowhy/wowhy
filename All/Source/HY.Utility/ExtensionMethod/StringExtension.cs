namespace HY.Utitily.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HY.Utitily.Reflection;
    using System.Text.RegularExpressions;

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

        /// <summary>
        /// 将字符串转换成字节
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(this string input, Encoding encoding)
        {
            if (input == null)
                return null;

            return encoding.GetBytes(input);
        }

        /// <summary>
        /// 使用UTF8格式转换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(this string input)
        {
            return ToBytes(input, Encoding.UTF8);
        }

        /// <summary>
        /// 按照类型名称创建Type对象（缓存）
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <returns>Type对象</returns>
        public static Type ToType(this string typeName)
        {
            return ReflectionHelper.Instance.GetType(typeName);
        }

        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join<string>(separator, values);
        }

        public static string Join(this IEnumerable<string> values, char separator)
        {
            return string.Join<string>(separator.ToString(), values);
        }

        #region Format
        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string Format(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        public static string Format(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        public static string Format(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        #endregion

        #region Regex
        public static bool IsMatch(this string input, string pattern)
        {
            return IsMatch(input, new Regex(pattern));
        }

        public static bool IsMatch(this string input, Regex regex)
        {
            return regex.IsMatch(input);
        }

        public static bool MatchTo(this string pattern, string input)
        {
            return Regex.IsMatch(input, pattern);
        }
        #endregion

    }
}