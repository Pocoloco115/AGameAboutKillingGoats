using UnityEngine;
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
                SceneHandler.Instance.LoadScene("MainMenu");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
