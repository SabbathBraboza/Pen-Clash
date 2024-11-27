using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    public static PlayerTurnManager Instance;

    [Header("Parameters:")]
    public int currentPlayer = 1;
    [SerializeField] private float baseTurnTimeLimit = 20f, penaltyTime = 5f;
    private float[] playerTurnTimeLimits;
    private float[] playerPenalties;

    void Awake()
    {
        Instance = this;
        playerTurnTimeLimits = new float[2] { baseTurnTimeLimit, baseTurnTimeLimit };
        playerPenalties = new float[2] { 0f, 0f };
    }

    public void SwitchPlayerTurn(bool missedTurn, int player)  // Switch player turn
    {
        if (missedTurn)
        {
            playerPenalties[player - 1] += penaltyTime;
            playerTurnTimeLimits[player - 1] -= penaltyTime;
            if (playerTurnTimeLimits[player - 1] <= 0)
            {
                Debug.Log("Player " + player + " loses!");
                return;
            }
        }
        currentPlayer = player == 1 ? 2 : 1;
        Debug.Log("Switching to Player " + currentPlayer + "'s turn.");
    }

    // Get turn time limit for the specified player
    public float GetTurnTimeLimit(int player) => Mathf.Max(baseTurnTimeLimit - playerPenalties[player - 1], 0f);
}
