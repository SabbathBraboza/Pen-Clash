using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      using static EditorGUIUtility;

      [CustomPropertyDrawer(typeof(SeparatorAttribute), true)]
      internal class AttributeDrawer_Separator : BaseDecoratorDrawer
      {
            private SeparatorAttribute Attribute => attribute as SeparatorAttribute;


            public override void OnGUI(Rect position)
            {
                  if (Attribute.Stretch)
                  {
                        position.x = 0F;
                        position.width = currentViewWidth;
                  }
                  position.height = Attribute.Thickness;
                  EditorGUI.DrawRect(position, Attribute.Color);
            }
            public override float GetHeight() => Attribute.Thickness + standardVerticalSpacing /*additional spacing*/;
      }
}