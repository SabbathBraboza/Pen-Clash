using UnityEngine;
using UnityEngine.UI;

namespace PenClash.UI
{
      public class GameOver : Content
      {
            [SerializeField] private Image raycastBlocker;

            [SerializeField] private float duration = 0.2F;
            [SerializeField] private LeanTweenType tweenType = LeanTweenType.linear;


            protected override void Show(bool value)
            {
                  raycastBlocker.enabled = value;

                  transform.localScale = value ? Vector3.zero : Vector3.one;
                  transform.LeanScale(value ? Vector3.one : Vector3.zero, duration).setEase(tweenType).setIgnoreTimeScale(true);
            }
      }
}