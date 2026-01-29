using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private GameObject player;
    public static bool IsPaused;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerController.IsDead)
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                TogglePauseMenu();
            }
        }
    }

    private void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        IsPaused = pauseMenuUI.activeSelf;
        AudioManager.Instance.OnPause();
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = IsPaused ? 0f : 1f;
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        IsPaused = false;
        AudioManager.Instance.OnResume();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
}
