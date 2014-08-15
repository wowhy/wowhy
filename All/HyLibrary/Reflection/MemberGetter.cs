namespace HyLibrary.Reflection
{
    using System.Security;

    public delegate object MemberGetter(object obj);

    public delegate TProperty MemberGetter<TType, TProperty>(TType obj);
}