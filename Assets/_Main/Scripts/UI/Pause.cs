using UnityEngine;
using UnityEngine.UI;

namespace PenClash.UI
{
      public class Pause : Content
      {
            [SerializeField] private Image raycastBlocker;


            protected override void Show(bool value)
            {
                  raycastBlocker.enabled = value;
                  transform.localScale = value ? Vector3.zero : Vector3.one;
                  transform.LeanScale(value ? Vector3.one : Vector3.zero, 0.2F).setEase(value ? LeanTweenType.easeOutBack : LeanTweenType.easeInBack).setIgnoreTimeScale(true);
            }
      }
}