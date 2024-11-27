using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      using static ColorLibrary;


      [CustomPropertyDrawer(typeof(TitleAttribute), true)]
      internal class AttributeDrawer_Title : BaseDecoratorDrawer
      {
            private const float InitialGap = 8F, UnderlineHeight = 1F;

            private readonly GUIStyle style = new(EditorStyles.boldLabel)
            {
                  fontSize = 14,
                  stretchHeight = false,
            };

            private Vector2 size;

            private TitleAttribute Attribute => attribute as TitleAttribute;


            public override void Initialize()
            {
                  if (!Attribute.Stretch)
                  {
                        style.CalcMinMaxWidth(Attribute.Content, out _, out float max);
                        size.x = max;
                  }
                  size.y = style.CalcHeight(Attribute.Content, EditorGUIHelper.ReleventWidth);
            }
            public override void OnGUI(Rect position)
            {
                  base.OnGUI(position);

                  position.y += InitialGap; // - [ 1 ]
                  position.height = size.y;

                  style.normal.textColor = Pick(Attribute.Text);
                  EditorGUI.LabelField(position, Attribute.Content, style);

                  position.y += position.height + EditorGUIUtility.standardVerticalSpacing; // - [ 2 ]
                  if (!Attribute.Stretch)
                  {
                        position.width = size.x;
                  }
                  position.height = UnderlineHeight; // - [ 3 ]
                  EditorGUI.DrawRect(position, Pick(Attribute.Underline));
            }
            public override float GetHeight() => InitialGap /* - [ 1 ]*/ + size.y + 2F * EditorGUIUtility.standardVerticalSpacing /* - [ 2 ] + extra spacing*/ + UnderlineHeight /* - [ 3 ]*/;
      }
}