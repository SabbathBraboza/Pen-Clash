using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      using Type = SerializedPropertyType;


      [CustomPropertyDrawer(typeof(RepeatAttribute), true)]
      internal class AttributeDrawer_Repeat : BasePropertyDrawer
      {
            public override void Initialize(SerializedProperty property)
            {
                  Validate(property);
            }
            public override void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label)
            {
                  if (property.propertyType is not (Type.Float or Type.Integer or Type.Vector2 or Type.Vector3 or Type.Vector2Int or Type.Vector3Int))
                  {
                        EditorGUI.HelpBox(position, "Use RepeatAttribute on 'Floating' or 'Integer' field types.", UnityEditor.MessageType.Error);
                        return;
                  }
                  EditorGUI.BeginChangeCheck();
                  EditorGUI.PropertyField(position, property, label);
                  if (EditorGUI.EndChangeCheck())
                  {
                        Validate(property);
                  }
            }

            private void Validate(SerializedProperty property)
            {
                  var attribute = base.attribute as RepeatAttribute;
                  switch (property.propertyType)
                  {
                        #region I N T E G E R
                        case Type.Integer:
                              {
                                    var value = property.intValue;
                                    property.intValue = @int(value);
                                    break;
                              }
                        case Type.Vector2Int:
                              {
                                    var value = property.vector2IntValue;
                                    property.vector2IntValue = new(x: @int(value.x), y: @int(value.y));
                                    break;
                              }
                        case Type.Vector3Int:
                              {
                                    var value = property.vector3IntValue;
                                    property.vector3IntValue = new(x: @int(value.x), y: @int(value.y), z: @int(value.z));
                                    break;
                              }
                        #endregion

                        #region F L O A T
                        case Type.Float:
                              {
                                    var value = property.floatValue;
                                    property.floatValue = @float(value);
                                    break;
                              }
                        case Type.Vector2:
                              {
                                    var value = property.vector2Value;
                                    property.vector2Value = new(x: @float(value.x), y: @float(value.y));
                                    break;
                              }
                        case Type.Vector3:
                              {
                                    var value = property.vector3Value;
                                    property.vector3Value = new(x: @float(value.x), y: @float(value.y), z: @float(value.z));
                                    break;
                              }
                              #endregion
                  }
                  int @int(int value) => (int) (attribute.Min + Mathf.Repeat(value - attribute.Min, attribute.Max - attribute.Min));
                  float @float(float value) => attribute.Min + Mathf.Repeat(value - attribute.Min, attribute.Max - attribute.Min);
            }
      }
}