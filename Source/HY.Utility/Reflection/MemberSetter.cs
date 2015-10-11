namespace HY.Utitily.Reflection
{
    using System.Security;

    public delegate object MemberSetter(object obj, object value);

    public delegate TType MemberSetter<TType, TMember>(TType obj, TMember value);
}