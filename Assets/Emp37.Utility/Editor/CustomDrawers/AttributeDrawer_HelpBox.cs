using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(HelpBoxAttribute), true)]
      internal class AttributeDrawer_HelpBox : BaseDecoratorDrawer
      {
            private const float BoxOffset = -4F, TextOffset = 34F;

            private static readonly GUIStyle label = new(EditorStyles.label)
            {
                  alignment = TextAnchor.MiddleLeft,
                  wordWrap = true,
            };

            private GUIContent Icon;

            private HelpBoxAttribute Attribute => attribute as HelpBoxAttribute;


            public override void Initialize()
            {
                  var type = Attribute.MessageType;
                  Icon = type is 0 ? null : EditorGUIUtility.IconContent("console." + type switch { MessageType.Warning => "warnicon", MessageType.Error => "erroricon", _ => "infoicon", });
            }
            public override void OnGUI(Rect position)
            {
                  base.OnGUI(position);
                  position.height = Attribute.Height;

                  EditorGUI.HelpBox(position.Indent(BoxOffset), string.Empty, 0);

                  if (Icon != null)
                  {
                        EditorGUI.LabelField(position, Icon);
                        position = position.Indent(TextOffset);
                  }
                  EditorGUI.LabelField(position, Attribute.Message, label);
            }
            public override float GetHeight() => Attribute.Height + EditorGUIUtility.standardVerticalSpacing;
      }
}