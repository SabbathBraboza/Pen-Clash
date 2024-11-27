using System;

using UnityEngine;

namespace Emp37.Utility
{
      [AttributeUsage(AttributeTargets.Field)]
      public class RequireObjectAttribute : PropertyAttribute
      {
            public readonly string Message = "This field requires an assigned object.";

            public RequireObjectAttribute()
            {
            }
            public RequireObjectAttribute(string message) => Message = message;
      }
}