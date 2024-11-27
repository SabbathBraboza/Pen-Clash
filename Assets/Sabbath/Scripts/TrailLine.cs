using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class trailline : MonoBehaviour
{
      private LineRenderer lr;

      void Awake() => lr = GetComponent<LineRenderer>();

      public void RenderLine(Vector3 startPoint, Vector3 endPoint)
      {
            lr.positionCount = 2;
            lr.SetPositions(new Vector3[] { startPoint, endPoint });
      }

      public void ClearLine() => lr.positionCount = 0;
}