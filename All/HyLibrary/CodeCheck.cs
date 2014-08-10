namespace HyLibrary
{
    using System.Diagnostics.Contracts;

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
    }
}
