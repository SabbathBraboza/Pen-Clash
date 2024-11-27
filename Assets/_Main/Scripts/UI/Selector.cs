using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using Emp37.Utility;

namespace PenClash.UI
{
      public class Selector : MonoBehaviour
      {
            [Title("R E F E R E N C E S", Shades.Dandelion)]
            [SerializeField] private Image effector;
            [SerializeField] private new TMP_Text name;
            [SerializeField] private RectTransform a, b;

            [Title("T W E E N   V A L U E S", Shades.Dandelion)]
            [SerializeField] private float duration = 0.2F;
            [SerializeField] private LeanTweenType tweenType;

            [Title("E V E N T S", Shades.Dandelion)]
            public UnityEvent OnNext;
            public UnityEvent OnPrevious;


            private void OnDisable()
            {
                  OnNext.RemoveAllListeners();
                  OnPrevious.RemoveAllListeners();
            }

            public void Next()
            {
                  if (tweenType == LeanTweenType.notUsed)
                  {
                        OnNext.Invoke();
                        return;
                  }

                  var transform = effector.transform;
                  transform.LeanMoveLocalX(b.localPosition.x, duration).setEase(tweenType).setOnComplete(() =>
                  {
                        var position = transform.localPosition;
                        position.x = a.localPosition.x;
                        transform.localPosition = position;

                        OnNext.Invoke();

                        transform.LeanMoveLocalX(0F, duration).setEase(tweenType);
                  });
            }
            public void Previous()
            {
                  if (tweenType == LeanTweenType.notUsed)
                  {
                        OnPrevious.Invoke();
                        return;
                  }

                  var transform = effector.transform;
                  transform.LeanMoveLocalX(a.localPosition.x, duration).setEase(tweenType).setOnComplete(() =>
                  {
                        var position = transform.localPosition;
                        position.x = b.localPosition.x;
                        transform.localPosition = position;

                        OnPrevious.Invoke();

                        transform.LeanMoveLocalX(0F, duration).setEase(tweenType);
                  });
            }
            public void SetInfo(Sprite sprite, string text)
            {
                  effector.sprite = sprite;
                  name.text = text;
            }
      }
}