using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    // Game stats
    public int totalGamesPlayed;

    // Player 1 stats
    public int p1TotalElixirSpent;
    public int p1TotalCardsPlaced;
    public int p1TotalSpeedyPlaced;
    public int p1TotalTankPlaced;
    public int p1TotalRegularPlaced;

    // Player 2 stats
    public int p2TotalElixirSpent;
    public int p2TotalCardsPlaced;
    public int p2TotalSpeedyPlaced;
    public int p2TotalTankPlaced;
    public int p2TotalRegularPlaced;

}
