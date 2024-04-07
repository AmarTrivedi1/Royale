using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    // Player 1.
    public GameObject[] p1Cards;
    public Transform P1SpawnTop;
    public Transform P1SpawnBot;
    private int p1SelectedCardIndex = -1;

    // Player 2.
    public GameObject[] p2Cards;
    public Transform P2SpawnTop;
    public Transform P2SpawnBot;
    private int p2SelectedCardIndex = -1;
    

    void Update()
    {
        // Player 1 controls.
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectCard(ref p1SelectedCardIndex, 0, p1Cards);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectCard(ref p1SelectedCardIndex, 1, p1Cards);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectCard(ref p1SelectedCardIndex, 2, p1Cards);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectCard(ref p1SelectedCardIndex, 3, p1Cards);
        // Player 2 controls.
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectCard(ref p2SelectedCardIndex, 0, p2Cards);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SelectCard(ref p2SelectedCardIndex, 1, p2Cards);
        if (Input.GetKeyDown(KeyCode.Alpha9)) SelectCard(ref p2SelectedCardIndex, 2, p2Cards);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SelectCard(ref p2SelectedCardIndex, 3, p2Cards);

        // Placement controls for Player 1.
        if (p1SelectedCardIndex != -1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PlaceCard(p1SelectedCardIndex, P1SpawnTop, p1Cards);
                p1SelectedCardIndex = -1; // Reset the index after placing the card.
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                PlaceCard(p1SelectedCardIndex, P1SpawnBot, p1Cards);
                p1SelectedCardIndex = -1; // Reset the index after placing the card.
            }
        }

        // Placement controls for Player 2.
        if (p2SelectedCardIndex != -1)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                PlaceCard(p2SelectedCardIndex, P2SpawnTop, p2Cards);
                p2SelectedCardIndex = -1; // Reset the index after placing the card.
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                PlaceCard(p2SelectedCardIndex, P2SpawnBot, p2Cards);
                p2SelectedCardIndex = -1; // Reset the index after placing the card.
            }
        }
    }

    // Selects a card based on the given index and updates the appropriate player's selected card index.
    void SelectCard(ref int selectedCardIndex, int index, GameObject[] cards)
    {
        // Check if the selected index is within the bounds of the cards array.
        if (index >= 0 && index < cards.Length)
        {
            // Update the selected card index to reflect the player's choice.
            selectedCardIndex = index;
        }
    }

    
    // Places a card at the specified spawn point using the selected card index and cards array.
    void PlaceCard(int selectedCardIndex, Transform spawnPoint, GameObject[] cards)
    {
        // Check to ensure the selection is valid before proceeding.
        if (selectedCardIndex < 0 || selectedCardIndex >= cards.Length) return;
        
        // Retrieve the prefab to instantiate based on the selected card index.
        GameObject playerPrefab = cards[selectedCardIndex];
        
        // Instantiate the prefab at the designated spawn point.
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        // Reset the selected card index to -1 to indicate no selection, allowing for a new selection.
        selectedCardIndex = -1;
    }
}