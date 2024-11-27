using UnityEngine;

namespace Emp37.Utility.Editor
{
      internal abstract class BaseDecoratorDrawer : UnityEditor.DecoratorDrawer
      {
            private bool init;

            public virtual void Initialize() { }
            public override void OnGUI(Rect position)
            {
                  if (!init)
                  {
                        Initialize();
                        init = true;
                  }
            }
      }
}