using UnityEngine;
using UnityEngine.UI;

public class SettingCheckbox : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private string settingType;

    void Start()
    {
        var config = GraphicsConfigManager.GetWorkingCopy();

        if (settingType == "Fullscreen")
        {
            toggle.isOn = config.fullscreen;
        }
        else if (settingType == "VSync")
        {
            toggle.isOn = config.vsync;
        }

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool value)
    {
        var config = GraphicsConfigManager.GetWorkingCopy();

        if (settingType == "Fullscreen")
        {
            config.fullscreen = value;
        }
        else if (settingType == "VSync")
        {
            config.vsync = value;
        }
    }
}
