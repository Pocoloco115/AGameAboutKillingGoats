using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button keyboardSettings;
    [SerializeField] private Button audioSettings;
    [SerializeField] private Button videoSettings;
    [SerializeField] private Button mainMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboardSettings.onClick.RemoveAllListeners();
        audioSettings.onClick.RemoveAllListeners();
        videoSettings.onClick.RemoveAllListeners();
        mainMenu.onClick.RemoveAllListeners();

        keyboardSettings.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("KeyboardSettingsMenu");
            }
        });

        audioSettings.onClick.AddListener(() =>
        {
           if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("AudioSettings");
            }
        });

        videoSettings.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("GraphicsMenu");
            }
        });
        
        mainMenu.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("MainMenu");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
