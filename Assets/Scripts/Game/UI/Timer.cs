using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI timer;
    private float timeSurvived = 0f;
    private bool isTracking = true;
    public static Timer Instance { get; private set; }
    public bool IsTracking
    {
        get { return isTracking; } set { isTracking = value; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking)
        {
            timeSurvived += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeSurvived / 60f);
            int seconds = Mathf.FloorToInt(timeSurvived % 60f);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}
