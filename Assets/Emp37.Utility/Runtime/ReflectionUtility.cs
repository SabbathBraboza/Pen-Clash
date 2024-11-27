using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Emp37.Utility
{
      using Field = FieldInfo;
      using Property = PropertyInfo;
      using Method = MethodInfo;


      public static class ReflectionUtility
      {
            [Flags]
            public enum MemberType
            {
                  Field = 1,
                  Property = 2,
                  Method = 4,
                  All = 7,
            }

            public const BindingFlags DEFAULT_FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            private static readonly Dictionary<(Type, string), MemberInfo> cachedMembers = new();

            public static T FetchInfo<T>(string name, Type type, BindingFlags bindings = DEFAULT_FLAGS) where T : MemberInfo
            {
                  if (type == null) throw new ArgumentNullException(nameof(type));
                  if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));

                  var key = (type, name);
                  if (!cachedMembers.TryGetValue(key, out var member))
                  {
                        var returnType = typeof(T);
                        while (type != null)
                        {
                              member = returnType switch
                              {
                                    _ when returnType == typeof(Field) => type.GetField(name, bindings),
                                    _ when returnType == typeof(Property) => type.GetProperty(name, bindings),
                                    _ when returnType == typeof(Method) => type.GetMethod(name, bindings),
                                    _ => throw new NotSupportedException()
                              };
                              if (member != null)
                              {
                                    cachedMembers[key] = member;
                                    break;
                              }
                              type = type.BaseType;
                        }
                  }
                  return member as T;
            }
            public static bool TryFetchInfo<T>(string name, Type type, out T value, BindingFlags bindings = DEFAULT_FLAGS) where T : MemberInfo
            {
                  try
                  {
                        value = FetchInfo<T>(name, type, bindings);
                  }
                  catch (ArgumentException)
                  {
                        value = null;
                  }
                  return value != null;
            }
            public static object FetchValue(string name, object target, MemberType enabled = MemberType.All, BindingFlags bindings = DEFAULT_FLAGS, params object[] parameters)
            {
                  if (target == null) throw new ArgumentNullException(nameof(target));

                  var type = target.GetType();
                  if (enabled.HasFlag(MemberType.Field))
                  {
                        if (TryFetchInfo<Field>(name, type, out var field, bindings))
                        {
                              return field.GetValue(target);
                        }
                  }
                  if (enabled.HasFlag(MemberType.Property))
                  {
                        if (TryFetchInfo<Property>(name, type, out var property, bindings) && property.CanRead)
                        {
                              return property.GetValue(target);
                        }
                  }
                  if (enabled.HasFlag(MemberType.Method))
                  {
                        if (TryFetchInfo<Method>(name, type, out var method, bindings))
                        {
                              return method.Invoke(target, parameters);
                        }
                  }
                  return null;
            }

            public static object InvokeWithNamedParameters(Method method, object target, string[] names = null, BindingFlags bindings = DEFAULT_FLAGS)
            {
                  List<object> values = new();
                  ParameterInfo[] parameters = method.GetParameters();

                  if (parameters.Length > 0)
                  {
                        Assert(names != null && parameters.Length == names.Length, "Number of parameters specified does not match the expected number.");

                        for (byte i = 0; i < parameters.Length; i++)
                        {
                              object value = FetchValue(names[i], target, MemberType.Field | MemberType.Property, bindings);

                              Assert(value != null, $"Unable to fetch value for '{names[i]}' in type '{target.GetType().FullName}'. The member may not exist or may not be accessible.");

                              Type parameterType = value.GetType(), expectedType = parameters[i].ParameterType;

                              Assert(expectedType == parameterType, $"Parameter type mismatch at index {i}. Expected type '{expectedType}' but recieved '{parameterType}'.");

                              values.Add(value);
                        }

                        void Assert(bool condition, string message)
                        {
                              if (condition) return;

                              string signature = parameters.Length > 0 ? string.Join(", ", parameters.Select(param => param.ParameterType.Name)) : string.Empty;
                              string info = $"{method.ReflectedType}.{method.Name}({signature})";

                              throw new ArgumentException($"Couldn't invoke method '{info}'.\n -- {message}");
                        }
                  }
                  return method.Invoke(target, values.ToArray());
            }
      }
}