using UnityEngine;

namespace PenClash.UI.Menu
{
      public class Base : MonoBehaviour
      {
            [SerializeField] private Behaviour[] syncables;


            protected virtual void OnEnable()
            {
                  foreach (var syncable in syncables)
                  {
                        syncable.enabled = true;
                  }
            }
            protected virtual void OnDisable()
            {
                  foreach (var syncable in syncables)
                  {
                        syncable.enabled = false;
                  }
            }
      }
}