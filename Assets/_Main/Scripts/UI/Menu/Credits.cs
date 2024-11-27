using UnityEngine;
using UnityEngine.UI;

namespace PenClash.UI.Menu
{
      public class Credits : Base
      {
            [SerializeField] private Scrollbar vertical;


            private void Reset()
            {
                  vertical = GetComponentInChildren<Scrollbar>(true);
            }
            protected override void OnEnable()
            {
                  base.OnEnable();
                  vertical.value = 1F;
            }
            private void Update()
            {
                  // back button on mobile devices
                  if (Input.GetKeyDown(KeyCode.Escape))
                  {
                        enabled = false;
                  }
            }
      }
}