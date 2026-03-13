using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class StatsUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI goatsKilledText;
    [SerializeField] private TMPro.TextMeshProUGUI hitsTakenText;
    [SerializeField] private TMPro.TextMeshProUGUI shotsFiredText;
    [SerializeField] private TMPro.TextMeshProUGUI timeSurvivedText;
    [SerializeField] private TMPro.TextMeshProUGUI longDistanceKills;
    public static StatsUI Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
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
        
    }
    public void UpdateStats()
    {
        goatsKilledText.text = $"{GameStats.Instance.goatsKilled}";
        hitsTakenText.text = $"{GameStats.Instance.hitsTaken}";
        shotsFiredText.text = $"{GameStats.Instance.shotsFired}";
        timeSurvivedText.text = $"{GameStats.Instance.timeSurvived:F2}";
        longDistanceKills.text = $"{GameStats.Instance.longDistanceKills}";
    }
}
