using System.Reflection;

using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(InlineButtonAttribute), true)]
      internal class AttributeDrawer_InlineButton : BasePropertyDrawer
      {
            private const float Gap = 2F;

            public override void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label)
            {
                  var attribute = base.attribute as InlineButtonAttribute;

                  position.width -= attribute.Width;
                  EditorGUI.PropertyField(position, property, label);
                  position.x += position.width + Gap;
                  position.width = attribute.Width - Gap;

                  if (GUI.Button(position, attribute.Name ?? attribute.Method))
                  {
                        object target = property.serializedObject.targetObject;
                        MethodInfo method = ReflectionUtility.FetchInfo<MethodInfo>(attribute.Method, target.GetType());

                        if (method != null)
                        {
                              ReflectionUtility.InvokeWithNamedParameters(method, target, attribute.Parameters);
                        }
                  }
            }
      }
}