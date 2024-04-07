using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public GameObject[] cards;
    private int selectedCardIndex = -1; // No card is selected initially

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
                // Place the selected card at the top row.
                // PlaceCard(top);
            }
            // If a user presses s (for botton row placement).
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // Place the selected card at the bottom row.
                // PlaceCard(bottom);
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
            // Log the selection for debugging purposes.
            Debug.Log("Card " + index + " selected.");
        }
    }

    void PlaceCard(string position)
    {
        // Implement logic to place the card on the game map
        // Reset selectedCardIndex to allow for another selection
        selectedCardIndex = -1;
    }
}