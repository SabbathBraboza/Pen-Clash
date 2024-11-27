using System;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute to control the visibility of a field in the inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false)]
      public class ShowWhenAttribute : Attribute
      {
            public readonly string ConditionName;

            /// <param name="condition">The name of the boolean member type as (field, property or method) on this target.</param>
            public ShowWhenAttribute(string condition) => ConditionName = condition;
      }
}