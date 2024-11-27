using UnityEngine;

using Emp37.Utility;

namespace PenClash.Data
{
      public abstract class Data : ScriptableObject
      {
            [SerializeField, Readonly] private int current;
            public virtual int Current { get => current; set => current = value; }
      }
}