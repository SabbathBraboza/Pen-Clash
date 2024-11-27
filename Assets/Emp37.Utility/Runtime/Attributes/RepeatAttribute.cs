using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute used to make a serialized float or interger field value to loop between specified values.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field)]
      public class RepeatAttribute : PropertyAttribute
      {
            public readonly float Min, Max;

            public RepeatAttribute(float minInclusive, float maxExclusive)
            {
                  Min = minInclusive; Max = maxExclusive;
                  order = -10;
            }
      }
}