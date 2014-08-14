namespace HyLibrary.Reflection
{
    using System.Security;

    [SecurityCritical]
    public delegate object MemberGetter(object obj);
}
