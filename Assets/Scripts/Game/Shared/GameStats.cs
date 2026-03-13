using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get; private set; }

    public int goatsKilled { get; private set; }
    public int longDistanceKills { get; private set; }
    private int longDistanceKillStreak = 0;
    const int   LONG_DISTANCE_KILL_THRESHOLD = 25; 
    public int hitsTaken { get; private set; }
    public int shotsFired { get; private set; }
    public float timeSurvived { get; private set; }
    private bool wasMovedFromStart = false;
    public bool WasMovedFromStart
    {
        get { return wasMovedFromStart; }
        set { wasMovedFromStart = value; }
    }
    private bool isTracking = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isTracking && SceneManager.GetActiveScene().name == "SampleScene")
        {
            timeSurvived += Time.deltaTime;
            AchievementManager.Instance.TutorialCompleted(timeSurvived);
            AchievementManager.Instance.PacifistSurvivor(timeSurvived, shotsFired);
            AchievementManager.Instance.GoatDodger(timeSurvived, hitsTaken);
            AchievementManager.Instance.Speedrunner(goatsKilled, timeSurvived);
        }
    }

    public void AddGoatKill(int distance)
    {
        goatsKilled++;

        if(distance >= LONG_DISTANCE_KILL_THRESHOLD)
        {
            longDistanceKills++;
            longDistanceKillStreak++;
            AchievementManager.Instance.GoatSniper(longDistanceKills);
        }
        else
        {
            longDistanceKillStreak = 0;
        }

        AchievementManager.Instance.LockIn(wasMovedFromStart, goatsKilled);

        StatsDataManager.Instance.data.totalGoatsKilled += 1;

        if (StatsDataManager.Instance.data.totalGoatsKilled >= 1000)
        {
            AchievementManager.Instance.GoatSlayer(StatsDataManager.Instance.data.totalGoatsKilled);
        }

        StatsDataManager.Instance.SaveStats(); 
    }
    public void addDeath()
    {
        StatsDataManager.Instance.RegisterDeath();
        AchievementManager.Instance.ExplosiveFriendship(StatsDataManager.Instance.data.totalDeaths);
    }
    public void AddShotFired()
    {
        shotsFired++;
        AchievementManager.Instance.BillyTheKid(goatsKilled, shotsFired);
    }

    public void AddHitTaken()
    {
        hitsTaken++;
    }

    public void ResetStats()
    {
        goatsKilled = 0;
        hitsTaken = 0;
        shotsFired = 0;
        timeSurvived = 0f;
        longDistanceKills = 0;
        longDistanceKillStreak = 0;
        isTracking = true;
    }

    public void StopTracking()
    {
        isTracking = false;
        StatsDataManager.Instance.SaveStats(); 
    }
}