using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute for adding comments to a field in the inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
      public class CommentAttribute : PropertyAttribute
      {
            public readonly GUIContent Content;
            public readonly Color32 Tint = Color.black;
            public readonly FontStyle Style;

            public CommentAttribute(string text) => Content = new(text);
            public CommentAttribute(string text, Shades tint) : this(text) => Tint = ColorLibrary.Pick(tint);
            public CommentAttribute(string text, Shades tint, FontStyle style) : this(text, tint) => Style = style;
      }
}