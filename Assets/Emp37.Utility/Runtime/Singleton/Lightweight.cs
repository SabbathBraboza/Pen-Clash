using UnityEngine;

namespace Emp37.Utility.Singleton
{
      /// <summary>
      /// A simple MonoBehaviour-based singleton to use at runtime.
      /// </summary>
      /// <remarks>
      /// <b>NOTE:</b> This implementation -
      /// <br>• does not support the 'Recompile and Continue Playing' value in <i>Preferences > General > Script Changes While Playing</i>.</br>
      /// <br>• requires explicit placement of component T on the scene.</br>
      /// </remarks>
      public abstract class Lightweight<T> : MonoBehaviour where T : Lightweight<T> // ~Hextant Studios
      {
            public static T Instance { get; private set; }

            protected virtual void Awake()
            {
                  Debug.Assert(condition: Instance == null, message: $"Multiple instances of type '{typeof(T).Name}' exist on this scene.", context: this);

                  Instance = this as T;
            }
            protected virtual void OnDestroy() => Instance = null;
      }
}