using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatsManager : MonoBehaviour
{

    public PlayerStats stats;
    private string statsPath;

    void Awake()
    {
        statsPath = Application.persistentDataPath + "/playerStats.json";
        LoadStats();
    }

    private void LoadStats()
    {
        if (File.Exists(statsPath))
        {
            string json = File.ReadAllText(statsPath);
            stats = JsonUtility.FromJson<PlayerStats>(json);
        }
        else
        {
            stats = new PlayerStats();
        }
    }

    private void SaveStats()
    {
        string json = JsonUtility.ToJson(stats);
        File.WriteAllText(statsPath, json);
    }

    // Game Stats
    public void UpdateTotalGamesPlayed()
    {
        stats.totalGamesPlayed++;
        SaveStats();
    }

    // Player 1 Stats
    public void UpdateP1TotalElixirSpent(int amount)
    {
        stats.p1TotalElixirSpent += amount;
        SaveStats();
    }

    public void UpdateP1TotalCardsPlaced()
    {
        stats.p1TotalCardsPlaced++;
        SaveStats();
    }

    public void UpdateP1TotalSpeedyPlaced()
    {
        stats.p1TotalSpeedyPlaced++;
        SaveStats();
    }

    public void UpdateP1TotalTankPlaced()
    {
        stats.p1TotalTankPlaced++;
        SaveStats();
    }

    public void UpdateP1TotalRegularPlaced()
    {
        stats.p1TotalRegularPlaced++;
        SaveStats();
    }

    // Player 2 Stats
    public void UpdateP2TotalElixirSpent(int amount)
    {
        stats.p2TotalElixirSpent += amount;
        SaveStats();
    }

    public void UpdateP2TotalCardsPlaced()
    {
        stats.p2TotalCardsPlaced++;
        SaveStats();
    }

    public void UpdateP2TotalSpeedyPlaced()
    {
        stats.p2TotalSpeedyPlaced++;
        SaveStats();
    }

    public void UpdateP2TotalTankPlaced()
    {
        stats.p2TotalTankPlaced++;
        SaveStats();
    }

    public void UpdateP2TotalRegularPlaced()
    {
        stats.p2TotalRegularPlaced++;
        SaveStats();
    }

    
}
