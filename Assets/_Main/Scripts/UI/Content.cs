using UnityEngine;

namespace PenClash.UI
{
      public abstract class Content : MonoBehaviour
      {
            [SerializeField] protected new RectTransform transform;


            protected virtual void Reset()
            {
                  transform = base.transform as RectTransform;
            }
            protected virtual void OnEnable()
            {
                  Show(true);
            }
            protected virtual void OnDisable()
            {
                  Show(false);
            }

            protected abstract void Show(bool value);
            public void Toggle() => enabled = !enabled;
      }
}