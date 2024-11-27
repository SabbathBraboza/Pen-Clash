using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute used to visually separate content in the inspector with a horizontal line.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
      public class SeparatorAttribute : PropertyAttribute
      {
            public readonly Color Color;
            public readonly byte Thickness;
            public readonly bool Stretch;

            public SeparatorAttribute(Shades shade = Shades.Black, byte thickness = 3, bool stretch = false, byte alpha = byte.MaxValue)
            {
                  Color = ColorLibrary.Pick(shade).WithAlpha(alpha);
                  Thickness = thickness;
                  Stretch = stretch;
            }
      }
}