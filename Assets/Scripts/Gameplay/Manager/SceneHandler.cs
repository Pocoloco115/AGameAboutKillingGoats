using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float minLoadingScreenTime = 3f;
    public static string nextScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        PauseMenuHandler.IsPaused = false;

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.interactable = false;
            fadeCanvasGroup.blocksRaycasts = true;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;

        if (PauseMenuHandler.IsPaused)
        {
            // Clear the paused flag but do not automatically resume audio here.
            // Resuming audio should only happen when the player explicitly resumes the game,
            // otherwise changing scenes (e.g. returning to menu) would unpause audio unexpectedly.
            PauseMenuHandler.IsPaused = false;
        }

        StartCoroutine(FadeOut());

        if (scene.name == "LoadingScene" && !string.IsNullOrEmpty(nextScene))
        {
            StartCoroutine(LoadNextSceneAsync(nextScene));
        }
    }

    private IEnumerator FadeIn()
    {
        if (fadeCanvasGroup == null) yield break;

        fadeCanvasGroup.alpha = 0f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        if (fadeCanvasGroup == null) yield break;

        float elapsed = fadeDuration;

        while (elapsed > 0f)
        {
            elapsed -= Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
    }

    private IEnumerator LoadNextSceneAsync(string targetScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false;

        float startTime = Time.unscaledTime;
        bool sceneReady = false;
        bool minTimePassed = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                sceneReady = true;
            }

            if (Time.unscaledTime - startTime >= minLoadingScreenTime)
            {
                minTimePassed = true;
            }

            if (sceneReady && minTimePassed)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator FadeInAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeIn());

        if (sceneName == "SampleScene" && SceneManager.GetActiveScene().name != "SampleScene")
        {
            nextScene = sceneName;
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeInAndLoad(sceneName));
    }

    public void Close()
    {
        Application.Quit();
    }

    public void ReloadCurrentScene()
    {
        string current = SceneManager.GetActiveScene().name;
        LoadScene(current);
    }
}