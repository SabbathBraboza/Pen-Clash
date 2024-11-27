using System;
using System.Reflection;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      using static ReflectionUtility;

      public static class Extensions
      {
            #region S E R I A L I Z E D   P R O P E R T Y
            /// <summary>
            /// Retrieves the attribute of type <typeparamref name="TAttribute"/> associated with a serialized property.
            /// </summary>
            /// <param name="property">The serialized property to inspect.</param>
            /// <param name="bindings">Binding flags for fetching the field information.</param>
            /// <returns>Attribute of type TAttribute if found, otherwise null.</returns>
            /// <exception cref="ArgumentNullException">When the serialized property is null.</exception>
            /// <exception cref="ArgumentException">When the serialized property target object type is null.</exception>
            public static TAttribute GetAttribute<TAttribute>(this SerializedProperty property, BindingFlags bindings = DEFAULT_FLAGS) where TAttribute : Attribute
            {
                  if (property == null) throw new ArgumentNullException(nameof(property), "SerializedProperty cannot be null.");
                  var type = property.serializedObject.targetObject.GetType() ?? throw new ArgumentException($"Target targetType of property '{property.name}' is null or the serialized object is not set.");

                  return FetchInfo<FieldInfo>(property.name, type, bindings)?.GetCustomAttribute<TAttribute>();
            }
            public static bool TryGetAttribute<TAttribute>(this SerializedProperty property, out TAttribute attribute, BindingFlags bindings = DEFAULT_FLAGS) where TAttribute : Attribute
            {
                  try
                  {
                        attribute = GetAttribute<TAttribute>(property, bindings);
                  }
                  catch (ArgumentException)
                  {
                        attribute = default;
                  }
                  return attribute != null;
            }
            public static bool HasAttribute<TAttribute>(this SerializedProperty property, BindingFlags bindings = DEFAULT_FLAGS) where TAttribute : Attribute
            {
                  var type = property.serializedObject.targetObject.GetType();
                  if (type != null)
                  {
                        var field = FetchInfo<FieldInfo>(property.name, type, bindings);
                        if (field != null)
                        {
                              return field.IsDefined(typeof(TAttribute));
                        }
                  }
                  return false;
            }
            #endregion

            #region M E M B E R   I N F O
            public static bool TryGetAttribute<TAttribute>(this MemberInfo member, out TAttribute attribute) where TAttribute : Attribute
            {
                  try
                  {
                        attribute = member.GetCustomAttribute<TAttribute>();
                  }
                  catch (ArgumentException)
                  {
                        attribute = default;
                  }
                  return attribute != null;
            }
            #endregion
      }
}