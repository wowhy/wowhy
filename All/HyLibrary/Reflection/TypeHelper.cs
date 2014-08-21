namespace HyLibrary.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TypeHelper
    {
        public static bool CompareParameterTypes(Type goal, Type probe)
        {
            if (goal == probe)
                return true;

            if (goal.IsGenericParameter)
                return CheckConstraints(goal, probe);
            if (goal.IsGenericType && probe.IsGenericType)
                return CompareGenericTypes(goal, probe);

            return false;
        }

        public static bool CheckConstraints(Type goal, Type probe)
        {
            var constraints = goal.GetGenericParameterConstraints();

            for (var i = 0; i < constraints.Length; i++)
                if (!constraints[i].IsAssignableFrom(probe))
                    return false;

            return true;
        }

        public static bool CompareGenericTypes(Type goal, Type probe)
        {
            var genArgs = goal.GetGenericArguments();
            var specArgs = probe.GetGenericArguments();
            var match = (genArgs.Length == specArgs.Length);

            for (var i = 0; match && i < genArgs.Length; i++)
            {
                if (genArgs[i] == specArgs[i])
                    continue;

                if (genArgs[i].IsGenericParameter)
                    match = CheckConstraints(genArgs[i], specArgs[i]);
                else if (genArgs[i].IsGenericType && specArgs[i].IsGenericType)
                    match = CompareGenericTypes(genArgs[i], specArgs[i]);
                else
                    match = false;
            }

            return match;
        }
    }
}