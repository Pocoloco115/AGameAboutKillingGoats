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
            ControlConfig defaultConfig = new ControlConfig();
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Move Forward", key = KeyCode.W });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Move Backward", key = KeyCode.S });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Move Left", key = KeyCode.A });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Move Right", key = KeyCode.D });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Jump", key = KeyCode.Space });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Sprint", key = KeyCode.LeftShift });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Fire", key = KeyCode.Mouse0 });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Reload", key = KeyCode.R });
            defaultConfig.bindings.Add(new ActionBinding { actionName = "Crouch", key = KeyCode.C });
            SaveConfig(defaultConfig);
            return defaultConfig;
        }
    }

}