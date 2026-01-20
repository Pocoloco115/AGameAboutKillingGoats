using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ControlConfig
{
    public List<ActionBinding> bindings = new List<ActionBinding>();
    public List<SliderBinding> sliders = new List<SliderBinding>();
}

[Serializable]
public class ActionBinding
{
    public string actionName;
    public KeyCode key;
}

[Serializable]
public class SliderBinding
{
    public string sliderName;
    public float value;
}

[Serializable]
public class GraphicsConfig
{
    public string qualityLevel = "Default";   
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    public bool fullscreen = true;
    public bool vsync = true;
    public int targetFPS = 60;
}