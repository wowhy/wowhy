namespace HY.Utitily.Reflection.Emit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Security;

    public class AssemblyBuilderHelper
    {
        private string filepath;
        private AssemblyName assemblyName;
        private Lazy<AssemblyBuilder> assemblyBuilder;
        private ModuleBuilder modelBuilder;
        private List<TypeBuilderHelper> defineTypes = new List<TypeBuilderHelper>();

        public AssemblyBuilderHelper(string filepath)
            : this(filepath, new Version(1, 0))
        {
        }

        public AssemblyBuilderHelper(string filepath, Version version)
        {
            CodeCheck.NotEmpty(filepath, "filepath");

            this.filepath = filepath;
            this.assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(filepath));
            this.assemblyName.Version = version;
            this.assemblyName.Flags |= AssemblyNameFlags.EnableJITcompileOptimizer;

            this.assemblyBuilder = new Lazy<AssemblyBuilder>(() =>
            {
                var dir = Path.GetDirectoryName(this.filepath);
                var builder = default(AssemblyBuilder);
                if (!string.IsNullOrEmpty(dir))
                {
                    builder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                        this.assemblyName,
                        AssemblyBuilderAccess.RunAndSave,
                        Path.GetDirectoryName(this.filepath));
                }
                else
                {
                    builder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                       this.assemblyName,
                       AssemblyBuilderAccess.RunAndSave);
                }

                builder.SetCustomAttribute(
                    new CustomAttributeBuilder(
                        typeof(AllowPartiallyTrustedCallersAttribute)
                            .GetConstructor(Type.EmptyTypes),
                        new object[0]));

                return builder;
            }, true);
        }

        public string FilePath { get { return this.filepath; } }

        public string ModuleName { get { return Path.GetFileName(this.filepath); } }

        public AssemblyName AssemblyName { get { return this.assemblyName; } }

        public AssemblyBuilder AssemblyBuilder { get { return this.assemblyBuilder.Value; } }

        public ModuleBuilder ModuleBuilder
        {
            get
            {
                if (this.modelBuilder == null)
                {
                    this.modelBuilder = AssemblyBuilder.DefineDynamicModule(this.ModuleName);
                }

                return this.modelBuilder;
            }
        }

        public ReadOnlyCollection<TypeBuilderHelper> DefineTypes
        {
            get
            {
                return this.defineTypes.AsReadOnly();
            }
        }

        public void Save()
        {
            this.AssemblyBuilder.Save(this.ModuleName);
        }

        public TypeBuilderHelper DefineType(string name)
        {
            var type = new TypeBuilderHelper(this, ModuleBuilder.DefineType(name));
            defineTypes.Add(type);
            return type;
        }

        public TypeBuilderHelper DefineType(string name, TypeAttributes attrs)
        {
            var type = new TypeBuilderHelper(this, ModuleBuilder.DefineType(name, attrs));
            defineTypes.Add(type);
            return type;
        }

        public TypeBuilderHelper DefineType(string name, Type parent)
        {
            var type = new TypeBuilderHelper(this, ModuleBuilder.DefineType(name, TypeAttributes.Public, parent));
            defineTypes.Add(type);
            return type;
        }

        public TypeBuilderHelper DefineType(string name, TypeAttributes attrs, Type parent)
        {
            var type = new TypeBuilderHelper(this, ModuleBuilder.DefineType(name, attrs, parent));
            defineTypes.Add(type);
            return type;
        }

        public TypeBuilderHelper DefineType(string name, Type parent, params Type[] interfaces)
        {
            var type = new TypeBuilderHelper(
                this,
                ModuleBuilder.DefineType(name, TypeAttributes.Public, parent, interfaces));
            defineTypes.Add(type);
            return type;
        }

        public TypeBuilderHelper DefineType(string name, TypeAttributes attrs, Type parent, params Type[] interfaces)
        {
            var type = new TypeBuilderHelper(
                this,
                ModuleBuilder.DefineType(name, attrs, parent, interfaces));
            defineTypes.Add(type);
            return type;
        }
    }
}