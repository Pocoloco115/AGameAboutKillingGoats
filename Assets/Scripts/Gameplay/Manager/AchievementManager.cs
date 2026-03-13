using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class AchievementData
{
    public string id;
    public bool isUnlocked;
}

[System.Serializable]
public class AchievementListWrapper
{
    public List<AchievementData> achievements;
}

[System.Serializable]
public class AchievementDefinition
{
    public string id;
    public string title;
    public string description;
    public string icon;
}

[System.Serializable]
public class AchievementCatalog
{
    public List<AchievementDefinition> achievements;
}

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    private Dictionary<string, AchievementDefinition> catalog;
    private List<AchievementData> achievementStates;
    private Dictionary<string, Sprite> iconCache = new();
    private string filePath;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        filePath = Path.Combine(Application.persistentDataPath, "achievements.json");

        LoadCatalog();
        PreloadIcons();
        LoadAchievements();
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySavedStates();
    }

    private void LoadCatalog()
    {
        TextAsset json = Resources.Load<TextAsset>("achievements_catalog");

        AchievementCatalog data = JsonUtility.FromJson<AchievementCatalog>(json.text);

        catalog = new Dictionary<string, AchievementDefinition>();

        foreach (var a in data.achievements)
        {
            catalog[a.id] = a;
        }
    }

    public AchievementDefinition GetDefinition(string id)
    {
        if (catalog.ContainsKey(id))
        {
            return catalog[id];
        }

        return null;
    }
    private void PreloadIcons()
    {
        foreach (var def in catalog.Values)
        {
            if (!iconCache.ContainsKey(def.icon))
            {
                iconCache[def.icon] =
                    Resources.Load<Sprite>($"AchievementIcons/{def.icon}");
            }
        }
    }
    public Sprite GetIcon(string iconName)
    {
        if (iconCache.TryGetValue(iconName, out var sprite))
        {
            return sprite;
        }

        sprite = Resources.Load<Sprite>($"AchievementIcons/{iconName}");
        iconCache[iconName] = sprite;
        return sprite;
    }

    private void LoadAchievements()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var wrapper = JsonUtility.FromJson<AchievementListWrapper>(json);
            achievementStates = wrapper?.achievements ?? new List<AchievementData>();
        }
        else
        {
            achievementStates = new List<AchievementData>();

            foreach (var def in catalog.Values)
            {
                achievementStates.Add(new AchievementData
                {
                    id = def.id,
                    isUnlocked = false
                });
            }

            SaveAchievements();
        }
    }

    private void SaveAchievements()
    {
        string json = JsonUtility.ToJson(new AchievementListWrapper
        {
            achievements = achievementStates
        }, true);

        File.WriteAllText(filePath, json);
    }

    private void UnlockAchievement(string id)
    {
        AchievementData data = achievementStates.Find(a => a.id == id);

        if (data != null && !data.isUnlocked)
        {
            data.isUnlocked = true;
            AchievementPopupUI.Instance.Show(GetIcon(catalog[id].icon), catalog[id].title);
            SaveAchievements();
            UpdateUIIfLoaded(id);
            CheckGoatfather();
        }
    }

    private void UpdateUIIfLoaded(string id)
    {
        AchievementUIItem[] items = Object.FindObjectsByType<AchievementUIItem>(FindObjectsSortMode.None); 

        foreach (var item in items)
        {
            if (item.Id == id)
            {
                item.Unlock();
            }
        }
    }

    private void ApplySavedStates()
    {
        AchievementUIItem[] items = Object.FindObjectsByType<AchievementUIItem>(FindObjectsSortMode.None);

        foreach (var item in items)
        {
            AchievementData data = achievementStates.Find(a => a.id == item.Id);

            if (data != null && data.isUnlocked)
            {
                item.Unlock();
            }
        }
    }

    private void CheckGoatfather()
    {
        bool allUnlocked = true;

        foreach (var a in achievementStates)
        {
            if (a.id == "goatfather")
            {
                continue;
            }
            if (!a.isUnlocked) 
            { 
                allUnlocked = false; break; 
            }
        }

        if (allUnlocked)
        {
            UnlockAchievement("goatfather");
        }
    }
    public void TutorialCompleted(float time)
    {
        if (time >= 600f) UnlockAchievement("tutorial");
    }

    public void LockIn(bool moved, int kills)
    {
        if (!moved && kills >= 100) UnlockAchievement("lockin");
    }

    public void BillyTheKid(int kills, int shots)
    {
        if (kills >= 200 && kills == shots) UnlockAchievement("billy");
    }

    public void GoatSniper(int longRangeKills)
    {
        if (longRangeKills >= 50) UnlockAchievement("sniper");
    }

    public void PacifistSurvivor(float time, int shots)
    {
        if (time >= 300f && shots == 0) UnlockAchievement("pacifist");
    }

    public void ExplosiveFriendship(int deaths)
    {
        Debug.Log($"Checking Explosive Friendship: {deaths} deaths");
        Debug.Log($"Current achievement state: {achievementStates.Find(a => a.id == "explosive")?.isUnlocked}");
        if (deaths >= 10) UnlockAchievement("explosive");
    }

    public void Speedrunner(int kills, float time)
    {
        if (kills >= 120 && time <= 180f) UnlockAchievement("speedrunner");
    }

    public void GoatDodger(float time, int hits)
    {
        if (time >= 120f && hits == 0) UnlockAchievement("dodger");
    }

    public void GoatSlayer(int goatsKilled)
    {
        if (goatsKilled >= 1000)
        {
            UnlockAchievement("slayer");
        }
    }
}