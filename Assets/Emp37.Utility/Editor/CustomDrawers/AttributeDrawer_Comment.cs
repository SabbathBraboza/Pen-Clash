using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(CommentAttribute), true)]
      internal class AttributeDrawer_Comment : BasePropertyDrawer
      {
            private const byte BackgroundAlpha = 25;
            private const float MinHeight = 21F, BlockWidth = 3F;

            private readonly GUIStyle style = new(EditorStyles.label) { richText = true, wordWrap = true };

            private CommentAttribute Attribute => attribute as CommentAttribute;


            public override void Initialize(SerializedProperty property)
            {
                  style.fontStyle = Attribute.Style;
            }
            public override void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label)
            {
                  var commentArea = position;
                  commentArea.size = new(x: EditorGUIHelper.ReleventWidth, y: GetStyleHeight(Attribute.Content) /* - [ 1 ]*/);
                  EditorGUI.LabelField(commentArea, Attribute.Content, style);

                  EditorGUI.DrawRect(commentArea, Attribute.Tint.WithAlpha(BackgroundAlpha));

                  commentArea.width = BlockWidth;
                  commentArea.x -= commentArea.width + 1;
                  EditorGUI.DrawRect(commentArea, Attribute.Tint);

                  position.y += commentArea.height + EditorGUIUtility.standardVerticalSpacing; // - [ 2 ]
                  position.height = EditorGUI.GetPropertyHeight(property);
                  EditorGUI.PropertyField(position, property, label, true);
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                  float height = base.GetPropertyHeight(property, label);
                  height += GetStyleHeight(Attribute.Content) + EditorGUIUtility.standardVerticalSpacing; // - [ 1 ] + [ 2 ]
                  return height;
            }

            private float GetStyleHeight(GUIContent content) => Mathf.Clamp(style.CalcHeight(content, EditorGUIHelper.ReleventWidth), MinHeight, float.MaxValue);
      }
}