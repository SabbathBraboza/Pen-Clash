using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(RequireObjectAttribute))]
      internal class AttributeDrawer_RequireObject : BasePropertyDrawer
      {
            private const float errorMessageHeight = 21F;


            public override void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label)
            {
                  if (property.propertyType != SerializedPropertyType.ObjectReference)
                  {
                        EditorGUI.HelpBox(position, $"Use RequireObject attribute on a field of type '{SerializedPropertyType.ObjectReference}'.", UnityEditor.MessageType.Error);
                        return;
                  }
                  if (property.objectReferenceValue == null)
                  {
                        var attribute = base.attribute as RequireObjectAttribute;

                        position.height = errorMessageHeight;
                        EditorGUI.HelpBox(position, attribute.Message, UnityEditor.MessageType.Error);
                        position.y += errorMessageHeight + EditorGUIUtility.standardVerticalSpacing; // - [ 1 ]
                  }
                  position.height = EditorGUIUtility.singleLineHeight;
                  EditorGUI.PropertyField(position, property, label);
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                  float height = base.GetPropertyHeight(property, label);
                  if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
                  {
                        height += errorMessageHeight + EditorGUIUtility.standardVerticalSpacing; // - [ 1 ]
                  }
                  return height;
            }
      }
}