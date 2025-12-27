using UnityEngine;
using System.Collections.Generic;

public static class ControlBindings
{
    private static Dictionary<string, KeyCode> bindings;

    public static void Load()
    {
        bindings = new Dictionary<string, KeyCode>();
        var config = ControlConfigManager.LoadConfig();

        foreach (var b in config.bindings)
        {
            bindings[b.actionName] = b.key;
        }
    }

    public static KeyCode GetKey(string actionName)
    {
        if (bindings == null)
            Load();

        return bindings.TryGetValue(actionName, out var key)
            ? key
            : KeyCode.None;
    }

    public static float GetAxis(string negative, string positive)
    {
        float value = 0f;
        if (Input.GetKey(GetKey(negative))) value -= 1f;
        if (Input.GetKey(GetKey(positive))) value += 1f;
        return value;
    }
}