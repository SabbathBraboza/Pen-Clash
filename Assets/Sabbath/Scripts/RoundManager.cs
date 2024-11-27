using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    [SerializeField] private int totalRounds = 3;
    private int[] roundWins; // Index 0 for player 1, index 1 for player 2

    public delegate void RoundEndEvent();
    public event RoundEndEvent OnRoundEnd;

    void Awake()
    {
        Instance = this;
        ResetRoundWins();
    }

    public void PlayerWonRound(int playerIndex)
    {
        roundWins[playerIndex]++;
        if (roundWins[playerIndex] == totalRounds)
        {
            // Trigger game over, player wins
            Debug.Log("Player " + (playerIndex + 1) + " wins the game!");
            // Trigger win screen
            if (OnRoundEnd != null)
                OnRoundEnd();
        }
        else
        {
            // Trigger round end, continue to next round
            if (OnRoundEnd != null)
                OnRoundEnd(); // Indicates round end, but no winner yet
        }
    }

    public void ResetRoundWins()
    {
        roundWins = new int[2]; // Initialize array with size for two players
        for (int i = 0; i < roundWins.Length; i++)
        {
            roundWins[i] = 0;
        }
    }

    public int GetRoundWins(int playerIndex)
    {
        return roundWins[playerIndex];
    }

    public int GetTotalRounds()
    {
        return totalRounds;
    }
}
