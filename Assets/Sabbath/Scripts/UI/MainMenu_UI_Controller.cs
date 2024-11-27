using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_UI_Controller : MonoBehaviour
{
      [Header("-------Panels:-------")]
      [SerializeField] private GameObject LoadingScreenPanel;
      [SerializeField] private GameObject SelectionPanel;
      [SerializeField] private GameObject MainMenuPanel;


      [Header("-------GameObjects:-------")]
      [SerializeField] private GameObject Player1;
      [SerializeField] private GameObject Player2;
      [SerializeField] private GameObject Board;
      [SerializeField] private RectTransform playButton;

      [Header("-------loading Bar:-------")]
      public Image LoadingBar; // The UI Image that serves as the loading bar
      public float WaitTime = 5.0f; // Total time for the cooldown

      public void PlayGame()
      {
            SelectionPanel.SetActive(false);
            Invoke(nameof(MainGame), 3f);
      }

      public void Play()
      {
            MainMenuPanel.SetActive(false);
            LoadingScreenPanel.SetActive(true);
            Invoke("SelectPanel", 2f);
      }
      public void SelectPanel()
      {
            LoadingScreenPanel.SetActive(false);
            SelectionPanel.SetActive(true);
            Player1.SetActive(true);
            Player2.SetActive(true);
            Board.SetActive(true);
      }

      public void ToMainMenu() => SceneManager.LoadScene("Main");

      public void MainGame() => SceneManager.LoadScene("Gameplay");

      public void Quit()
      {
            Application.Quit();
            Debug.Log("GGs");
      }
}

