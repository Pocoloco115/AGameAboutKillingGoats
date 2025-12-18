using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] spawnerPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    private List<GameObject> enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = new List<GameObject>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SpawnEnemies()
    {
        for(; ; )
        {
            if (enemies.Count < 10)
            {
                int random = Random.Range(0, spawnerPoints.Length);
                GameObject newEnemy = Instantiate(enemyPrefab, spawnerPoints[random].transform.position, Quaternion.identity);
                enemies.Add(newEnemy);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
}
