using System;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute for hiding the default script field on a MonoBehaviour's inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Class, Inherited = false)]
      public class HideDefaultScriptAttribute : Attribute
      {
      }
}