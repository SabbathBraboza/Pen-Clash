using UnityEngine;

namespace Emp37.Utility.Singleton
{
      /// <summary>
      /// A dynamic MonoBehaviour-based singleton to use at runtime.
      /// </summary>
      /// <remarks>
      /// <b>NOTE:</b> This implementation -
      /// <br>• destroys any duplicate gameObject of type T instantiated at runtime.</br>
      /// <br>• supports implicit placement of the component T on the scene.</br>
      /// </remarks>
      public abstract class Dynamic<T> : MonoBehaviour where T : Dynamic<T>
      {
            private static T instance; public static T Instance => instance ??= Application.isPlaying ? (FindObjectOfType<T>(false) ?? new GameObject(typeof(T).Name + " : Singleton").AddComponent<T>()) : null;

            protected void Initialize(bool persistent)
            {
                  if (instance != null && instance != this)
                  {
                        string type = typeof(T).FullName;

                        Debug.LogWarning($"Duplicate instance of type '{type}' detected, destroying gameObject '{name}'.");
                        Destroy(gameObject);
                        Debug.Log($"Instance of type '{type}' is currently set at object named: '{Instance.name}'.", Instance);
                        return;
                  }
                  instance = this as T;
                  if (persistent) DontDestroyOnLoad(gameObject);
            }
            protected virtual void OnDestroy() => instance = instance == this ? null : instance;
      }
}