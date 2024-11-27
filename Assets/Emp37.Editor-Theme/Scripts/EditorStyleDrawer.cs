using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Emp37.ET
{
      using static EditorGUI;
      using static EditorGUIUtility;

      using static EditorStyles;


      [CustomPropertyDrawer(typeof(EditorStyle))]
      internal class EditorStyleDrawer : PropertyDrawer
      {
            private const string p_classTypes = "Types";
            private const string p_pseudoStates = "States";
            private const string p_flags = "Flags";

            private const float propertyDropdownMenuWidth = 25F;
            private const float emptyStyleWarningHeight = 32F;

            private static readonly GUIStyle expandableToggleStyle = new(foldoutHeader)
            {
                  clipping = TextClipping.Clip,
            };


            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                  var mask = property.FindPropertyRelative(p_flags);
                  BeginProperty(position, label, property);
                  {
                        position.height = singleLineHeight;
                        var headerRect = position;
                        headerRect.width -= propertyDropdownMenuWidth;

                        if (property.isExpanded = GUI.Toggle(headerRect, property.isExpanded, label, expandableToggleStyle)) // - [ 1 ]
                        {
                              position.y += position.height + standardVerticalSpacing;

                              #region T Y P E   &   S T A T E   A R R A Y
                              DrawArray("Class Types", ref position, property.FindPropertyRelative(p_classTypes));
                              position.y += 2F * standardVerticalSpacing; // - [ 2 ]
                              DrawArray("Pseudo States", ref position, property.FindPropertyRelative(p_pseudoStates));
                              position.y += 2F * standardVerticalSpacing; // - [ 3 ]
                              #endregion

                              #region F L A G G E D   P R O P E R T I E S
                              if (mask.intValue is 0)
                              {
                                    position.height = emptyStyleWarningHeight;
                                    HelpBox(position, "Style rule is empty.", MessageType.Warning);
                                    position.y += emptyStyleWarningHeight + standardVerticalSpacing; // - [ 4 ]
                              }
                              else EditorStyle.Properties.Where(option => (mask.intValue & option.Key) is not 0).ToList().ForEach(option =>
                              {
                                    var context = property.FindPropertyRelative(option.Value);
                                    position.height = EditorGUI.GetPropertyHeight(context);
                                    using (new IndentLevelScope(1))
                                    {
                                          var label = new GUIContent(' ' + context.name.Replace('_', '-'));
                                          PropertyField(position, context, label, context.hasVisibleChildren);
                                    }
                                    position.y += position.height + standardVerticalSpacing; // - [ 5 ]
                              });
                              #endregion
                        }

                        #region A D D   P R O P E R T Y
                        headerRect.x += headerRect.width;
                        headerRect.width = propertyDropdownMenuWidth;
                        if (DropdownButton(headerRect, GUIContent.none, FocusType.Passive, foldoutHeader))
                        {
                              var menu = new GenericMenu();
                              foreach (var option in EditorStyle.Properties)
                              {
                                    bool selected = (mask.intValue & option.Key) != 0;
                                    menu.AddItem(new GUIContent(option.Value.Replace('_', '-')), on: selected, func: () =>
                                     {
                                           mask.intValue ^= option.Key;
                                           property.serializedObject.ApplyModifiedProperties();
                                     });
                              }
                              menu.ShowAsContext();
                        }
                        #endregion
                  }
                  EndProperty();
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                  var next = singleLineHeight + standardVerticalSpacing;
                  var value = next; // - [ 1 ]
                  if (property.isExpanded)
                  {
                        value += GetArrayHeight(property.FindPropertyRelative(p_classTypes).arraySize);
                        value += 2F * standardVerticalSpacing; // - [ 2 ]
                        value += GetArrayHeight(property.FindPropertyRelative(p_pseudoStates).arraySize);
                        value += 2F * standardVerticalSpacing; // - [ 3 ]
                        float GetArrayHeight(int arraySize) => (2F * next) + (arraySize * next);

                        var maskValue = property.FindPropertyRelative(p_flags).intValue;
                        value += maskValue is 0 ?
                              emptyStyleWarningHeight + standardVerticalSpacing // - [ 4 ]
                              :
                              EditorStyle.Properties.Where(option => (maskValue & option.Key) != 0).Sum(option => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(option.Value)) + standardVerticalSpacing); // - [ 5 ]
                  }
                  return value;
            }

            private void DrawArray(string title, ref Rect position, SerializedProperty property)
            {
                  var next = position.height + standardVerticalSpacing;

                  #region T I T L E
                  var boxRect = position;
                  boxRect.height += property.arraySize * next; // every array element
                  boxRect.height += next /*(+, -) button height*/ + standardVerticalSpacing /*ignoreable bleeding*/;
                  LabelField(boxRect, ' ' + title, GUI.skin.box);
                  position.y += next;
                  #endregion

                  #region E L E M E N T S
                  for (byte i = 0; i < property.arraySize; i++)
                  {
                        var element = property.GetArrayElementAtIndex(i);
                        if (element.propertyType is SerializedPropertyType.Enum)
                        {
                              element.enumValueFlag = MaskField(position, GUIContent.none, element.enumValueFlag, element.enumNames);
                        }
                        else
                        if (element.propertyType is SerializedPropertyType.String)
                        {
                              BeginChangeCheck();
                              PropertyField(position, element, GUIContent.none);
                              if (EndChangeCheck()) element.stringValue = element.stringValue.Replace(' ', '-');
                        }
                        position.y += next;
                  }
                  #endregion

                  #region O P T I O N S
                  var buttonRect = position;
                  buttonRect.width *= 0.5F;
                  if (GUI.Button(buttonRect, "+", miniButtonLeft) || property.arraySize is 0)
                  {
                        property.InsertArrayElementAtIndex(property.arraySize);
                  }
                  buttonRect.x += buttonRect.width;
                  if (GUI.Button(buttonRect, "-", miniButtonRight) && property.arraySize > 1)
                  {
                        property.DeleteArrayElementAtIndex(property.arraySize - 1);
                  }
                  position.y += next;
                  #endregion
            }
      }
}