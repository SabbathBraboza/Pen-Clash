using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    private RoundManager RoundManager;
      [Header("-------Panels:-------")]
      [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject GameOverPanel;

    private bool isPaused = false;

    void OnEnable()
    {
        RoundManager.Instance.OnRoundEnd += HandleRoundEnd;
    }

    void OnDisable()
    {
        RoundManager.Instance.OnRoundEnd -= HandleRoundEnd;
    }

    public void ToMainMenu() => SceneManager.LoadScene("Menu");
      public void ToHome()
      {
       SceneManager.LoadScene("Menu");
       Time.timeScale = 1f;
      }
      public void Quit()
      {
            Application.Quit();
            Debug.Log("GGs");
      }

      public void GamePaused()
      {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
      }

    public void Reload() => SceneManager.LoadScene("Gameplay");
    
      private void HandleRoundEnd()
      {
            int player1Wins = RoundManager.Instance.GetRoundWins(0);
            int player2Wins = RoundManager.Instance.GetRoundWins(1);

        if (player1Wins == RoundManager.Instance.GetTotalRounds() || player2Wins == RoundManager.Instance.GetTotalRounds())
        {
            // Game over, show win screen
            WinScreen.SetActive(true);
        }
        else { }
      }
}
