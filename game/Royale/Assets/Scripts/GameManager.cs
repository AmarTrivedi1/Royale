using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public int player1Score = 0; // Score for player 1
    public int player2Score = 0; // Score for player 2

    public GameObject victoryPopup; // GameObject for displaying victory popup
    public TMP_Text victoryText; // TextMeshPro text for displaying victory message

    public TMP_Text player1ScoreText; // TextMeshPro text for displaying player 1's score
    public TMP_Text player2ScoreText; // TextMeshPro text for displaying player 2's score

    // Update will be called once per frame for tower counts
    void Update()
    {
        player1ScoreText.text = $"Player 1 Score: {player1Score}";
        player2ScoreText.text = $"Player 2 Score: {player2Score}";
    }

    // The Awake function ensures there's only one GameManager instance in the game.
    // This prevents duplicate GameManager objects and maintains game state consistency.
    void Awake()
    {
        if (instance == null)
            instance = this; // Set this instance as the singleton instance
        else if (instance != this)
            Destroy(gameObject); // If another instance exists, destroy this one

        DontDestroyOnLoad(gameObject);
    }

    // Call this method when a tower is destroyed
    public void OnTowerDestroyed(int playerNum)
    {
        // Update score based on which player's tower was destroyed
        if (playerNum == 1)
            player2Score++; // Increment player 2's score if player 1's tower was destroyed
        else if (playerNum == 2)
            player1Score++; // Increment player 1's score if player 2's tower was destroyed

        // Check victory condition only when both player1 and player2 have a score of 2
        if (player1Score == 2 && player2Score == 2)
        {
            // Display victory message for a tie
            victoryPopup.SetActive(true); // Activate the victory popup
            victoryText.gameObject.SetActive(true); // Activate the victory text
            victoryText.text = "It's a Tie!"; // Set the victory message
        }
        else if (player1Score == 2)
        {
            victoryPopup.SetActive(true);
            victoryText.gameObject.SetActive(true);
            victoryText.text = "Player 1 Wins!"; 
        }
        else if (player2Score == 2)
        {
           
            victoryPopup.SetActive(true); 
            victoryText.gameObject.SetActive(true); 
            victoryText.text = "Player 2 Wins!"; 
        }
    }
}
