using System;

using UnityEngine;

namespace Emp37.Utility
{
      [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
      public class HelpBoxAttribute : PropertyAttribute
      {
            public readonly string Message;
            public readonly MessageType MessageType;
            public readonly float Height = 37F;

            public HelpBoxAttribute(string message) => Message = message;
            public HelpBoxAttribute(string message, MessageType type) : this(message) => MessageType = type;
            public HelpBoxAttribute(string message, MessageType type, Size size) : this(message, type) => Height = size switch { Size.Small => 34F, Size.Medium => 48F, Size.Large => 64F, _ => Height };
      }
}