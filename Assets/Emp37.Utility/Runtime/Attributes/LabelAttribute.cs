using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute used to modify the label of a serialized property.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field)]
      public class LabelAttribute : PropertyAttribute
      {
            public readonly GUIContent Label;

            public LabelAttribute(string text)
            {
                  Label = new(text);
                  order = -20;
            }
      }
}