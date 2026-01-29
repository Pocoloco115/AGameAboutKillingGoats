using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SettingSelector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [Header("Config")]
    [SerializeField] private string settingType;

    private string[] options;
    private int currentIndex;
    private Resolution[] filteredResolutions;

    private static readonly Vector2Int[] commonResolutions = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1024, 768),
        new Vector2Int(1280, 720),
        new Vector2Int(1366, 768),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080) 
    };

    void Start()
    {
        var config = GraphicsConfigManager.GetWorkingCopy();

        if (settingType == "Quality")
        {
            options = QualitySettings.names;
            currentIndex = System.Array.IndexOf(options, config.qualityLevel);
            if (currentIndex < 0)
            {
                currentIndex = QualitySettings.GetQualityLevel();
            }
        }
        else if (settingType == "Resolution")
        {
            List<Resolution> resList = new List<Resolution>();
            foreach (var res in Screen.resolutions)
            {
                foreach (var common in commonResolutions)
                {
                    if (res.width == common.x && res.height == common.y)
                    {
                        resList.Add(res);
                        break;
                    }
                }
            }

            filteredResolutions = resList
                .GroupBy(r => new { r.width, r.height })
                .Select(g => g.First())
                .OrderBy(r => r.width * r.height)
                .ToArray();

            options = filteredResolutions.Select(r => $"{r.width}x{r.height}").ToArray();

            currentIndex = System.Array.FindIndex(filteredResolutions,
                r => r.width == config.resolutionWidth && r.height == config.resolutionHeight);

            if (currentIndex < 0 && filteredResolutions.Length > 0)
            {
                currentIndex = 0;
            }
        }
        else if (settingType == "FPS")
        {
            options = new string[] { "30", "60", "120", "144", "Unlimited" };
            string fpsStr = config.targetFPS == -1 ? "Unlimited" : config.targetFPS.ToString();
            currentIndex = System.Array.IndexOf(options, fpsStr);
            if (currentIndex < 0)
            {
                currentIndex = 1;
            }
        }

        UpdateValue();

        leftButton.onClick.AddListener(() => ChangeValue(-1));
        rightButton.onClick.AddListener(() => ChangeValue(1));
    }

    private void ChangeValue(int direction)
    {
        currentIndex = Mathf.Clamp(currentIndex + direction, 0, options.Length - 1);
        UpdateValue();

        var config = GraphicsConfigManager.GetWorkingCopy();

        if (settingType == "Quality")
        {
            config.qualityLevel = options[currentIndex];
        }
        else if (settingType == "Resolution")
        {
            Resolution res = filteredResolutions[currentIndex];
            config.resolutionWidth = res.width;
            config.resolutionHeight = res.height;
        }
        else if (settingType == "FPS")
        {
            config.targetFPS = options[currentIndex] == "Unlimited" ? -1 : int.Parse(options[currentIndex]);
        }
    }

    private void UpdateValue()
    {
        valueText.text = options[currentIndex];
    }
}
