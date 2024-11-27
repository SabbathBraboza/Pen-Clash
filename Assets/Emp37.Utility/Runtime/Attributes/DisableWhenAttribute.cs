using System;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute to conditionally disable the associated field in the Inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
      public class DisableWhenAttribute : Attribute
      {
            public readonly string ConditionName;

            /// <param name="condition">The name of the boolean member type as (field, property or method) on this target.</param>
            public DisableWhenAttribute(string condition) => ConditionName = condition;
      }
}