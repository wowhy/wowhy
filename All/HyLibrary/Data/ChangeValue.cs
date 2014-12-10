namespace HyLibrary.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ChangeValue
    {
        public object Origin { get; private set; }

        public object Value { get; private set; }

        public PropertyInfo PropertyInfo { get; private set; }

        public ChangeValue(object origin, object value, PropertyInfo propertyInfo)
        {
            this.Origin = origin;
            this.Value = value;
            this.PropertyInfo = propertyInfo;
        }
    }
}
