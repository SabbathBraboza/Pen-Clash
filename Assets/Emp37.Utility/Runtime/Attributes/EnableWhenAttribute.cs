using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute to conditionally enable the associated field in the Inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
      public class EnableWhenAttribute : Attribute
      {
            public readonly string ConditionName;

            /// <param name="condition">The name of the boolean member type as (field, property or method) on this target.</param>
            public EnableWhenAttribute(string condition) => ConditionName = condition;
      }
}