using System;
using UnityEngine;
using UTIRLib.Diagnostics;

namespace UTIRLib
{
    public static partial class MonoBehaviourExtensions
    {
        ///// <summary>
        ///// If <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> if exists and assign it
        ///// </summary>
        //public static void AssignComponent<TObj, TResult>(this MonoBehaviour mono, ref TResult value) 
        //    where TObj : class
        //    where TResult : class
        //{
        //    if (mono == null || value != null) {
        //        return;
        //    }

        //    value = mono.GetComponent<TObj>() as TResult;
        //    if (value == null) {
        //        Debug.LogError(new ObjectNotFoundMessage(nameof(value), typeof(TObj)));
        //    }
        //}
        ///// <summary>
        ///// If <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> if exists and assign it
        ///// </summary>
        //public static void AssignComponent<TObj>(this MonoBehaviour mono, ref TObj value)
        //    where TObj : class => mono.AssignComponent<TObj, TObj>(ref value);

        ///// <summary>
        ///// If <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from parent if exists and assign it
        ///// </summary>
        //public static void AssignComponentFromParent<TObj, TResult>(this MonoBehaviour mono, 
        //    ref TResult value, bool includeInactive = true)
        //    where TObj : class
        //    where TResult : class
        //{
        //    if (mono == null || value != null) {
        //        return;
        //    }

        //    value = mono.GetComponentInParent<TObj>(includeInactive) as TResult;
        //    if (value == null) {
        //        Debug.LogError(new ObjectNotFoundMessage(nameof(value), typeof(TObj)));
        //    }
        //}
        ///// <summary>
        ///// If <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from parent if exists and assign it
        ///// </summary>
        //public static void AssignComponentFromParent<TObj>(this MonoBehaviour mono,
        //    ref TObj value, bool includeInactive = true)
        //    where TObj : class => mono.AssignComponentFromParent<TObj, TObj>(ref value, includeInactive);

