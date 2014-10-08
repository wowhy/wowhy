namespace HyLibrary.Common
{
    public interface IOperable<T>
    {
        T Addition(T op1, T op2);
        T Subtraction(T op1, T op2);
        T Multiply(T op1, T op2);
        T Division(T op1, T op2);
        T Modulus(T op1, T op2);

        T BitwiseAnd(T op1, T op2);
        T BitwiseOr(T op1, T op2);
        T ExclusiveOr(T op1, T op2);

        T UnaryNegation(T op);
        T OnesComplement(T op);

        bool Equality(T op1, T op2);
        bool Inequality(T op1, T op2);
        bool GreaterThan(T op1, T op2);
        bool GreaterThanOrEqual(T op1, T op2);
        bool LessThan(T op1, T op2);
        bool LessThanOrEqual(T op1, T op2);
    }

    public interface IOperable
    {
        object Addition(object op1, object op2);
        object Subtraction(object op1, object op2);
        object Multiply(object op1, object op2);
        object Division(object op1, object op2);
        object Modulus(object op1, object op2);

        object BitwiseAnd(object op1, object op2);
        object BitwiseOr(object op1, object op2);
        object ExclusiveOr(object op1, object op2);

        object UnaryNegation(object op);
        object OnesComplement(object op);

        bool Equality(object op1, object op2);
        bool Inequality(object op1, object op2);
        bool GreaterThan(object op1, object op2);
        bool GreaterThanOrEqual(object op1, object op2);
        bool LessThan(object op1, object op2);
        bool LessThanOrEqual(object op1, object op2);
    }
}