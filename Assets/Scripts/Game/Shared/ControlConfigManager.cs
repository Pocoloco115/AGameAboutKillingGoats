using UnityEngine;
using System.IO;

public static class ControlConfigManager
{
    private static string filePath => Path.Combine(Application.persistentDataPath, "controls.json");

    public static void SaveConfig(ControlConfig config)
    {
        string json = JsonUtility.ToJson(config, true);
        File.WriteAllText(filePath, json);
    }

    public static ControlConfig LoadConfig()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<ControlConfig>(json);
        }
        else
        {
            return new ControlConfig(); 
        }
    }
}