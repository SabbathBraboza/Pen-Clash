using UnityEngine;

using UnityEditor;

namespace Emp37.ET
{
      using static EditorGUI;
      using static EditorGUIUtility;


      [CustomPropertyDrawer(typeof(EditorStyleGroup))]
      internal class EditorStyleGroupDrawer : PropertyDrawer
      {
            private const string p_description = "Description";
            private const string p_editorStyles = "Styles";

            private const float toggleButtonWidth = 27F; // - [ c : 1 ]
            private const float headerHeight = 21F;


            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                  using (new PropertyScope(position, label, property))
                  {
                        #region O U T L I N E D   C O N T A I N E R
                        position.height = property.isExpanded ? position.height - standardVerticalSpacing : headerHeight;
                        HelpBox(position, string.Empty, MessageType.None);
                        #endregion

                        #region H E A D E R
                        position.height = headerHeight;
                        var width = position.width; // storing original width
                        position.width = toggleButtonWidth;
                        if (GUI.Button(position, property.isExpanded ? "-" : "+")) // - [ c : 1 ]
                        {
                              property.isExpanded = !property.isExpanded;
                        }
                        _ = PropertyField(new(position) { x = position.x + position.width, width = width - position.width }, property.FindPropertyRelative(p_description), GUIContent.none);
                        position.width = width; // restoring original width
                        position.y += position.height; // ensures that the description occupies the defined height
                        #endregion

                        if (property.isExpanded)
                        {
                              var styles = property.FindPropertyRelative(p_editorStyles);

                              #region A R R A Y   E L E M E N T S
                              position.y += standardVerticalSpacing; // [ 1 ] - extra spacing in between header & expanded content
                              for (byte i = 0; i < styles.arraySize; i++)
                              {
                                    var styleElement = styles.GetArrayElementAtIndex(i);
                                    using (new IndentLevelScope(0))
                                    {
                                          position.height = EditorGUI.GetPropertyHeight(styleElement);
                                          _ = PropertyField(position, styleElement, true);
                                          position.y += position.height;
                                    }
                              }
                              #endregion

                              #region A R R A Y   C O N T R O L   O P T I O N S
                              position.height = singleLineHeight; // [ 2 ]
                              position.width /= 2F;
                              if (GUI.Button(position, "ADD", EditorStyles.miniButtonLeft))
                              {
                                    styles.InsertArrayElementAtIndex(styles.arraySize);
                              }
                              position.x += position.width;
                              if (GUI.Button(position, "REMOVE", EditorStyles.miniButtonRight) && styles.arraySize > 0)
                              {
                                    styles.DeleteArrayElementAtIndex(styles.arraySize - 1);
                              }
                              #endregion
                        }
                  }
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                  float value = headerHeight;
                  if (property.isExpanded)
                  {
                        var styles = property.FindPropertyRelative(p_editorStyles);
                        for /*individual elements*/ (int i = 0; i < styles.arraySize; i++)
                        {
                              value += EditorGUI.GetPropertyHeight(styles.GetArrayElementAtIndex(i), GUIContent.none);
                        }
                        value += standardVerticalSpacing + singleLineHeight; /* [ 1 ] + [ 2 ] */
                  }
                  return value;
            }
      }
}