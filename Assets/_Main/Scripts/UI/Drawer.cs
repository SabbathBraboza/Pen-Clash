using UnityEngine;

using Emp37.Utility;

namespace PenClash.UI
{
      [RequireComponent(typeof(RectTransform))]
      public class Drawer : Content
      {
            [Title("B A S E")]
            [SerializeField] private RectTransform[] options;

            [Title("T W E E N")]
            [SerializeField] private LeanTweenType tweenType = LeanTweenType.linear;
            [SerializeField] private float duration = 1F, interval = 0.2F;


            protected override void Show(bool value)
            {
                  float getOffset(bool value) => value ? transform.rect.width : 0F;

                  var position = transform.anchoredPosition;
                  position.x = getOffset(value);
                  transform.anchoredPosition = position;
                  transform.LeanMoveX(getOffset(!value), duration).setEase(tweenType);

                  if (!value) return;
                  float delay = 0F;
                  foreach (var option in options)
                  {
                        delay += interval;
                        option.localScale = Vector3.zero;
                        option.LeanScale(Vector3.one, duration).setDelay(delay).setEase(tweenType);
                  }
            }
      }
}