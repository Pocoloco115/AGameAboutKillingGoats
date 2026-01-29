using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button mainMenu1;
    [SerializeField] private Button mainMenu2;
    [SerializeField] private Button restart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu1.onClick.RemoveAllListeners();
        mainMenu2.onClick.RemoveAllListeners();
        restart.onClick.RemoveAllListeners();

        mainMenu1.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("MainMenu");
            }
        });
        mainMenu2.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.LoadScene("MainMenu");
            }
        });
        restart.onClick.AddListener(() =>
        {
            if(SceneHandler.Instance != null)
            {
                SceneHandler.Instance.ReloadCurrentScene();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
