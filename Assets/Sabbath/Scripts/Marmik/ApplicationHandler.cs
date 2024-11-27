using UnityEngine;

public class ApplicationHandler : MonoBehaviour
{
      public bool Logging;

      private void Start()
      {
            Application.targetFrameRate = 400;
            Debug.unityLogger.logEnabled = Logging;
      }
}