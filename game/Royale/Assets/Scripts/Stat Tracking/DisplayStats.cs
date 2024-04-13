using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public TextMeshProUGUI statsText;  // TextMeshProUGUI component

    void Start()
    {
        UpdateStatsDisplay();
    }

    private void UpdateStatsDisplay()
    {
        if (StatsManager.Instance != null)
        {
            PlayerStats stats = StatsManager.Instance.stats;
            statsText.text = $"Total Games Played: {stats.totalGamesPlayed}\n" +
                            
                             $"Player 1 Total Elixir Spent: {stats.p1TotalElixirSpent}\n" +
                             $"Player 1 Total Cards Placed: {stats.p1TotalCardsPlaced}\n" +
                             $"Player 1 Total Speedy Placed: {stats.p1TotalSpeedyPlaced}\n" +
                             $"Player 1 Total Tank Placed: {stats.p1TotalTankPlaced}\n" +
                             $"Player 1 Total Regular Placed: {stats.p1TotalRegularPlaced}\n" +
                             
                             $"Player 2 Total Elixir Spent: {stats.p2TotalElixirSpent}\n" +
                             $"Player 2 Total Cards Placed: {stats.p2TotalCardsPlaced}\n" +
                             $"Player 2 Total Speedy Placed: {stats.p2TotalSpeedyPlaced}\n" +
                             $"Player 2 Total Tank Placed: {stats.p2TotalTankPlaced}\n" +
                             $"Player 2 Total Regular Placed: {stats.p2TotalRegularPlaced}";
        }
        else
        {
            statsText.text = "Stats not loaded!";
        }
    }
}
