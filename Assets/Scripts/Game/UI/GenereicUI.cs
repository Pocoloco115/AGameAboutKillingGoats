using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenereicUI : MonoBehaviour
{
    [SerializeField] private Button mainMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.onClick.RemoveAllListeners();

        mainMenu.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                if(SceneManager.GetActiveScene().name == "Achievements")
                {
                    SceneHandler.Instance.LoadScene("MainMenu");
                    return;
                }
                else
                {
                    SceneHandler.Instance.LoadScene("SettingsMenu");
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
