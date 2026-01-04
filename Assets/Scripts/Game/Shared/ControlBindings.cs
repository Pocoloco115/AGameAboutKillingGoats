using UnityEngine;
using System.Collections.Generic;

public static class ControlBindings
{
    private static Dictionary<string, KeyCode> bindings;
    private static float sensitivity = 1f;

    public static float Sensitivity
    {
        get
        {
            if (bindings == null) Load();
            return sensitivity;
        }
        private set
        {
            sensitivity = Mathf.Clamp(value, 0.1f, 1f);
        }
    }

    public static void Load()
    {
        var config = ControlConfigManager.GetConfig();

        bindings = new Dictionary<string, KeyCode>();
        foreach (var b in config.bindings)
            bindings[b.actionName] = b.key;

        var slider = config.sliders.Find(s => s.sliderName == "Sensitivity");
        Sensitivity = (slider != null ? slider.value : 5f) / 10f;
    }

    public static void Reload()
    {
        ControlConfigManager.ReloadFromDisk();
        Load();
    }

    public static KeyCode GetKey(string actionName)
    {
        if (bindings == null) Load();
        return bindings.TryGetValue(actionName, out var key) ? key : KeyCode.None;
    }

    public static float GetAxis(string negative, string positive)
    {
        float value = 0f;
        if (Input.GetKey(GetKey(negative))) value -= 1f;
        if (Input.GetKey(GetKey(positive))) value += 1f;
        return value;
    }
}