using UnityEngine;
using UnityEngine.UI;

public class SaveGraphicsButton : MonoBehaviour
{
    [SerializeField] private Button saveButton;

    void Start()
    {
        saveButton.onClick.AddListener(SaveGraphicsSettings);
    }

    private void SaveGraphicsSettings()
    {
        GraphicsConfigManager.SaveAndApply();
    }
}