using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(LabelAttribute), true)]
      internal class AttributeDrawer_Label : BasePropertyDrawer
      {
            public override void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label)
            {
                  var attribute = base.attribute as LabelAttribute;

                  EditorGUI.PropertyField(position, property, attribute.Label, true);
            }
      }
}