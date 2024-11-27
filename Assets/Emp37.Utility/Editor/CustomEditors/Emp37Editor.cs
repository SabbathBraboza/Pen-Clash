using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      using static ReflectionUtility;


      #region B A S E   E D I T O R S
      [CanEditMultipleObjects, CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
      internal class MonoBehaviourEditor : Emp37Editor
      {
      }

      [CanEditMultipleObjects, CustomEditor(typeof(ScriptableObject), true, isFallback = true)]
      internal class ScriptableObjectEditor : Emp37Editor
      {
      }
      #endregion

      internal class Emp37Editor : UnityEditor.Editor
      {
            private Type targetType;

            private bool showDefaultProperty;
            private SerializedProperty defaultProperty;

            private SerializedProperty[] serializedProperties;
            private MethodInfo[] serializedMethods;


            private void OnEnable()
            {
                  targetType = target.GetType();

                  showDefaultProperty = !targetType.IsDefined(typeof(HideDefaultScriptAttribute));

                  #region I N I T I A L I Z E   P R O P E R T I E S
                  if (serializedProperties == null)
                  {
                        Queue<SerializedProperty> properties = new();
                        SerializedProperty iterator = serializedObject.GetIterator();

                        while (iterator.NextVisible(true))
                        {
                              SerializedProperty property = serializedObject.FindProperty(iterator.name);
                              if (property != null)
                              {
                                    if (property.name == "m_Script")
                                    {
                                          defaultProperty = property;
                                          continue;
                                    }
                                    properties.Enqueue(property);
                              }
                        }
                        serializedProperties = properties.ToArray();
                  }
                  #endregion

                  #region I N I T I A L I Z E   M E T H O D S
                  serializedMethods = targetType.GetMethods(DEFAULT_FLAGS);
                  #endregion
            }
            public override void OnInspectorGUI()
            {
                  serializedObject.Update();
                  {
                        #region D E F A U L T   S C R I P T
                        if (showDefaultProperty)
                        {
                              GUI.enabled = false;
                              EditorGUILayout.PropertyField(defaultProperty);
                        }
                        #endregion

                        #region S E R I A L I Z E D   P R O P E R T I E S
                        foreach (var property in serializedProperties)
                        {
                              FieldInfo field = FetchInfo<FieldInfo>(property.name, targetType);
                              if (EvaluateVisibility(field))
                              {
                                    GUI.enabled = EvaluateEnabled(field);
                                    EditorGUILayout.PropertyField(property);
                              }
                        }
                        #endregion

                        #region S E R I A L I Z E D   M E TH O D S
                        foreach (MethodInfo method in serializedMethods)
                        {
                              if (method.TryGetAttribute(out ButtonAttribute a0) && EvaluateVisibility(method))
                              {
                                    GUI.enabled = EvaluateEnabled(method);
                                    GUI.backgroundColor = ColorLibrary.Pick(a0.Shade);
                                    if (GUILayout.Button(method.Name, GUILayout.Height(a0.Height)))
                                    {
                                          InvokeWithNamedParameters(method, target, a0.Parameters);
                                    }
                              }
                        }
                        #endregion

                        GUI.enabled = true;
                  }
                  serializedObject.ApplyModifiedProperties();
            }

            private bool EvaluateEnabled(MemberInfo member)
            {
                  if (member.TryGetAttribute(out EnableWhenAttribute a0) && FetchValue(a0.ConditionName, target) is bool v0)
                  {
                        return v0;
                  }
                  else
                  if (member.TryGetAttribute(out DisableWhenAttribute a1) && FetchValue(a1.ConditionName, target) is bool v1)
                  {
                        return !v1;
                  }
                  else
                  if (member.TryGetAttribute(out ReadonlyAttribute a2))
                  {
                        return a2.ExclusiveToPlaymode && !EditorApplication.isPlaying;
                  }
                  return true;
            }
            private bool EvaluateVisibility(MemberInfo member)
            {
                  if (member.TryGetAttribute(out ShowWhenAttribute a0) && FetchValue(a0.ConditionName, target) is bool v0)
                  {
                        return v0;
                  }
                  else
                  if (member.TryGetAttribute(out HideWhenAttribute a1) && FetchValue(a0.ConditionName, target) is bool v1)
                  {
                        return !v1;
                  }
                  return true;
            }
      }
}