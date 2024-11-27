using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(ExpandableObjectAttribute))]
      internal class AttributeDrawer_ExpandableObject : BasePropertyDrawer // ~Warped Imagination
      {
            private UnityEditor.Editor m_Editor = null;

            public override void Initialize(SerializedProperty property)
            {
                  property.isExpanded = false;
            }
            public override void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label)
            {
#if UNITY_2022_1_OR_NEWER
                  if (property.propertyType != SerializedPropertyType.ObjectReference)
                  {
                        EditorGUI.HelpBox(position, $"Use ExpandableObject attribute on a field of type '{SerializedPropertyType.ObjectReference}'.", UnityEditor.MessageType.Error);
                        return;
                  }
                  EditorGUI.PropertyField(position, property, label);

                  if (property.objectReferenceValue != null && (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none, true)))
                  {
                        using (new EditorGUI.IndentLevelScope(increment: 1))
                        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                        {
                              if (m_Editor == null)
                              {
                                    UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref m_Editor);
                              }
                              else
                              {
                                    position.y += EditorGUI.GetPropertyHeight(property);
                                    m_Editor.OnInspectorGUI();
                              }
                        }
                  }
#else
                  EditorGUI.HelpBox(position, "The ExpandableObject attribute is not supported in Unity versions older than 2022.", UnityEditor.MessageType.Error);
#endif
            }
      }
}