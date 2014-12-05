namespace HyLibrary
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// 检查代码
    /// </summary>
    public static class CodeCheck
    {
        public static void Null<TValue>(TValue value, string userMessage)
        {
            Contract.Assert(value == null, userMessage);
        }

        public static void NotNull<TValue>(TValue value, string userMessage)
        {
            Contract.Assert(value != null, userMessage);
        }

        public static void NotEmpty(string value, string userMessage)
        {
            Contract.Assert(!string.IsNullOrWhiteSpace(value), userMessage);
        }

        public static void IsTrue(bool condition, string userMessage)
        {
            Contract.Assert(condition, userMessage);
        }

        public static void IsFalse(bool condition, string userMessage)
        {
            Contract.Assert(!condition, userMessage);
        }
    }
}