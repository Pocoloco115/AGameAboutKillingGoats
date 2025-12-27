using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ControlConfig
{
    public List<ActionBinding> bindings = new List<ActionBinding>();
}

[Serializable]
public class ActionBinding
{
    public string actionName;   
    public KeyCode key;         
}