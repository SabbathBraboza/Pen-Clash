using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
      public Image[] player1Stars;
      public Image[] player2Stars;

      public void UpdateStars(int player1Wins, int player2Wins)
      {
            for (int i = 0; i < player1Stars.Length; i++)
            {
                  player1Stars[i].enabled = i < player1Wins;
            }

            for (int i = 0; i < player2Stars.Length; i++)
            {
                  player2Stars[i].enabled = i < player2Wins;
            }
      }
}
