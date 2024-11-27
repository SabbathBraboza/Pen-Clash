using UnityEngine;

namespace PenClash.UI
{
      public class Achievements : Content
      {
            [SerializeField] private float duration = 0.2F;


            protected override void Show(bool value)
            {
                  static Vector3 getScale(bool value) => value ? Vector3.zero : Vector3.one;

                  transform.localScale = getScale(value);
                  transform.LeanScale(getScale(!value), duration).setEase(value ? LeanTweenType.easeOutBack : LeanTweenType.easeInBack);
            }
      }
}