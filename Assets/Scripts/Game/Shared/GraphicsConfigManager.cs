using UnityEngine;
using System.IO;

public static class GraphicsConfigManager
{
    private static GraphicsConfig _cached;
    private static GraphicsConfig _workingCopy;

    private static string filePath => Path.Combine(Application.persistentDataPath, "graphics.json");

    public static GraphicsConfig GetConfig()
    {
        if (_cached == null)
            _cached = LoadConfig();
        return _cached;
    }

    public static GraphicsConfig GetWorkingCopy()
    {
        if (_workingCopy == null)
            _workingCopy = GetConfig().Clone();
        return _workingCopy;
    }

    public static void ApplyCurrentSettings()
    {
        var config = GetWorkingCopy();

        int qualityIndex = System.Array.IndexOf(QualitySettings.names, config.qualityLevel);
        if (qualityIndex >= 0)
            QualitySettings.SetQualityLevel(qualityIndex);

        Screen.SetResolution(config.resolutionWidth, config.resolutionHeight, config.fullscreen);

        Application.targetFrameRate = config.targetFPS > 0 ? config.targetFPS : -1;

        QualitySettings.vSyncCount = config.vsync ? 1 : 0;
    }

    public static void SaveAndApply()
    {
        _cached = GetWorkingCopy().Clone();
        string json = JsonUtility.ToJson(_cached, true);
        File.WriteAllText(filePath, json);

        ApplyCurrentSettings();
    }

    public static void DiscardChanges()
    {
        _workingCopy = null;
    }

    private static GraphicsConfig LoadConfig()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GraphicsConfig>(json);
        }

        GraphicsConfig defaultConfig = new GraphicsConfig();
        defaultConfig.resolutionWidth = Screen.currentResolution.width;
        defaultConfig.resolutionHeight = Screen.currentResolution.height;
        defaultConfig.qualityLevel = QualitySettings.names[QualitySettings.GetQualityLevel()];
        return defaultConfig;
    }
}

public static class GraphicsConfigExtensions
{
    public static GraphicsConfig Clone(this GraphicsConfig config)
    {
        string json = JsonUtility.ToJson(config);
        return JsonUtility.FromJson<GraphicsConfig>(json);
    }
}
