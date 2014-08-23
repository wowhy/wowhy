namespace HyLibrary.Reflection.Emit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Text;
    using System.Threading.Tasks;

    public class TypeBuilderHelper
    {
        private AssemblyBuilderHelper assemblyBuilderHelper;

        private TypeBuilder typeBuilder;

        public TypeBuilderHelper(
            AssemblyBuilderHelper assemblyBuilderHelper,
            TypeBuilder typeBuilder)
        {
            CodeCheck.NotNull(assemblyBuilderHelper, "assemblyBuilderHelper");
            CodeCheck.NotNull(typeBuilder, "typeBuilder");

            this.assemblyBuilderHelper = assemblyBuilderHelper;
            this.typeBuilder = typeBuilder;
        }

        public AssemblyBuilderHelper Assembly
        {
            get { return assemblyBuilderHelper; }
        }

        public TypeBuilder TypeBuilder
        {
            get { return typeBuilder; }
        }

        public Type Create()
        {
            return this.typeBuilder.CreateType();
        }

        #region CustomAttribute
        public TypeBuilderHelper SetCustomAttribute(Type attributeType)
        {
            return this.SetCustomAttribute(attributeType, new object[0]);
        }

        public TypeBuilderHelper SetCustomAttribute(Type attributeType, object[] args)
        {
            CodeCheck.NotNull(attributeType, "attributeType");

            var ci = attributeType.GetConstructor(Type.EmptyTypes);
            var caBuilder = new CustomAttributeBuilder(ci, args);

            this.typeBuilder.SetCustomAttribute(caBuilder);
            return this;
        }

        public TypeBuilderHelper SetCustomAttribute(
            Type attributeType,
            PropertyInfo[] properties,
            object[] propertyValues)
        {
            return this.SetCustomAttribute(attributeType, new object[0], properties, propertyValues);
        }

        public TypeBuilderHelper SetCustomAttribute(
            Type attributeType,
            string propertyName,
            object propertyValue)
        {
            return this.SetCustomAttribute(
                attributeType,
                new object[0],
                new PropertyInfo[] { attributeType.GetProperty(propertyName) },
                new object[] { propertyValue });
        }

        public TypeBuilderHelper SetCustomAttribute(
            Type attributeType,
            object[] args,
            PropertyInfo[] properties,
            object[] propertyValues)
        {
            CodeCheck.NotNull(attributeType, "attributeType");

            var ci = attributeType.GetConstructor(Type.EmptyTypes);
            var caBuilder = new CustomAttributeBuilder(ci, args, properties, propertyValues);

            this.typeBuilder.SetCustomAttribute(caBuilder);
            return this;
        }
        #endregion

        #region Constructors
        #endregion

        #region DefineNestedType
        public TypeBuilderHelper DefineNestedType(string name)
        {
            return new TypeBuilderHelper(this.assemblyBuilderHelper, this.typeBuilder.DefineNestedType(name));
        }

        public TypeBuilderHelper DefineNestedType(string name, Type parent)
        {
            return new TypeBuilderHelper(
                this.assemblyBuilderHelper,
                this.typeBuilder.DefineNestedType(name, TypeAttributes.NestedPublic, parent));
        }

        public TypeBuilderHelper DefineNestedType(
            string name,
            TypeAttributes attributes,
            Type parent)
        {
            return new TypeBuilderHelper(
                this.assemblyBuilderHelper,
                this.typeBuilder.DefineNestedType(name, attributes, parent));
        }

        public TypeBuilderHelper DefineNestedType(
            string name,
            Type parent,
            params Type[] interfaces)
        {
            return new TypeBuilderHelper(
                this.assemblyBuilderHelper,
                this.typeBuilder.DefineNestedType(name, TypeAttributes.NestedPublic, parent, interfaces));
        }

        public TypeBuilderHelper DefineNestedType(
            string name,
            TypeAttributes attributes,
            Type parent,
            params Type[] interfaces)
        {
            return new TypeBuilderHelper(
                this.assemblyBuilderHelper,
                this.typeBuilder.DefineNestedType(name, attributes, parent, interfaces));
        }
        #endregion
    }
}