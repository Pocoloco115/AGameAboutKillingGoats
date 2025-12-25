using UnityEngine;
using System.Collections.Generic;

public class ControlMenuManager : MonoBehaviour
{
    [SerializeField] private Transform container;
    private List<ControlActionPrefab> actions = new();
    private ControlActionPrefab currentRebindingAction;

    void Start()
    {
        actions.Clear();
        foreach (Transform child in container)
        {
            var action = child.GetComponent<ControlActionPrefab>();
            if (action == null) continue;

            action.manager = this;
            action.OnKeyChanged += SaveConfig;
            actions.Add(action);
        }

        var config = ControlConfigManager.LoadConfig();
        foreach (var binding in config.bindings)
        {
            var action = actions.Find(a => a.ActionName == binding.actionName);
            if (action != null)
                action.SetKey(binding.key);
        }
    }

    private void SaveConfig()
    {
        var config = new ControlConfig();
        foreach (var action in actions)
        {
            config.bindings.Add(new ActionBinding
            {
                actionName = action.ActionName,
                key = action.CurrentKey
            });
        }
        ControlConfigManager.SaveConfig(config);
    }
    public bool IsKeyInUse(KeyCode key, ControlActionPrefab requester)
    {
        foreach (var action in actions)
        {
            if (action == requester) continue; 
            if (action.CurrentKey == key) return true;
        }
        return false;
    }
    public bool CanStartRebind(ControlActionPrefab requester)
    {
        return currentRebindingAction == null || currentRebindingAction == requester;
    }

    public void NotifyRebindStarted(ControlActionPrefab action)
    {
        currentRebindingAction = action;
    }

    public void NotifyRebindFinished(ControlActionPrefab action)
    {
        if (currentRebindingAction == action)
            currentRebindingAction = null;
    }

}