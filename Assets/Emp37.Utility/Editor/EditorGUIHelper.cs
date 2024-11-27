using UnityEditor;

namespace Emp37.Utility.Editor
{
      using static EditorGUIUtility;

      public static class EditorGUIHelper
      {
            public static readonly float ExcessWidth = 2F * standardVerticalSpacing + singleLineHeight;
            public static float ReleventWidth => currentViewWidth - ExcessWidth;
      }
}