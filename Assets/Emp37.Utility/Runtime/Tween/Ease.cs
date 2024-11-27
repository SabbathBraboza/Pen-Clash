using static System.MathF;

namespace Emp37.Utility.Tween
{
      public static class Ease
      {
            public enum Type
            {
                  Linear,
                  InSine,
                  OutSine,
                  InOutSine,
                  InCubic,
                  OutCubic,
                  InOutCubic,
                  InQuint,
                  OutQuint,
                  InOutQuint,
                  InCirc,
                  OutCirc,
                  InOutCirc,
                  InQuad,
                  OutQuad,
                  InOutQuad,
                  InQuart,
                  OutQuart,
                  InOutQuart,
                  InExpo,
                  OutExpo,
                  InOutExpo,
                  InBounce,
                  OutBounce,
                  InOutBounce,
                  InBack,
                  OutBack,
                  InOutBack,
                  InElastic,
                  OutElastic,
                  InOutElastic,
                  BreakOutBounce,
            }

            #region E A S I N G   F U N C T I O N S
            private static float Linear(float x) => x;

            // S I N E
            private static float EaseInSine(float x)
            {
                  x *= PI * 0.5F;

                  return 1F - Cos(x);
            }
            private static float EaseOutSine(float x)
            {
                  x *= PI * 0.5F;

                  return Sin(x);
            }
            private static float EaseInOutSine(float x)
            {
                  x *= PI;

                  return (Cos(x) - 1F) * -0.5F;
            }

            // C U B I C
            private static float EaseInCubic(float x)
            {
                  return x * x * x;
            }
            private static float EaseOutCubic(float x)
            {
                  x--;

                  return x * x * x + 1F;
            }
            private static float EaseInOutCubic(float x)
            {
                  if (x < 0.5F)
                  {
                        return 4F * x * x * x;
                  }
                  else
                  {
                        x = -2F * x + 2F;

                        return 1F - (x * x * x * 0.5F);
                  }
            }

            // Q U I N T
            private static float EaseInQuint(float x)
            {
                  return x * x * x * x * x;
            }
            private static float EaseOutQuint(float x)
            {
                  x = 1F - x;

                  return 1F - x * x * x * x * x;
            }
            private static float EaseInOutQuint(float x)
            {
                  if (x < 0.5F)
                  {
                        return 16F * x * x * x * x * x;
                  }
                  else
                  {
                        x = (x * -2F) + 2F;

                        return 1F - (x * x * x * x * x * 0.5F);
                  }
            }

            // C I R C
            private static float EaseInCirc(float x)
            {
                  if (x <= 1F)
                  {
                        return 1F - Sqrt(1F - x * x);
                  }
                  else
                  {
                        return 1F;
                  }
            }
            private static float EaseOutCirc(float x)
            {
                  x--;

                  return Sqrt(1F - x * x);
            }
            private static float EaseInOutCirc(float x)
            {
                  if (x < 0.5F)
                  {
                        x *= 2F;

                        return 0.5F * (1F - Sqrt(1F - (x * x)));
                  }
                  else
                  {
                        x = -2F * x + 2F;

                        return 0.5F * (Sqrt(1F - (x * x)) + 1F);
                  }
            }

            // Q U A D
            private static float EaseInQuad(float x)
            {
                  return x * x;
            }
            private static float EaseOutQuad(float x)
            {
                  x = 1F - x;

                  return 1F - x * x;
            }
            private static float EaseInOutQuad(float x)
            {
                  if (x < 0.5F)
                  {
                        return x * x * 2F;
                  }
                  else
                  {
                        x = (-2F * x) + 2F;

                        return 1F - (x * x * 0.5F);
                  }
            }

            // Q U A R T
            private static float EaseInQuart(float x)
            {
                  return x * x * x * x;
            }
            private static float EaseOutQuart(float x)
            {
                  x--;

                  return -(x * x * x * x - 1F);
            }
            private static float EaseInOutQuart(float x)
            {
                  if (x < 0.5F)
                  {
                        return 8F * x * x * x * x;
                  }
                  else
                  {
                        x = (-2F * x) + 2F;

                        return 1F - (x * x * x * x * 0.5F);
                  }
            }

            // E X P O
            private static float EaseInExpo(float x)
            {
                  return Pow(2F, 10F * x - 10F);
            }
            private static float EaseOutExpo(float x)
            {
                  if (x == 1F)
                  {
                        return 1F;
                  }
                  else
                  {
                        return 1F - Pow(2F, -10F * x);
                  }
            }
            private static float EaseInOutExpo(float x)
            {
                  if (x == 0F) return 0F; else if (x == 1F) return 1F;

                  float p = 20F * x - 10F;

                  if (x < 0.5F)
                  {
                        return Pow(2F, p) * 0.5F;
                  }
                  else
                  {
                        return (2F - Pow(2F, -p)) * 0.5F;
                  }
            }

            // B O U N C E
            private static float EaseInBounce(float x)
            {
                  return 1F - EaseOutBounce(1F - x);
            }
            private static float EaseOutBounce(float x)
            {
                  const float B1 = 2.75F, B2 = 7.5625F;

                  return x < 1F / B1 ? B2 * x * x : x < 2F / B1 ? B2 * (x -= 1.5F / B1) * x + 0.75F : x < 2.5F / B1 ? B2 * (x -= 2.25F / B1) * x + 0.9375F : B2 * (x -= 2.625F / B1) * x + 0.984375F;
            }
            private static float EaseInOutBounce(float x)
            {
                  return (x < 0.5F ? 1F - EaseOutBounce(1F - (2F * x)) : 1F + EaseOutBounce((2F * x) - 1F)) * 0.5F;
            }

