using PenClash.Data;
using PenClash.UI;
using System.ComponentModel;
using UnityEngine;

public class BorderDetect : MonoBehaviour
{
      [Header("------------GameObjects:-----------")]
      [SerializeField] private GameObject lose;
      [SerializeField] private GameObject timerText;
      [SerializeField] private GameObject player1;
      [SerializeField] private GameObject player2;
      [SerializeField] private GameOver GameOverPanel;
      [SerializeField] private GameObject Slider;
      [SerializeField] private GameObject p1s1;
      [SerializeField] private GameObject p1s2;
      [SerializeField] private GameObject p2s1;
      [SerializeField] private GameObject p2s2;

      [Header("--------Text--------")]
      [SerializeField] private TMPro.TextMeshProUGUI RoundWinnerText;
      [SerializeField] private TMPro.TextMeshProUGUI GameOverText;
      [SerializeField] private TMPro.TextMeshProUGUI RoundText;

      [Header("------------Particle System------------")]
      [SerializeField] private ParticleSystem GameOverEffects;

      [Header("---------Board Selection--------")]
      [SerializeField] private BoardData boardData;
      public SpriteRenderer BoardSprite;

      private bool roundInProgress = false;
      private bool gameOver = false;

      private int roundCount = 0;
      private int player1Wins = 0;
      private int player2Wins = 0;
      private int p1Health;
      private int p2Health;
      private float timer = 0f;

      private Vector3 player1StartPosition = new Vector3(-6f, 0f, 0f);
      private Vector3 player2StartPosition = new Vector3(6f, 0f, 0f);
      private int SelectionOption = 0;

      private void Start() => UpdateSprite();

      private void Awake() => RoundWinnerText = lose.GetComponent<TMPro.TextMeshProUGUI>();

      private void OnTriggerEnter2D(Collider2D collision)
      {
            if (!roundInProgress && !gameOver)
                  StartRound();
      }

      private void OnTriggerExit2D(Collider2D collision)
      {
            if (roundInProgress && !gameOver)
            {
                  roundInProgress = false;
                  lose.SetActive(true);
                  timerText.SetActive(false);
                  player1.SetActive(false);
                  player2.SetActive(false);
                  Slider.SetActive(false);
                  GameOverEffects.Play();  // Particle System

                  // Increment win count for the winning player
                  if (collision.gameObject.name == "Player 1")
                        player2Wins++;
                  else if (collision.gameObject.name == "Player 2")
                        player1Wins++;

                  roundCount++;

                  // Award one point to the winning player
                  if (collision.gameObject.name == "Player 1")
                  {
                        RoundWinnerText.text = "Player 2 wins the round.";
                        Debug.Log("Player 2 wins round " + roundCount + ".");
                  }
                  else if (collision.gameObject.name == "Player 2")
                  {
                        RoundWinnerText.text = "Player 1 wins the round.";
                        Debug.Log("Player 1 wins round " + roundCount + ".");
                  }

                  // Check if any player has won 2 rounds
                  if (player1Wins >= 2 || player2Wins >= 2)
                  {
                        gameOver = true;
                        Debug.Log("Player " + (player1Wins >= 2 ? "1" : "2") + " wins the game!");
                        GameOverText.text = $"Player {(player1Wins >= 2 ? '1' : '2')} wins the game!";
                        Invoke("OnGameOver", 3f); // Show Game Over panel
                  }
                  else
                  {
                        Debug.Log("Starting next round...");
                        Invoke("StartNextRound", 4f); // Start the next round after a delay
                  }
            }
      }

      private void LateUpdate()
      {
            p1Health = player1.GetComponent<Health>().Value;
            p2Health = player2.GetComponent<Health>().Value;

            if (p1Health == 0 && roundInProgress)
            {
                  timer += Time.deltaTime;
                  if (timer > 2f)
                  {
                        player1.gameObject.SetActive(false);
                        timer = 0f;
                  }
            }
            else if (p2Health == 0 && roundInProgress)
            {
                  timer += Time.deltaTime;
                  if (timer > 2f)
                  {
                        player2.gameObject.SetActive(false);
                        timer = 0f;
                  }
            }
            switch (player1Wins)
            {
                  case 1:
                        p1s1.SetActive(true); break;
                  case 2:
                        p1s2.SetActive(true); break;
            }
            switch (player2Wins)
            {
                  case 1:
                        p2s1.SetActive(true); break;
                  case 2:
                        p2s2.SetActive(true); break;
            }
      }

      private void StartRound()
      {
            roundInProgress = true;
            Debug.Log("Starting round " + (roundCount + 1) + "...");
            RoundText.text = (roundCount + 1).ToString();
      }

      private void StartNextRound()
      {
            // Reset round state
            roundInProgress = false;
            lose.SetActive(false);
            timerText.SetActive(true);
            player1.SetActive(true);
            player2.SetActive(true);
            Slider.SetActive(true);
            RoundWinnerText.text = "";
            GameOverEffects.Stop();

            // Set player positions for the next round
            player1.transform.position = player1StartPosition;
            player2.transform.position = player2StartPosition;

            Debug.Log("Resetting round for round " + (roundCount + 1) + "...");
      }

      private void OnGameOver() => GameOverPanel.enabled = true;

      private void UpdateSprite()
      {
            if (BoardSprite != null && boardData != null)
            {
                  BoardSprite.sprite = boardData.FetchBoard(boardData.Current);
            }
      }

}