        ///// <summary>
        ///// If <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from child if exists and assign it
        ///// </summary>
        //public static void AssignComponentFromChild<TObj, TResult>(this MonoBehaviour mono, ref TResult value,
        //    string childName = null, bool includeInactive = true)
        //    where TObj : class
        //    where TResult : class
        //{
        //    if (mono == null || value != null) {
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(childName)) {
        //        value = mono.GetComponentInChildren<TObj>(includeInactive) as TResult;
        //    }
        //    else {
        //        Transform foundTransform = mono.transform.Find(childName);
        //        if (foundTransform == null) {
        //            Debug.LogError(new ObjectNotFoundMessage(childName, typeof(Transform)));
        //        }
        //        value = foundTransform.GetComponent<TObj>() as TResult;
        //    }
            
        //    if (value == null) {
        //        Debug.LogError(new ObjectNotFoundMessage(nameof(value), typeof(TResult)));
        //    }
        //}
        ///// <summary>
        ///// If <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from child if exists and assign it
        ///// </summary>
        //public static void AssignComponentFromChild<T>(this MonoBehaviour mono, ref T value,
        //   string childName = null, bool includeInactive = true)
        //   where T : class => mono.AssignComponentFromChild<T, T>(ref value, childName, includeInactive);
        ///// <summary>
        ///// If <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> if exists and assign it
        ///// </summary>
        //public static void SetComponentValueFromChild<TObj, TResult, TEnum>(this MonoBehaviour mono,
        //    ref TResult value, TEnum childNameEnum, bool includeInactive = true)
        //    where TObj : class
        //    where TResult : class
        //    where TEnum : Enum => mono.AssignComponentFromChild<TObj, TResult>(
        //        ref value, childNameEnum.ToString(), includeInactive);

        //private static bool IsTypesEquals<T1, T2>() => typeof(T1) == typeof(T2);

        ////[Obsolete]
        /////// <summary>
        /////// If <see langword="field"/> <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from child if exists and assign to <see langword="field"/>
        /////// </summary>
        ////public static void ForceSetComponentFieldValue<T>(this MonoBehaviour mono, string fieldName,
        ////    BindingFlags? bindingFlags = null)
        ////    where T : class
        ////{
        ////    T value = null;
        ////    mono.SetComponentValue(ref value);
        ////    mono.ForceSetFieldValue(fieldName, value, bindingFlags);
        ////}
        ////[Obsolete]
        /////// <summary>
        /////// If <see langword="field"/> <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from parent if exists and assign to <see langword="field"/>
        /////// </summary>
        ////public static void ForceSetComponentFieldValueFromParent<T>(this MonoBehaviour mono, string fieldName,
        ////    bool includeInactive = true, BindingFlags? bindingFlags = null)
        ////    where T : class
        ////{
        ////    T value = null;
        ////    mono.SetComponentValueFromParent(ref value, includeInactive);
        ////    mono.ForceSetFieldValue(fieldName, value, bindingFlags);
        ////}
        ////[Obsolete]
        /////// <summary>
        /////// If <see langword="field"/> <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from child if exists and assign to <see langword="field"/>
        /////// </summary>
        ////public static void ForceSetComponentFieldValueFromChild<T>(this MonoBehaviour mono, string fieldName,
        ////    string childName = null, bool includeInactive = true, BindingFlags? bindingFlags = null)
        ////    where T : class
        ////{
        ////    T value = null;
        ////    mono.SetComponentValueFromChild(ref value, childName, includeInactive);
        ////    mono.ForceSetFieldValue(fieldName, value, bindingFlags);
        ////}
        ////[Obsolete]
        /////// <summary>
        /////// If <see langword="field"/> <see langword="value"/> is <see langword="null"/> - gets <see cref="Component"/> from child if exists and assign to <see langword="field"/>
        /////// </summary>
        ////public static void ForceSetComponentFieldValueFromChild<T, TEnum>(this MonoBehaviour mono, string fieldName,
        ////    TEnum childNameEnum, bool includeInactive = true, BindingFlags? bindingFlags = null)
        ////    where T : class
        ////    where TEnum : Enum =>
        ////    mono.ForceSetComponentFieldValueFromChild<T>(
        ////        fieldName, childNameEnum.ToString(), includeInactive, bindingFlags);

        ////[Obsolete]
        /////// <summary>
        /////// Force sets <see langword="readonly"/> <see langword="field"/> <see langword="value"/>
        /////// </summary>
        ////public static void ForceSetFieldValue<TValue>(this MonoBehaviour mono, string fieldName, TValue value,
        ////    BindingFlags? bindingFlags = null)
        ////{
        ////    FieldInfo field = mono.GetType().
        ////        GetField(fieldName, bindingFlags ?? 
        ////        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        ////    if (field is null) {
        ////        Debug.LogError($"Wasn't found field: {fieldName}.");
        ////        return;
        ////    }

        ////    field.SetValue(mono, value);
        ////}

        ////[Obsolete]
        /////// <summary>
        /////// Apply <see cref="Component.GetComponent(Type)"/> to every <see cref="Component"/> <see langword="field"/>
        /////// </summary>
        ////public static void AutoSetComponents(this MonoBehaviour mono, BindingFlags? bindingFlags = null)
        ////{
        ////    FieldInfo[] fields = mono.GetType().
        ////        GetFields(bindingFlags ?? BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        ////    foreach (FieldInfo field in fields) {
        ////        if (field.FieldType.IsClass && field.FieldType.IsSubclassOf(typeof(Component)) ||
        ////            field.FieldType == typeof(Component)) {
        ////            field.SetValue(mono, mono.GetComponent(field.FieldType));
        ////        }
        ////    }
        ////}
        ////[Obsolete]
        /////// <summary>
        /////// Apply <see cref="Component.GetComponent(Type)"/> to every <see cref="Component"/> <see langword="field"/>
        /////// </summary>
        ////public static void AutoSetComponents(this MonoBehaviour mono, BindingFlags? bindingFlags = null,
        ////    params string[] fieldNames)
        ////{
        ////    bindingFlags ??= BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        ////    Type monoType = mono.GetType();
        ////    List<FieldInfo> foundFields = new(fieldNames.Length);
        ////    FieldInfo foundField;
        ////    foreach (string fieldName in fieldNames) {
        ////        foundField = monoType.GetField(fieldName, bindingFlags.Value);
        ////        if (foundField is null) {
        ////            Debug.LogError($"Wasn't found field: {fieldName}.");
        ////            continue;
        ////        }

        ////        foundFields.Add(foundField);
        ////    }

        ////    foreach (FieldInfo field in foundFields) {
        ////        if (field.FieldType.IsClass && field.FieldType.IsSubclassOf(typeof(Component)) ||
        ////            field.FieldType == typeof(Component)) {
        ////            field.SetValue(mono, mono.GetComponent(field.FieldType));
        ////        }
        ////    }
        ////}
    }
}