            // B A C K
            private const float Back = 1.70158F;

            private static float EaseInBack(float x, float overshoot)
            {
                  float spring = Back * overshoot;

                  return x * x * ((spring + 1F) * x - spring);
            }
            private static float EaseOutBack(float x, float overshoot)
            {
                  float spring = Back * overshoot;

                  x--;

                  return x * x * ((spring + 1F) * x + spring) + 1F;
            }
            private static float EaseInOutBack(float x, float overshoot)
            {
                  float spring = Back * overshoot * 1.525F;

                  if (x < 0.5F)
                  {
                        x *= 2F;

                        return x * x * ((spring + 1F) * x - spring) * 0.5F;
                  }
                  else
                  {
                        x = (2F * x) - 2F;

                        return (x * x * ((spring + 1F) * x + spring) + 2F) * 0.5F;
                  }
            }

            // E L A S T I C
            private static float EaseInElastic(float x, float overshoot, float period = 0.3F)
            {
                  if (x == 0F) return 0F; else if (x == 1F) return 1F;

                  float c = 2F * PI / period;

                  if (overshoot > 1F && x > 0.6F)
                  {
                        overshoot = 1F + ((1F - x) / 0.4F * (overshoot - 1F));
                  }

                  x--;

                  return Pow(2F, 10F * x) * Sin((x - c) * c) * overshoot;
            }
            private static float EaseOutElastic(float x, float overshoot, float period = 0.3F)
            {
                  if (x == 0F) return 0F; else if (x == 1F) return 1F;

                  float c = 2F * PI / period;

                  if (overshoot > 1F && x < 0.4F)
                  {
                        overshoot = 1F + (x / 0.4F * (overshoot - 1F));
                  }

                  return 1F + Pow(2F, -10F * x) * Sin((x - 0.075F) * c) * overshoot;
            }
            public static float EaseInOutElastic(float x, float overshoot, float period = 0.5F)
            {
                  if (x == 0F) return 0F; else if (x == 1F) return 1F;

                  float c = 2F * PI / period, s = period / 4F;

                  x *= 2F;

                  if (overshoot > 1F)
                  {
                        if (x < 0.2F)
                        {
                              overshoot = 1F + (x / 0.2F * (overshoot - 1F));
                        }
                        else
                        if (x > 0.8F)
                        {
                              overshoot = 1F + ((1F - x) / 0.2F * (overshoot - 1F));
                        }
                  }

                  if (x-- < 1F)
                  {
                        return -0.5F * (Pow(2F, 10F * x) * Sin((x - s) * c)) * overshoot;
                  }
                  else
                  {
                        return 1F + Pow(2F, -10F * x) * Sin((x - s) * c) * 0.5F * overshoot;
                  }
            }

            // C U S T O M
            private static float BreakOutBounce(float x)
            {
                  return (x < 0.5F ? 1F - EaseInBounce(1F - (2F * x)) : 1F + EaseOutBounce((2F * x) - 1F)) * 0.5F;
            }
            #endregion

            /// <summary>
            /// Simulates transition of a value on a non-linear path.
            /// </summary>
            /// <param name="value">Normalized point on a linear path.</param>
            /// <param name="type">Type of curve to be simulated.</param>
            /// <returns>Corresponding point on a selected type path.</returns>
            public static float EasedRatio(float value, Type type, float overshoot = 1F) => type switch
            {
                  Type.Linear => Linear(x: value),
                  Type.InSine => EaseInSine(x: value),
                  Type.OutSine => EaseOutSine(x: value),
                  Type.InOutSine => EaseInOutSine(x: value),
                  Type.InCubic => EaseInCubic(x: value),
                  Type.OutCubic => EaseOutCubic(x: value),
                  Type.InOutCubic => EaseInOutCubic(x: value),
                  Type.InQuint => EaseInQuint(x: value),
                  Type.OutQuint => EaseOutQuint(x: value),
                  Type.InOutQuint => EaseInOutQuint(x: value),
                  Type.InCirc => EaseInCirc(x: value),
                  Type.OutCirc => EaseOutCirc(x: value),
                  Type.InOutCirc => EaseInOutCirc(x: value),
                  Type.InQuad => EaseInQuad(x: value),
                  Type.OutQuad => EaseOutQuad(x: value),
                  Type.InOutQuad => EaseInOutQuad(x: value),
                  Type.InQuart => EaseInQuart(x: value),
                  Type.OutQuart => EaseOutQuart(x: value),
                  Type.InOutQuart => EaseInOutQuart(x: value),
                  Type.InExpo => EaseInExpo(x: value),
                  Type.OutExpo => EaseOutExpo(x: value),
                  Type.InOutExpo => EaseInOutExpo(x: value),
                  Type.InBack => EaseInBack(x: value, overshoot),
                  Type.OutBack => EaseOutBack(x: value, overshoot),
                  Type.InElastic => EaseInElastic(x: value, overshoot),
                  Type.OutElastic => EaseOutElastic(x: value, overshoot),
                  Type.InOutBack => EaseInOutBack(x: value, overshoot),
                  Type.InOutElastic => EaseInOutElastic(x: value, overshoot),
                  Type.InBounce => EaseInBounce(x: value),
                  Type.OutBounce => EaseOutBounce(x: value),
                  Type.InOutBounce => EaseInOutBounce(x: value),
                  Type.BreakOutBounce => BreakOutBounce(x: value),
                  _ => 1F
            };
      }
}