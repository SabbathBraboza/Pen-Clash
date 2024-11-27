using UnityEngine;
using UnityEngine.UI;

namespace Emp37.Utility
{
      public static class Extensions
      {
            #region R E C T
            /// <summary>
            /// Adds left indentation to this rect.
            /// </summary>
            public static Rect Indent(this Rect rect, float value) => new(rect.x + value, rect.y, rect.width - value, rect.height);
            #endregion

            #region C O L O R   L I B R A R Y
            public static Color32 WithAlpha(this Color32 color, byte value) => new(color.r, color.g, color.b, value);
            public static void ApplyShade(this Image renderer, Shades shade, byte alpha = byte.MaxValue) => renderer.color = ColorLibrary.Pick(shade).WithAlpha(alpha);
            public static void ApplyShade(this SpriteRenderer renderer, Shades shade, byte alpha = byte.MaxValue) => renderer.color = ColorLibrary.Pick(shade).WithAlpha(alpha);
            #endregion

            #region T R A N S F O R M
            public static void Zero(this Transform transform) => transform.localPosition = transform.localEulerAngles = transform.localScale = Vector3.zero;
            #endregion
      }
}