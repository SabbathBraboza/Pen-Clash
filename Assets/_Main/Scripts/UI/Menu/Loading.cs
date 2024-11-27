using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Emp37.Utility;

namespace PenClash.UI.Menu
{
      public class Loading : Base
      {
            [SerializeField] private RectTransform red, blue, thunder, versus, p1, p2;
            [SerializeField] private CanvasGroup dots, lines;
            [SerializeField] private GameObject message;
            private Animator versusAnim;

            private const float offset = 50F;
            private readonly WaitForSeconds loadDuration = new(2.25F);

            [Title("T W E E N")]
            [SerializeField] private float duration = 0.2F;
            [SerializeField] private LeanTweenType tweenType;


            private void Awake()
            {
                  versusAnim = versus.GetComponent<Animator>();
            }
            protected override void OnEnable()
            {
                  base.OnEnable();

                  Vector3 position;
                  float original, width = Screen.width;

                  position = red.localPosition;
                  position.x = -width;
                  red.localPosition = position;

                  position = blue.localPosition;
                  position.x = width;
                  blue.localPosition = position;

                  position = p1.localPosition;
                  original = position.x;
                  position.x -= p1.rect.width + offset;
                  p1.localPosition = position;
                  p1.LeanMoveLocalX(original, duration).setEase(tweenType).setDelay(duration);

                  position = p2.localPosition;
                  original = position.x;
                  position.x += p2.rect.width + offset;
                  p2.localPosition = position;
                  p2.LeanMoveLocalX(original, duration).setEase(tweenType).setDelay(duration);

                  thunder.localScale = versus.localScale = Vector3.zero;

                  lines.alpha = dots.alpha = 0F;

                  red.LeanMoveLocalX(0F, duration).setEase(tweenType);
                  blue.LeanMoveLocalX(0F, duration).setEase(tweenType);
                  thunder.LeanScale(Vector3.one, duration).setEase(tweenType);

                  versusAnim.enabled = false;
                  versus.LeanScale(Vector3.one, duration).setEase(tweenType).setDelay(duration).setOnComplete(() => versusAnim.enabled = true);

                  float half = 0.5F * duration;
                  lines.LeanAlpha(1F, half).setEase(tweenType).setDelay(half);
                  dots.LeanAlpha(1F, duration).setEase(tweenType).setDelay(duration);

                  StartCoroutine(Load);
            }

            private IEnumerator Load
            {
                  get
                  {
                        yield return loadDuration;

                        var operation = SceneManager.LoadSceneAsync("Gameplay");
                        operation.allowSceneActivation = false;

                        while (!operation.isDone)
                        {
                              if (operation.progress >= 0.9F)
                              {
                                    message.SetActive(true);
                                    operation.allowSceneActivation = Input.touchCount > 0;
                              }
                              yield return null;
                        }
                  }
            }
      }
}