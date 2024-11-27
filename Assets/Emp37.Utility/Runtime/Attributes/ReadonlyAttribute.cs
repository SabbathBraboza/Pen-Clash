using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute for disabling a field in the inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field)]
      public class ReadonlyAttribute : PropertyAttribute
      {
            public readonly bool ExclusiveToPlaymode;

            public ReadonlyAttribute()
            {
            }
            /// <param name="exclusiveToPlaymode">If set to <c>true</c>, the serialized property is read-only only during play mode.</param>
            public ReadonlyAttribute(bool exclusiveToPlaymode) => ExclusiveToPlaymode = exclusiveToPlaymode;
      }
}
