using UnityEngine;
using UnityEngine.Events;

using Emp37.Utility;

namespace PenClash.UI.Menu
{
      public class Transition : Base
      {
            [SerializeField] private CanvasGroup background;
            [SerializeField] private RectTransform logo;

            [SerializeField] private float delay;
            [SerializeField, Readonly] private float elapsed;

            public UnityEvent OnComplete;


            protected override void OnEnable()
            {
                  base.OnEnable();
                  elapsed = 0F;

                  background.alpha = 0F;
                  background.LeanAlpha(1F, 0.2F).setEaseOutQuart();

                  logo.localScale = Vector3.zero;
                  logo.LeanScale(Vector3.one, 0.5F).setEaseOutBounce();
            }
            private void Update()
            {
                  elapsed += Time.deltaTime;
                  if (elapsed > delay)
                  {
                        OnComplete.Invoke();
                        enabled = false;
                  }
            }
      }
}