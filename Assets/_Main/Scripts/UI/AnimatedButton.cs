using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Emp37.Utility;

namespace PenClash.UI
{
      [RequireComponent(typeof(RectTransform), typeof(Button))]
      public class AnimatedButton : MonoBehaviour
      {
            [Title("B A S E")]
            [SerializeField] private new RectTransform transform;
            [SerializeField] private Button button;

            [Space(10F)]
            public UnityEvent OnClick;

            [Title("T W E E N")]
            [SerializeField] private float duration = 1F;
            [SerializeField, Min(0F)] private Vector3 originalScale = Vector3.one, targetScale = Vector3.one;
            [SerializeField] private LeanTweenType tweenType = LeanTweenType.linear;


            private void Reset()
            {
                  transform = base.transform as RectTransform;
                  button = GetComponent<Button>();
            }
            private void OnEnable()
            {
                  button.onClick.AddListener(Click);
            }
            private void OnDisable()
            {
                  button.onClick.RemoveListener(Click);
            }

            private void Click() => transform.LeanScale(targetScale, duration).setEase(tweenType).setIgnoreTimeScale(true).setOnComplete(() => transform.LeanScale(originalScale, duration).setEase(tweenType).setIgnoreTimeScale(true).setOnComplete(() => OnClick.Invoke()));
      }
}