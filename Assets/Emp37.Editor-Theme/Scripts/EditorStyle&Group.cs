using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Emp37.ET
{
      [Serializable]
      internal struct EditorStyle
      {
            [Flags]
            public enum PseudoState : int
            {
                  Active = 1, Hover = 2, Focus = 4, Selected = 8, Checked = 16, Enabled = 32, Disabled = 64
            }

            public string[] Types;
            public PseudoState[] States;
            public int Flags;

            private static readonly string[] _propertyNames =
            {
                  nameof(background_image),
                  nameof(background_color),
                  nameof(border_color),
                  nameof(border_top_color),
                  nameof(border_right_color),
                  nameof(border_bottom_color),
                  nameof(border_left_color),
                  nameof(border_radius),
                  nameof(border_width),
                  nameof(color)
            };
            public static readonly Dictionary<int, string> Properties = Enumerable.Range(0, _propertyNames.Length).ToDictionary(indexKey => 1 << indexKey, indexElement => _propertyNames[indexElement]);

            #region A P P L I C A B L E   P R O P E R T I E S
            public Texture2D
                  background_image;
            public Color32
                  background_color,
                  border_color,
                  border_top_color,
                  border_right_color,
                  border_bottom_color,
                  border_left_color,
                  color;
            public RectOffset
                  border_radius,
                  border_width;
            #endregion
      }
      [Serializable]
      internal struct EditorStyleGroup
      {
            public string Description;

            public EditorStyle[] Styles;
      }
}