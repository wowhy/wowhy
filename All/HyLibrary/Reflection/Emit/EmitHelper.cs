namespace HyLibrary.Reflection.Emit
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.SymbolStore;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    public class EmitHelper
    {
        private ILGenerator il;

        public EmitHelper(ILGenerator il)
        {
            CodeCheck.NotNull(il, "il");

            this.il = il;
        }

        public ILGenerator IL
        {
            get { return this.il; }
        }

        #region ILGenerator Methods
        public EmitHelper BeginCatchBlock(Type exceptionType)
        {
            this.il.BeginCatchBlock(exceptionType); return this;
        }

        public EmitHelper BeginExceptFilterBlock()
        {
            this.il.BeginExceptFilterBlock(); return this;
        }

        public Label BeginExceptionBlock()
        {
            return this.il.BeginExceptionBlock();
        }

        public EmitHelper BeginFaultBlock()
        {
            this.il.BeginFaultBlock(); return this;
        }

        public EmitHelper BeginFinallyBlock()
        {
            this.il.BeginFinallyBlock(); return this;
        }

        public EmitHelper BeginScope()
        {
            this.il.BeginScope(); return this;
        }

        public LocalBuilder DeclareLocal(Type localType)
        {
            return this.il.DeclareLocal(localType);
        }

        public LocalBuilder DeclareLocal(Type localType, bool pinned)
        {
            return this.il.DeclareLocal(localType, pinned);
        }

        public Label DefineLabel()
        {
            return this.il.DefineLabel();
        }

        public EmitHelper EndExceptionBlock()
        {
            this.il.EndExceptionBlock(); return this;
        }

        public EmitHelper EndScope()
        {
            this.il.EndScope(); return this;
        }

        public EmitHelper MarkLabel(Label loc)
        {
            this.il.MarkLabel(loc); return this;
        }

        public EmitHelper MarkSequencePoint(
            ISymbolDocumentWriter document,
            int startLine,
            int startColumn,
            int endLine,
            int endColumn)
        {
            this.il.MarkSequencePoint(document, startLine, startColumn, endLine, endColumn);
            return this;
        }

        public EmitHelper ThrowException(Type exceptionType)
        {
            this.il.ThrowException(exceptionType); return this;
        }

        public EmitHelper UsingNamespace(string namespaceName)
        {
            this.il.UsingNamespace(namespaceName); return this;
        }
        #endregion

        #region Emit
        public EmitHelper add
        {
            get { this.il.Emit(OpCodes.Add); return this; }
        }

        public EmitHelper add_ovf
        {
            get { this.il.Emit(OpCodes.Add_Ovf); return this; }
        }

        public EmitHelper add_ovf_un
        {
            get { this.il.Emit(OpCodes.Add_Ovf_Un); return this; }
        }

        public EmitHelper and
        {
            get { this.il.Emit(OpCodes.And); return this; }
        }

        public EmitHelper arglist
        {
            get { this.il.Emit(OpCodes.Arglist); return this; }
        }

        public EmitHelper beq(Label label)
        {
            this.il.Emit(OpCodes.Beq, label); return this;
        }

        public EmitHelper beq_s(Label label)
        {
            this.il.Emit(OpCodes.Beq_S, label); return this;
        }

        public EmitHelper bge(Label label)
        {
            this.il.Emit(OpCodes.Bge, label); return this;
        }

        public EmitHelper bge_s(Label label)
        {
            this.il.Emit(OpCodes.Bge_S, label); return this;
        }

        public EmitHelper bge_un(Label label)
        {
            this.il.Emit(OpCodes.Bge_Un, label); return this;
        }

        public EmitHelper bge_un_s(Label label)
        {
            this.il.Emit(OpCodes.Bge_Un_S, label); return this;
        }

        public EmitHelper bgt(Label label)
        {
            this.il.Emit(OpCodes.Bgt, label); return this;
        }

        public EmitHelper bgt_s(Label label)
        {
            this.il.Emit(OpCodes.Bgt_S, label); return this;
        }

        public EmitHelper bgt_un(Label label)
        {
            this.il.Emit(OpCodes.Bgt_Un, label); return this;
        }

        public EmitHelper bgt_un_s(Label label)
        {
            this.il.Emit(OpCodes.Bgt_Un_S, label); return this;
        }

        public EmitHelper ble(Label label)
        {
            this.il.Emit(OpCodes.Ble, label); return this;
        }

        public EmitHelper ble_s(Label label)
        {
            this.il.Emit(OpCodes.Ble_S, label); return this;
        }

        public EmitHelper ble_un(Label label)
        {
            this.il.Emit(OpCodes.Ble_Un, label); return this;
        }

        public EmitHelper ble_un_s(Label label)
        {
            this.il.Emit(OpCodes.Ble_Un_S, label); return this;
        }

        public EmitHelper blt(Label label)
        {
            this.il.Emit(OpCodes.Blt, label); return this;
        }

        public EmitHelper blt_s(Label label)
        {
            this.il.Emit(OpCodes.Blt_S, label); return this;
        }

        public EmitHelper blt_un(Label label)
        {
            this.il.Emit(OpCodes.Blt_Un, label); return this;
        }

        public EmitHelper blt_un_s(Label label)
        {
            this.il.Emit(OpCodes.Blt_Un_S, label); return this;
        }

        public EmitHelper bne_un(Label label)
        {
            this.il.Emit(OpCodes.Bne_Un, label); return this;
        }

        public EmitHelper bne_un_s(Label label)
        {
            this.il.Emit(OpCodes.Bne_Un_S, label); return this;
        }

        public EmitHelper box(Type type)
        {
            this.il.Emit(OpCodes.Box, type); return this;
        }

        public EmitHelper boxIfValueType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return type.IsValueType ? box(type) : this;
        }

        public EmitHelper br(Label label)
        {
            this.il.Emit(OpCodes.Br, label); return this;
        }

        public EmitHelper @break
        {
            get { this.il.Emit(OpCodes.Break); return this; }
        }

        public EmitHelper brfalse(Label label)
        {
            this.il.Emit(OpCodes.Brfalse, label); return this;
        }

        public EmitHelper brfalse_s(Label label)
        {
            this.il.Emit(OpCodes.Brfalse_S, label); return this;
        }

        public EmitHelper brtrue(Label label)
        {
            this.il.Emit(OpCodes.Brtrue, label); return this;
        }

        public EmitHelper brtrue_s(Label label)
        {
            this.il.Emit(OpCodes.Brtrue_S, label); return this;
        }

        public EmitHelper br_s(Label label)
        {
            this.il.Emit(OpCodes.Br_S, label); return this;
        }

        public EmitHelper call(MethodInfo methodInfo)
        {
            this.il.Emit(OpCodes.Call, methodInfo); return this;
        }

        public EmitHelper call(ConstructorInfo constructorInfo)
        {
            this.il.Emit(OpCodes.Call, constructorInfo); return this;
        }

        public EmitHelper call(MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.il.EmitCall(OpCodes.Call, methodInfo, optionalParameterTypes); return this;
        }

        public EmitHelper call(Type type, string methodName, params Type[] optionalParameterTypes)
        {
            if (type == null) throw new ArgumentNullException("type");

            MethodInfo methodInfo = type.GetMethod(methodName, optionalParameterTypes);

            if (methodInfo == null)
                throw new ArgumentNullException("methodName", methodName);

            return call(methodInfo);
        }

        public EmitHelper call(Type type, string methodName, BindingFlags flags, params Type[] optionalParameterTypes)
        {
            if (type == null) throw new ArgumentNullException("type");

            MethodInfo methodInfo = type.GetMethod(methodName, flags, null, optionalParameterTypes, null);

            if (methodInfo == null)
                throw new ArgumentNullException("methodName", methodName);

            return call(methodInfo);
        }

        public EmitHelper calli(CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
        {
            this.il.EmitCalli(OpCodes.Calli, unmanagedCallConv, returnType, parameterTypes);
            return this;
        }

        public EmitHelper calli(CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
        {
            this.il.EmitCalli(OpCodes.Calli, callingConvention, returnType, parameterTypes, optionalParameterTypes);
            return this;
        }

        public EmitHelper callvirt(MethodInfo methodInfo)
        {
            this.il.Emit(OpCodes.Callvirt, methodInfo); return this;
        }

        public EmitHelper callvirt(MethodInfo methodInfo, Type[] optionalParameterTypes)
        {
            this.il.EmitCall(OpCodes.Callvirt, methodInfo, optionalParameterTypes); return this;
        }

        public EmitHelper callvirt(Type type, string methodName, params Type[] optionalParameterTypes)
        {
            if (type == null) throw new ArgumentNullException("type");

            MethodInfo methodInfo = type.GetMethod(methodName, optionalParameterTypes);

            if (methodInfo == null)
                throw new ArgumentNullException("methodName", methodName);

            return callvirt(methodInfo);
        }

        public EmitHelper callvirt(Type type, string methodName, BindingFlags flags, params Type[] optionalParameterTypes)
        {
            MethodInfo methodInfo =
                optionalParameterTypes == null ?
                    type.GetMethod(methodName, flags) :
                    type.GetMethod(methodName, flags, null, optionalParameterTypes, null);

            if (methodInfo == null)
                throw new ArgumentNullException("methodName", methodName);

            return callvirt(methodInfo, null);
        }

        public EmitHelper callvirt(Type type, string methodName, BindingFlags flags)
        {
            return callvirt(type, methodName, flags, null);
        }

        public EmitHelper callvirtNoGenerics(Type type, string methodName, params Type[] optionalParameterTypes)
        {
            MethodInfo methodInfo = type.GetMethod(
                methodName,
                BindingFlags.Instance | BindingFlags.Public,
                GenericBinder.NonGeneric,
                optionalParameterTypes, null);

            if (methodInfo == null)
                throw new ArgumentNullException("methodName", methodName);

            return callvirt(methodInfo);
        }

        public EmitHelper castclass(Type type)
        {
            this.il.Emit(OpCodes.Castclass, type); return this;
        }

        public EmitHelper castType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return type.IsValueType ? unbox_any(type) : castclass(type);
        }

        public EmitHelper ceq
        {
            get { this.il.Emit(OpCodes.Ceq); return this; }
        }

        public EmitHelper cgt
        {
            get { this.il.Emit(OpCodes.Cgt); return this; }
        }

        public EmitHelper cgt_un
        {
            get { this.il.Emit(OpCodes.Cgt_Un); return this; }
        }

        public EmitHelper constrained(Type type)
        {
            this.il.Emit(OpCodes.Constrained, type); return this;
        }

        public EmitHelper ckfinite
        {
            get { this.il.Emit(OpCodes.Ckfinite); return this; }
        }

        public EmitHelper clt
        {
            get { this.il.Emit(OpCodes.Clt); return this; }
        }

        public EmitHelper clt_un
        {
            get { this.il.Emit(OpCodes.Clt_Un); return this; }
        }

        public EmitHelper conv_i
        {
            get { this.il.Emit(OpCodes.Conv_I); return this; }
        }

        public EmitHelper conv_i1
        {
            get { this.il.Emit(OpCodes.Conv_I1); return this; }
        }

        public EmitHelper conv_i2
        {
            get { this.il.Emit(OpCodes.Conv_I2); return this; }
        }

        public EmitHelper conv_i4
        {
            get { this.il.Emit(OpCodes.Conv_I4); return this; }
        }

        public EmitHelper conv_i8
        {
            get { this.il.Emit(OpCodes.Conv_I8); return this; }
        }

        public EmitHelper conv(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.SByte: conv_i1.end(); break;
                case TypeCode.Int16: conv_i2.end(); break;
                case TypeCode.Int32: conv_i4.end(); break;
                case TypeCode.Int64: conv_i8.end(); break;

                case TypeCode.Byte: conv_u1.end(); break;
                case TypeCode.Char:
                case TypeCode.UInt16: conv_u2.end(); break;
                case TypeCode.UInt32: conv_u4.end(); break;
                case TypeCode.UInt64: conv_u8.end(); break;

                case TypeCode.Single: conv_r4.end(); break;
                case TypeCode.Double: conv_r8.end(); break;

                default:
                    {
                        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            ConstructorInfo ci = type.GetConstructor(type.GetGenericArguments());
                            if (ci != null)
                            {
                                newobj(ci);
                                break;
                            }
                        }

                        throw new ArgumentException(type.FullName);
                    }
            }

            return this;
        }

        public EmitHelper conv_ovf_i
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I); return this; }
        }

        public EmitHelper conv_ovf_i1
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I1); return this; }
        }

        public EmitHelper conv_ovf_i1_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I1_Un); return this; }
        }

        public EmitHelper conv_ovf_i2
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I2); return this; }
        }

        public EmitHelper conv_ovf_i2_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I2_Un); return this; }
        }

        public EmitHelper conv_ovf_i4
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I2_Un); return this; }
        }

        public EmitHelper conv_ovf_i4_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I4_Un); return this; }
        }

        public EmitHelper conv_ovf_i8
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I8); return this; }
        }

        public EmitHelper conv_ovf_i8_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I8_Un); return this; }
        }

        public EmitHelper conv_ovf_i_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_I_Un); return this; }
        }

        public EmitHelper conv_ovf_u
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U); return this; }
        }

        public EmitHelper conv_ovf_u1
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U1); return this; }
        }

        public EmitHelper conv_ovf_u1_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U1_Un); return this; }
        }

        public EmitHelper conv_ovf_u2
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U2); return this; }
        }

        public EmitHelper conv_ovf_u2_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U2_Un); return this; }
        }

        public EmitHelper conv_ovf_u4
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U4); return this; }
        }

        public EmitHelper conv_ovf_u4_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U4_Un); return this; }
        }

        public EmitHelper conv_ovf_u8
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U8); return this; }
        }

        public EmitHelper conv_ovf_u8_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U8_Un); return this; }
        }

        public EmitHelper conv_ovf_u_un
        {
            get { this.il.Emit(OpCodes.Conv_Ovf_U_Un); return this; }
        }

        public EmitHelper conv_r4
        {
            get { this.il.Emit(OpCodes.Conv_R4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Conv_R8"/>) that
        /// converts the value on top of the evaluation stack to float64.
        /// </summary>
        /// <seealso cref="OpCodes.Conv_R8">OpCodes.Conv_R8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper conv_r8
        {
            get { this.il.Emit(OpCodes.Conv_R8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Conv_R_Un"/>) that
        /// converts the unsigned integer value on top of the evaluation stack to float32.
        /// </summary>
        /// <seealso cref="OpCodes.Conv_R_Un">OpCodes.Conv_R_Un</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper conv_r_un
        {
            get { this.il.Emit(OpCodes.Conv_R_Un); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Conv_U"/>) that
        /// converts the value on top of the evaluation stack to unsigned natural int, and extends it to natural int.
        /// </summary>
        /// <seealso cref="OpCodes.Conv_U">OpCodes.Conv_U</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper conv_u
        {
            get { this.il.Emit(OpCodes.Conv_U); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Conv_U1"/>) that
        /// converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.
        /// </summary>
        /// <seealso cref="OpCodes.Conv_U1">OpCodes.Conv_U1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper conv_u1
        {
            get { this.il.Emit(OpCodes.Conv_U1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Conv_U2"/>) that
        /// converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.
        /// </summary>
        /// <seealso cref="OpCodes.Conv_U2">OpCodes.Conv_U2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper conv_u2
        {
            get { this.il.Emit(OpCodes.Conv_U2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Conv_U4"/>) that
        /// converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.
        /// </summary>
        /// <seealso cref="OpCodes.Conv_U4">OpCodes.Conv_U4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper conv_u4
        {
            get { this.il.Emit(OpCodes.Conv_U4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Conv_U8"/>) that
        /// converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.
        /// </summary>
        /// <seealso cref="OpCodes.Conv_U8">OpCodes.Conv_U8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper conv_u8
        {
            get { this.il.Emit(OpCodes.Conv_U8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Cpblk"/>) that
        /// copies a specified number bytes from a source address to a destination address.
        /// </summary>
        /// <seealso cref="OpCodes.Cpblk">OpCodes.Cpblk</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper cpblk
        {
            get { this.il.Emit(OpCodes.Cpblk); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Cpobj"/>, type) that
        /// copies the value type located at the address of an object (type &amp;, * or natural int) 
        /// to the address of the destination object (type &amp;, * or natural int).
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Cpobj">OpCodes.Cpobj</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper cpobj(Type type)
        {
            this.il.Emit(OpCodes.Cpobj, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Div"/>) that
        /// divides two values and pushes the result as a floating-point (type F) or
        /// quotient (type int32) onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Div">OpCodes.Div</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper div
        {
            get { this.il.Emit(OpCodes.Div); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Div_Un"/>) that
        /// divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Div_Un">OpCodes.Div_Un</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper div_un
        {
            get { this.il.Emit(OpCodes.Div_Un); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Dup"/>) that
        /// copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Dup">OpCodes.Dup</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper dup
        {
            get { this.il.Emit(OpCodes.Dup); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Endfilter"/>) that
        /// transfers control from the filter clause of an exception back to
        /// the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        /// <seealso cref="OpCodes.Endfilter">OpCodes.Endfilter</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper endfilter
        {
            get { this.il.Emit(OpCodes.Endfilter); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Endfinally"/>) that
        /// transfers control from the fault or finally clause of an exception block back to
        /// the Common Language Infrastructure (CLI) exception handler.
        /// </summary>
        /// <seealso cref="OpCodes.Endfinally">OpCodes.Endfinally</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper endfinally
        {
            get { this.il.Emit(OpCodes.Endfinally); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Initblk"/>) that
        /// initializes a specified block of memory at a specific address to a given size and initial value.
        /// </summary>
        /// <seealso cref="OpCodes.Initblk">OpCodes.Initblk</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper initblk
        {
            get { this.il.Emit(OpCodes.Initblk); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Initobj"/>, type) that
        /// initializes all the fields of the object at a specific address to a null reference or 
        /// a 0 of the appropriate primitive type.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Initobj">OpCodes.Initobj</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper initobj(Type type)
        {
            this.il.Emit(OpCodes.Initobj, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Isinst"/>, type) that
        /// tests whether an object reference (type O) is an instance of a particular class.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Isinst">OpCodes.Isinst</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper isinst(Type type)
        {
            this.il.Emit(OpCodes.Isinst, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Jmp"/>, methodInfo) that
        /// exits current method and jumps to specified method.
        /// </summary>
        /// <param name="methodInfo">The method to be jumped.</param>
        /// <seealso cref="OpCodes.Jmp">OpCodes.Jmp</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,MethodInfo)">ILGenerator.Emit</seealso>
        public EmitHelper jmp(MethodInfo methodInfo)
        {
            this.il.Emit(OpCodes.Jmp, methodInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarg"/>, short) that
        /// loads an argument (referenced by a specified index value) onto the stack.
        /// </summary>
        /// <param name="index">Index of the argument that is pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldarg">OpCodes.Ldarg</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper ldarg(short index)
        {
            this.il.Emit(OpCodes.Ldarg, index); return this;
        }

        /// <summary>
        /// Loads an argument onto the stack.
        /// </summary>
        /// <param name="parameterInfo">A <see cref="ParameterInfo"/> representing a parameter.</param>
        /// <param name="box">True, if parameter must be converted to a reference.</param>
        /// <seealso cref="ldarg(ParameterInfo)"/>
        public EmitHelper ldargEx(ParameterInfo parameterInfo, bool box)
        {
            ldarg(parameterInfo);

            Type type = parameterInfo.ParameterType;

            if (parameterInfo.ParameterType.IsByRef)
                type = parameterInfo.ParameterType.GetElementType();

            if (parameterInfo.ParameterType.IsByRef)
            {
                if (type.IsValueType && type.IsPrimitive == false)
                    ldobj(type);
                else
                    ldind(type);
            }

            if (box)
                boxIfValueType(type);

            return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarg"/>, short) or 
        /// ILGenerator.Emit(<see cref="OpCodes.Ldarg_S"/>, byte) that
        /// loads an argument (referenced by a specified index value) onto the stack.
        /// </summary>
        /// <param name="index">Index of the argument that is pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldarg">OpCodes.Ldarg</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper ldarg(int index)
        {
            switch (index)
            {
                case 0: ldarg_0.end(); break;
                case 1: ldarg_1.end(); break;
                case 2: ldarg_2.end(); break;
                case 3: ldarg_3.end(); break;
                default:
                    if (index <= byte.MaxValue) ldarg_s((byte)index);
                    else if (index <= short.MaxValue) ldarg((short)index);
                    else
                        throw new ArgumentOutOfRangeException("index");

                    break;
            }

            return this;
        }

        /// <summary>
        /// Loads an argument onto the stack.
        /// </summary>
        /// <param name="parameterInfo">A <see cref="ParameterInfo"/> representing a parameter.</param>
        public EmitHelper ldarg(ParameterInfo parameterInfo, bool isStatic = false)
        {
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            return ldarg(parameterInfo.Position + (isStatic ? 0 : 1));
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarga"/>, short) that
        /// load an argument address onto the evaluation stack.
        /// </summary>
        /// <param name="index">Index of the address addr of the argument that is pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldarga">OpCodes.Ldarga</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper ldarga(short index)
        {
            this.il.Emit(OpCodes.Ldarga, index); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarga_S"/>, byte) that
        /// load an argument address, in short form, onto the evaluation stack.
        /// </summary>
        /// <param name="index">Index of the address addr of the argument that is pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldarga_S">OpCodes.Ldarga_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,byte)">ILGenerator.Emit</seealso>
        public EmitHelper ldarga_s(byte index)
        {
            this.il.Emit(OpCodes.Ldarga_S, index); return this;
        }

        public EmitHelper ldarga_s(LocalBuilder local)
        {
            this.il.Emit(OpCodes.Ldarga_S, local); return this;
        }

        /// <summary>
        /// Load an argument address onto the evaluation stack.
        /// </summary>
        /// <param name="index">Index of the address addr of the argument that is pushed onto the stack.</param>
        public EmitHelper ldarga(int index)
        {
            if (index <= byte.MaxValue) ldarga_s((byte)index);
            else if (index <= short.MaxValue) ldarga((short)index);
            else
                throw new ArgumentOutOfRangeException("index");

            return this;
        }

        /// <summary>
        /// Loads an argument address onto the stack.
        /// </summary>
        /// <param name="parameterInfo">A <see cref="ParameterInfo"/> representing a parameter.</param>
        public EmitHelper ldarga(ParameterInfo parameterInfo, bool isStatic)
        {
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            return ldarga(parameterInfo.Position + (isStatic ? 0 : 1));
        }


        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarg_0"/>) that
        /// loads the argument at index 0 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldarg_0">OpCodes.Ldarg_0</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldarg_0
        {
            get { this.il.Emit(OpCodes.Ldarg_0); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarg_1"/>) that
        /// loads the argument at index 1 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldarg_1">OpCodes.Ldarg_1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldarg_1
        {
            get { this.il.Emit(OpCodes.Ldarg_1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarg_2"/>) that
        /// loads the argument at index 2 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldarg_2">OpCodes.Ldarg_2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldarg_2
        {
            get { this.il.Emit(OpCodes.Ldarg_2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarg_3"/>) that
        /// loads the argument at index 3 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldarg_3">OpCodes.Ldarg_3</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldarg_3
        {
            get { this.il.Emit(OpCodes.Ldarg_3); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldarg_S"/>, byte) that
        /// loads the argument (referenced by a specified short form index) onto the evaluation stack.
        /// </summary>
        /// <param name="index">Index of the argument value that is pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldarg_S">OpCodes.Ldarg_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,byte)">ILGenerator.Emit</seealso>
        public EmitHelper ldarg_s(byte index)
        {
            this.il.Emit(OpCodes.Ldarg_S, index); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_0"/> or <see cref="OpCodes.Ldc_I4_1"/>) that
        /// pushes a supplied value of type int32 onto the evaluation stack as an int32.
        /// </summary>
        /// <param name="b">The value pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldc_I4">OpCodes.Ldc_I4_0</seealso>
        /// <seealso cref="OpCodes.Ldc_I4">OpCodes.Ldc_I4_1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,int)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_bool(bool b)
        {
            this.il.Emit(b ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4"/>, int) that
        /// pushes a supplied value of type int32 onto the evaluation stack as an int32.
        /// </summary>
        /// <param name="num">The value pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldc_I4">OpCodes.Ldc_I4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,int)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4(int num)
        {
            this.il.Emit(OpCodes.Ldc_I4, num); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_0"/>) that
        /// pushes the integer value of 0 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_0">OpCodes.Ldc_I4_0</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_0
        {
            get { this.il.Emit(OpCodes.Ldc_I4_0); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_1"/>) that
        /// pushes the integer value of 1 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_1">OpCodes.Ldc_I4_1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_1
        {
            get { this.il.Emit(OpCodes.Ldc_I4_1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_2"/>) that
        /// pushes the integer value of 2 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_2">OpCodes.Ldc_I4_2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_2
        {
            get { this.il.Emit(OpCodes.Ldc_I4_2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_3"/>) that
        /// pushes the integer value of 3 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_3">OpCodes.Ldc_I4_3</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_3
        {
            get { this.il.Emit(OpCodes.Ldc_I4_3); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_4"/>) that
        /// pushes the integer value of 4 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_4">OpCodes.Ldc_I4_4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_4
        {
            get { this.il.Emit(OpCodes.Ldc_I4_4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_5"/>) that
        /// pushes the integer value of 5 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_5">OpCodes.Ldc_I4_0</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_5
        {
            get { this.il.Emit(OpCodes.Ldc_I4_5); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_6"/>) that
        /// pushes the integer value of 6 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_6">OpCodes.Ldc_I4_6</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_6
        {
            get { this.il.Emit(OpCodes.Ldc_I4_6); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_7"/>) that
        /// pushes the integer value of 7 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_7">OpCodes.Ldc_I4_7</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_7
        {
            get { this.il.Emit(OpCodes.Ldc_I4_7); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_8"/>) that
        /// pushes the integer value of 8 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_8">OpCodes.Ldc_I4_8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_8
        {
            get { this.il.Emit(OpCodes.Ldc_I4_8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_M1"/>) that
        /// pushes the integer value of -1 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_M1">OpCodes.Ldc_I4_M1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_m1
        {
            get { this.il.Emit(OpCodes.Ldc_I4_M1); return this; }
        }

        /// <summary>
        /// Calls the best form of ILGenerator.Emit(Ldc_I4_X) that
        /// pushes the integer value of -1 onto the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="ldc_i4"/>
        public EmitHelper ldc_i4_(int num)
        {
            switch (num)
            {
                case -1: ldc_i4_m1.end(); break;
                case 0: ldc_i4_0.end(); break;
                case 1: ldc_i4_1.end(); break;
                case 2: ldc_i4_2.end(); break;
                case 3: ldc_i4_3.end(); break;
                case 4: ldc_i4_4.end(); break;
                case 5: ldc_i4_5.end(); break;
                case 6: ldc_i4_6.end(); break;
                case 7: ldc_i4_7.end(); break;
                case 8: ldc_i4_8.end(); break;
                default:
                    if (num >= sbyte.MinValue && num <= sbyte.MaxValue)
                        ldc_i4_s((sbyte)num);
                    else
                        ldc_i4(num);

                    break;
            }

            return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I4_S"/>, byte) that
        /// pushes the supplied int8 value onto the evaluation stack as an int32, short form.
        /// </summary>
        /// <param name="num">The value pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldc_I4_S">OpCodes.Ldc_I4_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,byte)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i4_s(sbyte num)
        {
            this.il.Emit(OpCodes.Ldc_I4_S, num); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_I8"/>, long) that
        /// pushes a supplied value of type int64 onto the evaluation stack as an int64.
        /// </summary>
        /// <param name="num">The value pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldc_I8">OpCodes.Ldc_I8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,long)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_i8(long num)
        {
            this.il.Emit(OpCodes.Ldc_I8, num); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_R4"/>, float) that
        /// pushes a supplied value of type float32 onto the evaluation stack as type F (float).
        /// </summary>
        /// <param name="num">The value pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldc_R4">OpCodes.Ldc_R4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,float)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_r4(float num)
        {
            this.il.Emit(OpCodes.Ldc_R4, num); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldc_R8"/>, double) that
        /// pushes a supplied value of type float64 onto the evaluation stack as type F (float).
        /// </summary>
        /// <param name="num">The value pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldc_R8">OpCodes.Ldc_R8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,double)">ILGenerator.Emit</seealso>
        public EmitHelper ldc_r8(double num)
        {
            this.il.Emit(OpCodes.Ldc_R8, num); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelema"/>, type) that
        /// loads the address of the array element at a specified array index onto the top of the evaluation stack 
        /// as type &amp; (managed pointer).
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Ldelema">OpCodes.Ldelema</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper ldelema(Type type)
        {
            this.il.Emit(OpCodes.Ldelema, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_I"/>) that
        /// loads the element with type natural int at a specified array index onto the top of the evaluation stack 
        /// as a natural int.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_I">OpCodes.Ldelem_I</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_i
        {
            get { this.il.Emit(OpCodes.Ldelem_I); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_I1"/>) that
        /// loads the element with type int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_I1">OpCodes.Ldelem_I1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_i1
        {
            get { this.il.Emit(OpCodes.Ldelem_I1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_I2"/>) that
        /// loads the element with type int16 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_I2">OpCodes.Ldelem_I2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_i2
        {
            get { this.il.Emit(OpCodes.Ldelem_I2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_I4"/>) that
        /// loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_I4">OpCodes.Ldelem_I4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_i4
        {
            get { this.il.Emit(OpCodes.Ldelem_I4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_I8"/>) that
        /// loads the element with type int64 at a specified array index onto the top of the evaluation stack as an int64.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_I8">OpCodes.Ldelem_I8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_i8
        {
            get { this.il.Emit(OpCodes.Ldelem_I8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_R4"/>) that
        /// loads the element with type float32 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_R4">OpCodes.Ldelem_R4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_r4
        {
            get { this.il.Emit(OpCodes.Ldelem_R4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_R8"/>) that
        /// loads the element with type float64 at a specified array index onto the top of the evaluation stack as type F (float).
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_R8">OpCodes.Ldelem_R8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_r8
        {
            get { this.il.Emit(OpCodes.Ldelem_R8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_Ref"/>) that
        /// loads the element containing an object reference at a specified array index 
        /// onto the top of the evaluation stack as type O (object reference).
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_Ref">OpCodes.Ldelem_Ref</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_ref
        {
            get { this.il.Emit(OpCodes.Ldelem_Ref); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_U1"/>) that
        /// loads the element with type unsigned int8 at a specified array index onto the top of the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_U1">OpCodes.Ldelem_U1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_u1
        {
            get { this.il.Emit(OpCodes.Ldelem_U1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_U2"/>) that
        /// loads the element with type unsigned int16 at a specified array index 
        /// onto the top of the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_U2">OpCodes.Ldelem_U2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_u2
        {
            get { this.il.Emit(OpCodes.Ldelem_U2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldelem_U4"/>) that
        /// loads the element with type unsigned int32 at a specified array index 
        /// onto the top of the evaluation stack as an int32.
        /// </summary>
        /// <seealso cref="OpCodes.Ldelem_U4">OpCodes.Ldelem_U4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldelem_u4
        {
            get { this.il.Emit(OpCodes.Ldelem_U4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldfld"/>, fieldInfo) that
        /// finds the value of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        /// <param name="fieldInfo">A <see cref="FieldInfo"/> representing a field.</param>
        /// <seealso cref="OpCodes.Ldfld">OpCodes.Ldfld</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldfld(FieldInfo fieldInfo)
        {
            this.il.Emit(OpCodes.Ldfld, fieldInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldflda"/>, fieldInfo) that
        /// finds the address of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        /// <param name="fieldInfo">A <see cref="FieldInfo"/> representing a field.</param>
        /// <seealso cref="OpCodes.Ldflda">OpCodes.Ldflda</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldflda(FieldInfo fieldInfo)
        {
            this.il.Emit(OpCodes.Ldflda, fieldInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldftn"/>, methodInfo) that
        /// pushes an unmanaged pointer (type natural int) to the native code implementing a specific method 
        /// onto the evaluation stack.
        /// </summary>
        /// <param name="methodInfo">The method to be called.</param>
        /// <seealso cref="OpCodes.Ldftn">OpCodes.Ldftn</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,MethodInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldftn(MethodInfo methodInfo)
        {
            this.il.Emit(OpCodes.Ldftn, methodInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_I"/>) that
        /// loads a value of type natural int as a natural int onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_I">OpCodes.Ldind_I</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_i
        {
            get { this.il.Emit(OpCodes.Ldind_I); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_I1"/>) that
        /// loads a value of type int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_I1">OpCodes.Ldind_I1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_i1
        {
            get { this.il.Emit(OpCodes.Ldind_I1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_I2"/>) that
        /// loads a value of type int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_I2">OpCodes.Ldind_I2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_i2
        {
            get { this.il.Emit(OpCodes.Ldind_I2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_I4"/>) that
        /// loads a value of type int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_I4">OpCodes.Ldind_I4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_i4
        {
            get { this.il.Emit(OpCodes.Ldind_I4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_I8"/>) that
        /// loads a value of type int64 as an int64 onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_I8">OpCodes.Ldind_I8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_i8
        {
            get { this.il.Emit(OpCodes.Ldind_I8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_R4"/>) that
        /// loads a value of type float32 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_R4">OpCodes.Ldind_R4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_r4
        {
            get { this.il.Emit(OpCodes.Ldind_R4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_R8"/>) that
        /// loads a value of type float64 as a type F (float) onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_R8">OpCodes.Ldind_R8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_r8
        {
            get { this.il.Emit(OpCodes.Ldind_R8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_Ref"/>) that
        /// loads an object reference as a type O (object reference) onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_Ref">OpCodes.Ldind_Ref</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_ref
        {
            get { this.il.Emit(OpCodes.Ldind_Ref); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_U1"/>) that
        /// loads a value of type unsigned int8 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_U1">OpCodes.Ldind_U1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_u1
        {
            get { this.il.Emit(OpCodes.Ldind_U1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_U2"/>) that
        /// loads a value of type unsigned int16 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_U2">OpCodes.Ldind_U2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_u2
        {
            get { this.il.Emit(OpCodes.Ldind_U2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldind_U4"/>) that
        /// loads a value of type unsigned int32 as an int32 onto the evaluation stack indirectly.
        /// </summary>
        /// <seealso cref="OpCodes.Ldind_U4">OpCodes.Ldind_U4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldind_u4
        {
            get { this.il.Emit(OpCodes.Ldind_U4); return this; }
        }

        /// <summary>
        /// Loads a value of the type from a supplied address.
        /// </summary>
        /// <param name="type">A Type.</param>
        public EmitHelper ldind(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte: ldind_i1.end(); break;

                case TypeCode.Char:
                case TypeCode.Int16:
                case TypeCode.UInt16: ldind_i2.end(); break;

                case TypeCode.Int32:
                case TypeCode.UInt32: ldind_i4.end(); break;

                case TypeCode.Int64:
                case TypeCode.UInt64: ldind_i8.end(); break;

                case TypeCode.Single: ldind_r4.end(); break;
                case TypeCode.Double: ldind_r8.end(); break;

                default:
                    if (type.IsClass)
                        ldind_ref.end();
                    else if (type.IsValueType)
                        stobj(type);
                    else
                        throw new ArgumentException(type.FullName);
                    break;
            }

            return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldlen"/>) that
        /// pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldlen">OpCodes.Ldlen</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldlen
        {
            get { this.il.Emit(OpCodes.Ldlen); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloc"/>, short) that
        /// load an argument address onto the evaluation stack.
        /// </summary>
        /// <param name="index">Index of the local variable value pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Ldloc">OpCodes.Ldloc</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper ldloc(short index)
        {
            this.il.Emit(OpCodes.Ldloc, index); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloc"/>, <see cref="LocalBuilder"/>) that
        /// load an argument address onto the evaluation stack.
        /// </summary>
        /// <param name="localBuilder">Local variable builder.</param>
        /// <seealso cref="OpCodes.Ldloc">OpCodes.Ldloc</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper ldloc(LocalBuilder localBuilder)
        {
            this.il.Emit(OpCodes.Ldloc, localBuilder); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloca"/>, short) that
        /// loads the address of the local variable at a specific index onto the evaluation stack.
        /// </summary>
        /// <param name="index">Index of the local variable.</param>
        /// <seealso cref="OpCodes.Ldloca">OpCodes.Ldloca</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper ldloca(short index)
        {
            this.il.Emit(OpCodes.Ldloca, index); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloca_S"/>, byte) that
        /// loads the address of the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        /// <param name="index">Index of the local variable.</param>
        /// <seealso cref="OpCodes.Ldloca_S">OpCodes.Ldloca_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,byte)">ILGenerator.Emit</seealso>
        public EmitHelper ldloca_s(byte index)
        {
            this.il.Emit(OpCodes.Ldloca_S, index); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloca_S"/>, <see cref="LocalBuilder"/>) that
        /// loads the address of the local variable at a specific index onto the evaluation stack.
        /// </summary>
        /// <param name="local">A <see cref="LocalBuilder"/> representing the local variable.</param>
        /// <seealso cref="OpCodes.Ldloca_S">OpCodes.Ldloca</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,LocalBuilder)">ILGenerator.Emit</seealso>
        public EmitHelper ldloca_s(LocalBuilder local)
        {
            this.il.Emit(OpCodes.Ldloca_S, local); return this;
        }


        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloca"/>, <see cref="LocalBuilder"/>) that
        /// loads the address of the local variable at a specific index onto the evaluation stack.
        /// </summary>
        /// <param name="local">A <see cref="LocalBuilder"/> representing the local variable.</param>
        /// <seealso cref="OpCodes.Ldloca">OpCodes.Ldloca</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper ldloca(LocalBuilder local)
        {
            this.il.Emit(OpCodes.Ldloca, local); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloc_0"/>) that
        /// loads the local variable at index 0 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldloc_0">OpCodes.Ldloc_0</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldloc_0
        {
            get { this.il.Emit(OpCodes.Ldloc_0); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloc_1"/>) that
        /// loads the local variable at index 1 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldloc_1">OpCodes.Ldloc_1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldloc_1
        {
            get { this.il.Emit(OpCodes.Ldloc_1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloc_2"/>) that
        /// loads the local variable at index 2 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldloc_2">OpCodes.Ldloc_2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldloc_2
        {
            get { this.il.Emit(OpCodes.Ldloc_2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloc_3"/>) that
        /// loads the local variable at index 3 onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldloc_3">OpCodes.Ldloc_3</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldloc_3
        {
            get { this.il.Emit(OpCodes.Ldloc_3); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldloc_S"/>, byte) that
        /// loads the local variable at a specific index onto the evaluation stack, short form.
        /// </summary>
        /// <param name="index">Index of the local variable.</param>
        /// <seealso cref="OpCodes.Ldloc_S">OpCodes.Ldloc_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,byte)">ILGenerator.Emit</seealso>
        public EmitHelper ldloc_s(byte index)
        {
            this.il.Emit(OpCodes.Ldloca_S, index); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldnull"/>) that
        /// pushes a null reference (type O) onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldnull">OpCodes.Ldnull</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ldnull
        {
            get { this.il.Emit(OpCodes.Ldnull); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldobj"/>, type) that
        /// copies the value type object pointed to by an address to the top of the evaluation stack.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Ldobj">OpCodes.Ldobj</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper ldobj(Type type)
        {
            this.il.Emit(OpCodes.Ldobj, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldsfld"/>, fieldInfo) that
        /// pushes the value of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="fieldInfo">A <see cref="FieldInfo"/> representing a field.</param>
        /// <seealso cref="OpCodes.Ldsfld">OpCodes.Ldsfld</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldsfld(FieldInfo fieldInfo)
        {
            this.il.Emit(OpCodes.Ldsfld, fieldInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldsflda"/>, fieldInfo) that
        /// pushes the address of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="fieldInfo">A <see cref="FieldInfo"/> representing a field.</param>
        /// <seealso cref="OpCodes.Ldsflda">OpCodes.Ldsflda</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldsflda(FieldInfo fieldInfo)
        {
            this.il.Emit(OpCodes.Ldsflda, fieldInfo); return this;
        }

        /// <summary>
        /// Calls <see cref="ldstr"/> -or- <see cref="ldnull"/>,
        /// if given string is a null reference.
        /// </summary>
        /// <param name="str">The String to be emitted.</param>
        /// <seealso cref="ldstr"/>
        /// <seealso cref="ldnull"/>
        public EmitHelper ldstrEx(string str)
        {
            return str == null ? ldnull : ldstr(str);
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldstr"/>, string) that
        /// pushes a new object reference to a string literal stored in the metadata.
        /// </summary>
        /// <param name="str">The String to be emitted.</param>
        /// <seealso cref="OpCodes.Ldstr">OpCodes.Ldstr</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldstr(string str)
        {
            this.il.Emit(OpCodes.Ldstr, str); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldtoken"/>, methodInfo) that
        /// converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
        /// </summary>
        /// <param name="methodInfo">The method to be called.</param>
        /// <seealso cref="OpCodes.Ldtoken">OpCodes.Ldtoken</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,MethodInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldtoken(MethodInfo methodInfo)
        {
            this.il.Emit(OpCodes.Ldtoken, methodInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldtoken"/>, fieldInfo) that
        /// converts a metadata token to its runtime representation, 
        /// pushing it onto the evaluation stack.
        /// </summary>
        /// <param name="fieldInfo">A <see cref="FieldInfo"/> representing a field.</param>
        /// <seealso cref="OpCodes.Ldtoken">OpCodes.Ldtoken</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldtoken(FieldInfo fieldInfo)
        {
            this.il.Emit(OpCodes.Ldtoken, fieldInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldtoken"/>, type) that
        /// converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Ldtoken">OpCodes.Ldtoken</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper ldtoken(Type type)
        {
            this.il.Emit(OpCodes.Ldtoken, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ldvirtftn"/>, methodInfo) that
        /// pushes an unmanaged pointer (type natural int) to the native code implementing a particular virtual method 
        /// associated with a specified object onto the evaluation stack.
        /// </summary>
        /// <param name="methodInfo">The method to be called.</param>
        /// <seealso cref="OpCodes.Ldvirtftn">OpCodes.Ldvirtftn</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,MethodInfo)">ILGenerator.Emit</seealso>
        public EmitHelper ldvirtftn(MethodInfo methodInfo)
        {
            this.il.Emit(OpCodes.Ldvirtftn, methodInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Leave"/>, label) that
        /// exits a protected region of code, unconditionally tranferring control to a specific target instruction.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <seealso cref="OpCodes.Leave">OpCodes.Leave</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Label)">ILGenerator.Emit</seealso>
        public EmitHelper leave(Label label)
        {
            this.il.Emit(OpCodes.Leave, label); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Leave_S"/>, label) that
        /// exits a protected region of code, unconditionally transferring control to a target instruction (short form).
        /// </summary>
        /// <param name="label">The label.</param>
        /// <seealso cref="OpCodes.Leave_S">OpCodes.Leave_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Label)">ILGenerator.Emit</seealso>
        public EmitHelper leave_s(Label label)
        {
            this.il.Emit(OpCodes.Leave_S, label); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Localloc"/>) that
        /// allocates a certain number of bytes from the local dynamic memory pool and pushes the address 
        /// (a transient pointer, type *) of the first allocated byte onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Localloc">OpCodes.Localloc</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper localloc
        {
            get { this.il.Emit(OpCodes.Localloc); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Mkrefany"/>, type) that
        /// pushes a typed reference to an instance of a specific type onto the evaluation stack.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Mkrefany">OpCodes.Mkrefany</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper mkrefany(Type type)
        {
            this.il.Emit(OpCodes.Mkrefany, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Mul"/>) that
        /// multiplies two values and pushes the result on the evaluation stack.
        /// (a transient pointer, type *) of the first allocated byte onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Mul">OpCodes.Mul</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper mul
        {
            get { this.il.Emit(OpCodes.Mul); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Mul_Ovf"/>) that
        /// multiplies two integer values, performs an overflow check, 
        /// and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Mul_Ovf">OpCodes.Mul_Ovf</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper mul_ovf
        {
            get { this.il.Emit(OpCodes.Mul_Ovf); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Mul_Ovf_Un"/>) that
        /// multiplies two unsigned integer values, performs an overflow check, 
        /// and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Mul_Ovf_Un">OpCodes.Mul_Ovf_Un</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper mul_ovf_un
        {
            get { this.il.Emit(OpCodes.Mul_Ovf_Un); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Neg"/>) that
        /// negates a value and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Neg">OpCodes.Neg</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper neg
        {
            get { this.il.Emit(OpCodes.Neg); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Newarr"/>, type) that
        /// pushes an object reference to a new zero-based, one-dimensional array whose elements 
        /// are of a specific type onto the evaluation stack.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Newarr">OpCodes.Newarr</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper newarr(Type type)
        {
            this.il.Emit(OpCodes.Newarr, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Newobj"/>, <see cref="ConstructorInfo"/>) that
        /// creates a new object or a new instance of a value type,
        /// pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        /// <param name="constructorInfo">A <see cref="ConstructorInfo"/> representing a constructor.</param>
        /// <seealso cref="OpCodes.Newobj">OpCodes.Newobj</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,ConstructorInfo)">ILGenerator.Emit</seealso>
        public EmitHelper newobj(ConstructorInfo constructorInfo)
        {
            this.il.Emit(OpCodes.Newobj, constructorInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Newobj"/>, ConstructorInfo) that
        /// creates a new object or a new instance of a value type,
        /// pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        /// <param name="type">A type.</param>
        /// <param name="parameters">An array of System.Type objects representing
        /// the number, order, and type of the parameters for the desired constructor.
        /// -or- An empty array of System.Type objects, to get a constructor that takes
        /// no parameters. Such an empty array is provided by the static field System.Type.EmptyTypes.</param>
        public EmitHelper newobj(Type type, params Type[] parameters)
        {
            if (type == null) throw new ArgumentNullException("type");

            ConstructorInfo ci = type.GetConstructor(parameters);

            return newobj(ci);
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Nop"/>) that
        /// fills space if opcodes are patched. No meaningful operation is performed although 
        /// a processing cycle can be consumed.
        /// </summary>
        /// <seealso cref="OpCodes.Nop">OpCodes.Nop</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper nop
        {
            get { this.il.Emit(OpCodes.Nop); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Not"/>) that
        /// computes the bitwise complement of the integer value on top of the stack 
        /// and pushes the result onto the evaluation stack as the same type.
        /// </summary>
        /// <seealso cref="OpCodes.Not">OpCodes.Not</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper not
        {
            get { this.il.Emit(OpCodes.Not); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Or"/>) that
        /// compute the bitwise complement of the two integer values on top of the stack and 
        /// pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Or">OpCodes.Or</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper or
        {
            get { this.il.Emit(OpCodes.Or); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Pop"/>) that
        /// removes the value currently on top of the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Pop">OpCodes.Pop</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper pop
        {
            get { this.il.Emit(OpCodes.Pop); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Refanytype"/>) that
        /// specifies that the subsequent array address operation performs
        /// no type check at run time, and that it returns a managed pointer
        /// whose mutability is restricted.
        /// </summary>
        /// <seealso cref="OpCodes.Refanytype">OpCodes.Refanytype</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper @readonly
        {
            get { this.il.Emit(OpCodes.Readonly); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Refanytype"/>) that
        /// retrieves the type token embedded in a typed reference.
        /// </summary>
        /// <seealso cref="OpCodes.Refanytype">OpCodes.Refanytype</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper refanytype
        {
            get { this.il.Emit(OpCodes.Refanytype); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Refanyval"/>, type) that
        /// retrieves the address (type &amp;) embedded in a typed reference.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Refanyval">OpCodes.Refanyval</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper refanyval(Type type)
        {
            this.il.Emit(OpCodes.Refanyval, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Rem"/>) that
        /// divides two values and pushes the remainder onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Rem">OpCodes.Rem</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper rem
        {
            get { this.il.Emit(OpCodes.Rem); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Rem_Un"/>) that
        /// divides two unsigned values and pushes the remainder onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Rem_Un">OpCodes.Rem_Un</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper rem_un
        {
            get { this.il.Emit(OpCodes.Rem_Un); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Ret"/>) that
        /// returns from the current method, pushing a return value (if present) 
        /// from the caller's evaluation stack onto the callee's evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ret">OpCodes.Ret</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper ret()
        {
            this.il.Emit(OpCodes.Ret); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Rethrow"/>) that
        /// rethrows the current exception.
        /// </summary>
        /// <seealso cref="OpCodes.Rethrow">OpCodes.Rethrow</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper rethrow
        {
            get { this.il.Emit(OpCodes.Rethrow); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Shl"/>) that
        /// shifts an integer value to the left (in zeroes) by a specified number of bits,
        /// pushing the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Shl">OpCodes.Shl</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper shl
        {
            get { this.il.Emit(OpCodes.Shl); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Shr"/>) that
        /// shifts an integer value (in sign) to the right by a specified number of bits,
        /// pushing the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Shr">OpCodes.Shr</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper shr
        {
            get { this.il.Emit(OpCodes.Shr); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Shr_Un"/>) that
        /// shifts an unsigned integer value (in zeroes) to the right by a specified number of bits,
        /// pushing the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Shr_Un">OpCodes.Shr_Un</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper shr_un
        {
            get { this.il.Emit(OpCodes.Shr_Un); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Sizeof"/>, type) that
        /// pushes the size, in bytes, of a supplied value type onto the evaluation stack.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Sizeof">OpCodes.Sizeof</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper @sizeof(Type type)
        {
            this.il.Emit(OpCodes.Sizeof, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Starg"/>, short) that
        /// stores the value on top of the evaluation stack in the argument slot at a specified index.
        /// </summary>
        /// <param name="index">Slot index.</param>
        /// <seealso cref="OpCodes.Starg">OpCodes.Starg</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper starg(short index)
        {
            this.il.Emit(OpCodes.Starg, index); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Starg_S"/>, byte) that
        /// stores the value on top of the evaluation stack in the argument slot at a specified index,
        /// short form.
        /// </summary>
        /// <param name="index">Slot index.</param>
        /// <seealso cref="OpCodes.Starg_S">OpCodes.Starg_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,byte)">ILGenerator.Emit</seealso>
        public EmitHelper starg_s(byte index)
        {
            this.il.Emit(OpCodes.Starg_S, index); return this;
        }

        /// <summary>
        /// Stores the value on top of the evaluation stack in the argument slot at a specified index.
        /// </summary>
        /// <param name="index">Slot index.</param>
        /// <seealso cref="OpCodes.Starg">OpCodes.Starg</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper starg(int index)
        {
            if (index < byte.MaxValue) starg_s((byte)index);
            else if (index < short.MaxValue) starg((short)index);
            else
                throw new ArgumentOutOfRangeException("index");

            return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_I"/>) that
        /// replaces the array element at a given index with the natural int value 
        /// on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_I">OpCodes.Stelem_I</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_i
        {
            get { this.il.Emit(OpCodes.Stelem_I); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_I1"/>) that
        /// replaces the array element at a given index with the int8 value on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_I1">OpCodes.Stelem_I1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_i1
        {
            get { this.il.Emit(OpCodes.Stelem_I1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_I2"/>) that
        /// replaces the array element at a given index with the int16 value on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_I2">OpCodes.Stelem_I2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_i2
        {
            get { this.il.Emit(OpCodes.Stelem_I2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_I4"/>) that
        /// replaces the array element at a given index with the int32 value on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_I4">OpCodes.Stelem_I4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_i4
        {
            get { this.il.Emit(OpCodes.Stelem_I4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_I8"/>) that
        /// replaces the array element at a given index with the int64 value on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_I8">OpCodes.Stelem_I8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_i8
        {
            get { this.il.Emit(OpCodes.Stelem_I8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_R4"/>) that
        /// replaces the array element at a given index with the float32 value on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_R4">OpCodes.Stelem_R4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_r4
        {
            get { this.il.Emit(OpCodes.Stelem_R4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_R8"/>) that
        /// replaces the array element at a given index with the float64 value on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_R8">OpCodes.Stelem_R8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_r8
        {
            get { this.il.Emit(OpCodes.Stelem_R8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stelem_Ref"/>) that
        /// replaces the array element at a given index with the object ref value (type O)
        /// on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Stelem_Ref">OpCodes.Stelem_Ref</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stelem_ref
        {
            get { this.il.Emit(OpCodes.Stelem_Ref); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stfld"/>, <see cref="FieldInfo"/>) that
        /// replaces the value stored in the field of an object reference or pointer with a new value.
        /// </summary>
        /// <param name="fieldInfo">A <see cref="FieldInfo"/> representing a field.</param>
        /// <seealso cref="OpCodes.Stfld">OpCodes.Stfld</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper stfld(FieldInfo fieldInfo)
        {
            this.il.Emit(OpCodes.Stfld, fieldInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_I"/>) that
        /// stores a value of type natural int at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_I">OpCodes.Stind_I</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_i
        {
            get { this.il.Emit(OpCodes.Stind_I); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_I1"/>) that
        /// stores a value of type int8 at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_I1">OpCodes.Stind_I1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_i1
        {
            get { this.il.Emit(OpCodes.Stind_I1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_I2"/>) that
        /// stores a value of type int16 at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_I2">OpCodes.Stind_I2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_i2
        {
            get { this.il.Emit(OpCodes.Stind_I2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_I4"/>) that
        /// stores a value of type int32 at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_I4">OpCodes.Stind_I4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_i4
        {
            get { this.il.Emit(OpCodes.Stind_I4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_I8"/>) that
        /// stores a value of type int64 at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_I8">OpCodes.Stind_I8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_i8
        {
            get { this.il.Emit(OpCodes.Stind_I8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_R4"/>) that
        /// stores a value of type float32 at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_R4">OpCodes.Stind_R4</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_r4
        {
            get { this.il.Emit(OpCodes.Stind_R4); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_R8"/>) that
        /// stores a value of type float64 at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_R8">OpCodes.Stind_R8</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_r8
        {
            get { this.il.Emit(OpCodes.Stind_R8); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stind_Ref"/>) that
        /// stores an object reference value at a supplied address.
        /// </summary>
        /// <seealso cref="OpCodes.Stind_Ref">OpCodes.Stind_Ref</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stind_ref
        {
            get { this.il.Emit(OpCodes.Stind_Ref); return this; }
        }

        /// <summary>
        /// Stores a value of the type at a supplied address.
        /// </summary>
        /// <param name="type">A Type.</param>
        public EmitHelper stind(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte: stind_i1.end(); break;

                case TypeCode.Char:
                case TypeCode.Int16:
                case TypeCode.UInt16: stind_i2.end(); break;

                case TypeCode.Int32:
                case TypeCode.UInt32: stind_i4.end(); break;

                case TypeCode.Int64:
                case TypeCode.UInt64: stind_i8.end(); break;

                case TypeCode.Single: stind_r4.end(); break;
                case TypeCode.Double: stind_r8.end(); break;

                default:
                    if (type.IsClass)
                        stind_ref.end();
                    else if (type.IsValueType)
                        stobj(type);
                    else
                        throw new ArgumentException(type.FullName);
                    break;
            }

            return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc"/>, <see cref="LocalBuilder"/>) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at a specified index.
        /// </summary>
        /// <param name="local">A local variable.</param>
        /// <seealso cref="OpCodes.Stloc">OpCodes.Stloc</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,LocalBuilder)">ILGenerator.Emit</seealso>
        public EmitHelper stloc(LocalBuilder local)
        {
            this.il.Emit(OpCodes.Stloc, local); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc"/>, short) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at a specified index.
        /// </summary>
        /// <param name="index">A local variable index.</param>
        /// <seealso cref="OpCodes.Stloc">OpCodes.Stloc</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper stloc(short index)
        {
            if (index >= byte.MinValue && index <= byte.MaxValue)
                return stloc_s((byte)index);

            this.il.Emit(OpCodes.Stloc, index);
            return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc_0"/>) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at index 0.
        /// </summary>
        /// <seealso cref="OpCodes.Stloc_0">OpCodes.Stloc_0</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stloc_0
        {
            get { this.il.Emit(OpCodes.Stloc_0); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc_1"/>) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at index 1.
        /// </summary>
        /// <seealso cref="OpCodes.Stloc_1">OpCodes.Stloc_1</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stloc_1
        {
            get { this.il.Emit(OpCodes.Stloc_1); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc_2"/>) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at index 2.
        /// </summary>
        /// <seealso cref="OpCodes.Stloc_2">OpCodes.Stloc_2</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stloc_2
        {
            get { this.il.Emit(OpCodes.Stloc_2); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc_3"/>) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at index 3.
        /// </summary>
        /// <seealso cref="OpCodes.Stloc_3">OpCodes.Stloc_3</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper stloc_3
        {
            get { this.il.Emit(OpCodes.Stloc_3); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc_S"/>, <see cref="LocalBuilder"/>) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at index (short form).
        /// </summary>
        /// <param name="local">A local variable.</param>
        /// <seealso cref="OpCodes.Stloc_S">OpCodes.Stloc_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,LocalBuilder)">ILGenerator.Emit</seealso>
        public EmitHelper stloc_s(LocalBuilder local)
        {
            this.il.Emit(OpCodes.Stloc_S, local); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stloc_S"/>, byte) that
        /// pops the current value from the top of the evaluation stack and stores it 
        /// in the local variable list at index (short form).
        /// </summary>
        /// <param name="index">A local variable index.</param>
        /// <seealso cref="OpCodes.Stloc_S">OpCodes.Stloc_S</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,short)">ILGenerator.Emit</seealso>
        public EmitHelper stloc_s(byte index)
        {
            switch (index)
            {
                case 0: stloc_0.end(); break;
                case 1: stloc_1.end(); break;
                case 2: stloc_2.end(); break;
                case 3: stloc_3.end(); break;

                default:
                    this.il.Emit(OpCodes.Stloc_S, index); break;
            }

            return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stobj"/>, type) that
        /// copies a value of a specified type from the evaluation stack into a supplied memory address.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Stobj">OpCodes.Stobj</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper stobj(Type type)
        {
            this.il.Emit(OpCodes.Stobj, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Stsfld"/>, fieldInfo) that
        /// replaces the value of a static field with a value from the evaluation stack.
        /// </summary>
        /// <param name="fieldInfo">A <see cref="FieldInfo"/> representing a field.</param>
        /// <seealso cref="OpCodes.Stsfld">OpCodes.Stsfld</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,FieldInfo)">ILGenerator.Emit</seealso>
        public EmitHelper stsfld(FieldInfo fieldInfo)
        {
            this.il.Emit(OpCodes.Stsfld, fieldInfo); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Sub"/>) that
        /// subtracts one value from another and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Sub">OpCodes.Sub</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper sub
        {
            get { this.il.Emit(OpCodes.Sub); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Sub_Ovf"/>) that
        /// subtracts one integer value from another, performs an overflow check,
        /// and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Sub_Ovf">OpCodes.Sub_Ovf</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper sub_ovf
        {
            get { this.il.Emit(OpCodes.Sub_Ovf); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Sub_Ovf_Un"/>) that
        /// subtracts one unsigned integer value from another, performs an overflow check,
        /// and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Sub_Ovf_Un">OpCodes.Sub_Ovf_Un</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper sub_ovf_un
        {
            get { this.il.Emit(OpCodes.Sub_Ovf_Un); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Switch"/>, label[]) that
        /// implements a jump table.
        /// </summary>
        /// <param name="labels">The array of label objects to which to branch from this location.</param>
        /// <seealso cref="OpCodes.Switch">OpCodes.Switch</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Label[])">ILGenerator.Emit</seealso>
        public EmitHelper @switch(Label[] labels)
        {
            this.il.Emit(OpCodes.Switch, labels); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Tailcall"/>) that
        /// performs a postfixed method call instruction such that the current method's stack frame 
        /// is removed before the actual call instruction is executed.
        /// </summary>
        /// <seealso cref="OpCodes.Tailcall">OpCodes.Tailcall</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper tailcall
        {
            get { this.il.Emit(OpCodes.Tailcall); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Throw"/>) that
        /// throws the exception object currently on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Throw">OpCodes.Throw</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper @throw
        {
            get { this.il.Emit(OpCodes.Throw); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Unaligned"/>, label) that
        /// indicates that an address currently atop the evaluation stack might not be aligned 
        /// to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, 
        /// initblk, or cpblk instruction.
        /// </summary>
        /// <param name="label">The label to branch from this location.</param>
        /// <seealso cref="OpCodes.Unaligned">OpCodes.Unaligned</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Label)">ILGenerator.Emit</seealso>
        public EmitHelper unaligned(Label label)
        {
            this.il.Emit(OpCodes.Unaligned, label); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Unaligned"/>, long) that
        /// indicates that an address currently atop the evaluation stack might not be aligned 
        /// to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, 
        /// initblk, or cpblk instruction.
        /// </summary>
        /// <param name="addr">An address is pushed onto the stack.</param>
        /// <seealso cref="OpCodes.Unaligned">OpCodes.Unaligned</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,long)">ILGenerator.Emit</seealso>
        public EmitHelper unaligned(long addr)
        {
            this.il.Emit(OpCodes.Unaligned, addr); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Unbox"/>, type) that
        /// converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Unbox">OpCodes.Unbox</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper unbox(Type type)
        {
            this.il.Emit(OpCodes.Unbox, type); return this;
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Unbox_Any"/>, type) that
        /// converts the boxed representation of a value type to its unboxed form.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Unbox_Any">OpCodes.Unbox_Any</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper unbox_any(Type type)
        {
            this.il.Emit(OpCodes.Unbox_Any, type);
            return this;
        }

        /// <summary>
        /// Calls <see cref="unbox_any(Type)"/> if given type is a value type.
        /// </summary>
        /// <param name="type">A Type</param>
        /// <seealso cref="OpCodes.Unbox_Any">OpCodes.Unbox</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode,Type)">ILGenerator.Emit</seealso>
        public EmitHelper unboxIfValueType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return type.IsValueType ? unbox_any(type) : this;
        }

        public EmitHelper unbox_or_castclass(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return type.IsValueType ? unbox_any(type) : castclass(type);
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Volatile"/>) that
        /// specifies that an address currently atop the evaluation stack might be volatile, 
        /// and the results of reading that location cannot be cached or that multiple stores 
        /// to that location cannot be suppressed.
        /// </summary>
        /// <seealso cref="OpCodes.Volatile">OpCodes.Volatile</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper @volatile
        {
            get { this.il.Emit(OpCodes.Volatile); return this; }
        }

        /// <summary>
        /// Calls ILGenerator.Emit(<see cref="OpCodes.Xor"/>) that
        /// computes the bitwise XOR of the top two values on the evaluation stack, 
        /// pushing the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Xor">OpCodes.Xor</seealso>
        /// <seealso cref="System.Reflection.Emit.ILGenerator.Emit(OpCode)">ILGenerator.Emit</seealso>
        public EmitHelper xor
        {
            get { this.il.Emit(OpCodes.Xor); return this; }
        }

        /// <summary>
        /// Ends sequence of property calls.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void end()
        {
        }
        #endregion
    }
}