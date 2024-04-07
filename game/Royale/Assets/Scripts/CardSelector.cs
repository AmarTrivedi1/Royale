using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public GameObject[] cards; // Array of card game objects.
    public Transform spawnP1Top; // Reference to the top spawn point.
    public Transform spawnP1Bot; // Reference to the bottom spawn point.
    private int selectedCardIndex = -1; // No card is selected initially.

    void Update()
    {
        // Listen for number key presses (1-4) to select corresponding cards.
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectCard(3);

        // If a card is selected:
        if (selectedCardIndex != -1)
        {
            // If a user presses w (for top row placement).
            if (Input.GetKeyDown(KeyCode.W))
            {
                PlaceCard(spawnP1Top);
            }
            // If a user presses s (for botton row placement).
            else if (Input.GetKeyDown(KeyCode.S))
            {
                PlaceCard(spawnP1Bot);
            }
        }
    }

    // Selects a card based on its index, and updates the selected card index.
    void SelectCard(int index)
    {   
        // If the given index is within the range of available cards:
        if (index >= 0 && index < cards.Length)
        {
            // Update the 'selectedCardIndex' to the current selection.
            selectedCardIndex = index;
        }
    }

    // Places a card depending on the spawnPoint.
    void PlaceCard(Transform spawnPoint)
    {
        // Extra check to ensure valid selection.
        if (selectedCardIndex < 0 || selectedCardIndex >= cards.Length) return;
        
        // Get the prefab to instantiate.
        GameObject playerPrefab = cards[selectedCardIndex];
        
        // Instantiate the prefab at the spawn point.
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        
        // Reset selectedCardIndex to allow for another selection.
        selectedCardIndex = -1;
    }
}