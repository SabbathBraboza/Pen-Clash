//using UnityEngine;
//using UnityEngine.Events;

//namespace Emp37.Utility.Tween
//{
//      internal abstract class Builder<T>
//      {
//            private protected readonly Transform transform;
//            private protected T A, B;
//            private readonly float duration;
//            private readonly Ease.Type type;

//            private float elapsed;

//            private float delay, overshoot = 1F;
//            private TimeMode delta;
//            private UnityAction onComplete = delegate { };

//            private bool init;


//            public Builder(Transform transform, T target, float duration, Ease.Type type)
//            {
//                  this.transform = transform;
//                  B = target;
//                  this.duration = duration;
//                  this.type = type;
//            }

//            public void Update()
//            {
//                  float deltaTime = delta switch
//                  {
//                        TimeMode.Unscaled => Time.unscaledDeltaTime,
//                        _ => Time.deltaTime,
//                  };

//                  if (delay > 0F)
//                  {
//                        delay -= deltaTime;
//                        return;
//                  }
//                  if (!init)
//                  {
//                        Initialize();
//                        init = true;
//                  }
//                  if (elapsed < 1F)
//                  {
//                        elapsed = Mathf.Clamp01(elapsed + deltaTime / duration);
//                        OnEase(value: Ease.EasedRatio(elapsed, type, overshoot));
//                  }
//                  else
//                  {
//                        onComplete.Invoke();
//                  }
//            }

//            private protected abstract void Initialize();
//            private protected abstract float OnEase(float value);


//            #region C H A I N E D   M E T H O D S
//#pragma warning disable IDE1006 // Naming Styles
//            public Builder<T> setDelay(float value)
//            {
//                  delay = value;

//                  return this;
//            }
//            public Builder<T> setMode(TimeMode mode)
//            {
//                  delta = mode;

//                  return this;
//            }
//            public Builder<T> setOvershoot(float value)
//            {
//                  overshoot = value;

//                  return this;
//            }
//            public Builder<T> setOnComplete(UnityAction action)
//            {
//                  onComplete = action;

//                  return this;
//            }
//#pragma warning restore IDE1006 // Naming Styles
//            #endregion
//      }
//}