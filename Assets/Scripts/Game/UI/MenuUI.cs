using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        playButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(() =>
        {
            if (SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("SampleScene");
            }

        });


        settingsButton.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("SettingsMenu");
            }
        });

        exitButton.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.Close();
            }
        });
    }
}
