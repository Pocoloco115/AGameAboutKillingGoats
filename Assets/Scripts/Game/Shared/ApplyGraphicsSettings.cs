using UnityEngine;

public class ApplyGraphicsSettings : MonoBehaviour
{
    void Awake()
    {
        var gConfig = GraphicsConfigManager.GetConfig();

        // Calidad
        int qualityIndex = System.Array.IndexOf(QualitySettings.names, gConfig.qualityLevel);
        if (qualityIndex >= 0)
            QualitySettings.SetQualityLevel(qualityIndex);

        // Resolución
        Screen.SetResolution(gConfig.resolutionWidth,
                             gConfig.resolutionHeight,
                             gConfig.fullscreen);

        // VSync
        QualitySettings.vSyncCount = gConfig.vsync ? 1 : 0;
    }
}
