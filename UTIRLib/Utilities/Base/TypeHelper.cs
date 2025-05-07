using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib
{
    public static class TypeHelper
    {
        public static bool TryResolveMemberValuesAndSetOrInvoke(MemberInfo member, object target, out object? methodResult,
            params object[] values)
        {
            if (member is MethodInfo method) {
                return TryResolveArgsAndInvokeMethod(method, target, out methodResult, values);
            }
            else if (member is PropertyInfo property) {
                methodResult = null;
                return TryResolveAndSetPropertyValue(property, target, values);
            }
            else if (member is FieldInfo field) {
                methodResult = null;
                return TryResolveAndSetFieldValue(field, target, values);
            }

            methodResult = null;
            return false;
        }
        /// <exception cref="Exception"></exception>
        public static void ResolveMemberValuesAndSetOrInvoke(MemberInfo member, object target, out object? methodResult, 
            params object[] values)
        {
            if (!TryResolveMemberValuesAndSetOrInvoke(member, target, out methodResult, values)) {
                throw new Exception("Error while resolving values.");
            }
        }

        /// <exception cref="NullOrEmptyCollectionException"></exception>
        public static bool TryResolveArgsAndInvokeMethod(MethodInfo method, object target, out object? methodResult,
            params object[] args)
        {
            if (args.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(nameof(args));
            }

            var resolvedArgs = new object[args.Length];
            BitArray resolvedMarks = new(args.Length);
            ParameterInfo[] parameters = method.GetParameters();
            Type parameterType;
            for (int i = 0; i < parameters.Length; i++) {
                parameterType = parameters[i].ParameterType;
                for (int j = 0; j < args.Length; j++) {
                    if (parameterType.IsInstanceOfType(args[j])) {
                        resolvedArgs[i] = args[j];
                        resolvedMarks[i] = true;
                    }
                }
            }

            for (int i = 0; i < resolvedMarks.Length; i++) {
                if (!resolvedMarks[i]) {
                    methodResult = null;
                    return false;
                }
            }

            methodResult = method.Invoke(target, resolvedArgs);
            return true;
        }
        /// <exception cref="Exception"></exception>
        public static void ResolveArgsAndInvokeMethod(MethodInfo method, object target, out object? methodResult,
            params object[] args)
        {
            if (!TryResolveArgsAndInvokeMethod(method, target, out methodResult, args)) {
                throw new Exception("Error while resolving values.");
            }
        }

        /// <exception cref="NullOrEmptyCollectionException"></exception>
        public static bool TryResolveAndSetPropertyValue(PropertyInfo property, object target, object[] values)
        {
            if (values.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(nameof(values));
            }

            Type propertyType = property.PropertyType;
            for (int i = 0; i < values.Length; i++) {
                if (propertyType.IsInstanceOfType(values[i])) {
                    property.SetValue(target, values[i]);
                    return true;
                }
            }

            return false;
        }
        /// <exception cref="Exception"></exception>
        public static void ResolveAndSetPropertyValue(PropertyInfo property, object target, object[] values)
        {
            if (!TryResolveAndSetPropertyValue(property, target, values)) {
                throw new Exception("Error while resolving values.");
            }
        }

        /// <exception cref="NullOrEmptyCollectionException"></exception>
        public static bool TryResolveAndSetFieldValue(FieldInfo field, object target, object[] values)
        {
            if (values.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(nameof(values));
            }

            Type fieldType = field.FieldType;
            for (int i = 0; i < values.Length; i++) {
                if (fieldType.IsInstanceOfType(values[i])) {
                    field.SetValue(target, values[i]);
                    return true;
                }
            }

            return false;
        }
        /// <exception cref="Exception"></exception>
        public static void ResolveAndSetFieldValue(FieldInfo field, object target, object[] values)
        {
            if (!TryResolveAndSetFieldValue(field, target, values)) {
                throw new Exception("Error while resolving values.");
            }
        }

        public static Type? ConvertStringToType(string typeName, bool logWhileError = true, params string[] namespaces)
        {
            try {
                return Type.GetType(typeName, throwOnError: true, ignoreCase: false);
            }
            catch (Exception) {
                Type[] assemblyTypes;
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                for (int i = 0; i < assemblies.Length; i++) {
                    assemblyTypes = assemblies[i].GetTypes();
                    foreach (var assemblyType in assemblyTypes) {
                        if (assemblyType.Name == typeName && namespaces.Length == 0 ||
                            namespaces.Any((str) => str.Contains(assemblyType.Namespace))) {
                            return assemblyType;
                        }
                    }
                }
            }

            if (logWhileError) {
                Debug.LogError("Error while getting type.");
            }
            return null;
        }

        public static TMember[] GetMembersRecursively<TMember>(Type type, BindingFlags bindingFlags)
            where TMember : MemberInfo
        {
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }

            List<TMember> results = new List<TMember>();
            TMember[] members;
            while (type != null) {
                members = type.GetMembers().OfType<TMember>().ToArray();
                if (members.Length > 0) {
                    results.AddRange(members);
                }

            }
            return results.ToArray();
        }
    }
}
