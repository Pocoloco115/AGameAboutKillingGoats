using UnityEngine;
using System.IO;

[System.Serializable]
public class StatsData
{
    public int totalGoatsKilled;
    public int maxKillsInGame;
    public int totalDeaths;
}

public class StatsDataManager : MonoBehaviour
{
    public static StatsDataManager Instance { get; private set; }

    private string filePath;
    public StatsData data;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        filePath = Path.Combine(Application.persistentDataPath, "stats.json");
        LoadStats();
    }

    public void RegisterGame(GameStats gameStats)
    {
        data.totalGoatsKilled += gameStats.goatsKilled;

        if (gameStats.goatsKilled > data.maxKillsInGame)
        {
            data.maxKillsInGame = gameStats.goatsKilled;
        }

        SaveStats();
    }
    public void RegisterDeath()
    {
        data.totalDeaths++;
        SaveStats();
    }

    public void SaveStats()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    private void LoadStats()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<StatsData>(json);
        }
        else
        {
            data = new StatsData();
            SaveStats();
        }
    }
}
