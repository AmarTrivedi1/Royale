using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    // Player 1 values.
    public GameObject[] p1Cards;
    public Transform P1SpawnTop;
    public Transform P1SpawnBot;
    private int p1SelectedCardIndex = -1;

    // Player 2 values.
    public GameObject[] p2Cards;
    public Transform P2SpawnTop;
    public Transform P2SpawnBot;
    private int p2SelectedCardIndex = -1;

    public GameManager gameManager;
    
    // Executed every frame.
    void Update()
    {
        // Player 1 controls. (1, 2, 3, 4)
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectCard(ref p1SelectedCardIndex, 0, p1Cards);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectCard(ref p1SelectedCardIndex, 1, p1Cards);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectCard(ref p1SelectedCardIndex, 2, p1Cards);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectCard(ref p1SelectedCardIndex, 3, p1Cards);
        // Player 2 controls. (7, 8, 9, 0)
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectCard(ref p2SelectedCardIndex, 0, p2Cards);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SelectCard(ref p2SelectedCardIndex, 1, p2Cards);
        if (Input.GetKeyDown(KeyCode.Alpha9)) SelectCard(ref p2SelectedCardIndex, 2, p2Cards);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SelectCard(ref p2SelectedCardIndex, 3, p2Cards);

        // Placement controls for Player 1.
        if (p1SelectedCardIndex != -1)
        {
            if (gameManager.player1Elixir > 0)
            {
                // If player 1 presses 'w' (top lane placement).
                if (Input.GetKeyDown(KeyCode.W))
                {
                    // Place the card in the corresponding postition.
                    PlaceCard(p1SelectedCardIndex, P1SpawnTop, p1Cards, 1);
                    // Reset the index after placing the card.
                    p1SelectedCardIndex = -1;

                }
                // If player 1 presses 's' (bot lane placement).
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    // Place the card in the corresponding postition.
                    PlaceCard(p1SelectedCardIndex, P1SpawnBot, p1Cards, 1);
                    // Reset the index after placing the card.
                    p1SelectedCardIndex = -1;

                }
            }
        }

        // Placement controls for Player 2.
        if (p2SelectedCardIndex != -1)
        {
            if (gameManager.player2Elixir > 0)
            {
                // If player 2 presses 'i' (top lane placement).
                if (Input.GetKeyDown(KeyCode.I))
                {
                    // Place the card in the corresponding postition.
                    PlaceCard(p2SelectedCardIndex, P2SpawnTop, p2Cards, 2);
                    // Reset the index after placing the card.
                    p2SelectedCardIndex = -1;

                }
                // If player 2 presses 'j' (bot lane placement).
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    // Place the card in the corresponding postition.
                    PlaceCard(p2SelectedCardIndex, P2SpawnBot, p2Cards, 2);
                    // Reset the index after placing the card.
                    p2SelectedCardIndex = -1;

                }
            }
        }
    }

    // Selects a card based on the given index and updates the appropriate player's selected card index.
    void SelectCard(ref int selectedCardIndex, int index, GameObject[] cards)
    {
        // If the selected index is within the bounds of the cards array:
        if (index >= 0 && index < cards.Length)
        {
            // Update the selected card index to reflect the player's choice.
            selectedCardIndex = index;
        }
    }

    
    // Places a card at the specified spawn point using the selected card index and cards array.
    void PlaceCard(int selectedCardIndex, Transform spawnPoint, GameObject[] cards, int playerNum)
    {
        // If the selection is invalid among the players list:
        if (selectedCardIndex < 0 || selectedCardIndex >= cards.Length) return;

        // Retrieve the prefab to instantiate based on the selected card index.
        GameObject playerPrefab = cards[selectedCardIndex];

        // Get the enemy script from the chosen prefab
        Enemy enemy = playerPrefab.GetComponent<Enemy>();

        // Elixir deduction
        if (playerNum == 1)
        {
            if (gameManager.player1Elixir >= enemy.elixirCost) // Check if player has enough elixir for the card
            {
                // Instantiate the prefab at the designated spawn point.
                Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
                gameManager.player1Elixir -= enemy.elixirCost;
            }
            
        }
        else if (playerNum == 2)
        {
            if (gameManager.player2Elixir >= enemy.elixirCost) // Check if the player has enough elixir for the card
            {
                // Instantiate the prefab at the designated spawn point.
                Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
                gameManager.player2Elixir -= enemy.elixirCost;
            }
            
        }
        

    }
}