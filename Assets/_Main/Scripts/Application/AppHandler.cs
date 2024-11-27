using UnityEngine;

namespace PenClash
{
      public class AppHandler : MonoBehaviour
      {
            private void Awake()
            {
                  Debug.unityLogger.logEnabled = false;
                  Application.targetFrameRate = 400;
            }
      }
}