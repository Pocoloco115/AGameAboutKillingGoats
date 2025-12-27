using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int healAmountPerTick = 1;       
    [SerializeField] private float healDelay = 2f;            
    [SerializeField] private float healInterval = 0.1f;       

    private Health health;
    private float lastDamageTime;
    private bool isHealing;

    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (!health.IsDead() && Time.time - lastDamageTime >= healDelay && !isHealing)
        {
            StartCoroutine(RegenerateHealth());
        }
    }

    public void OnDamageTaken()
    {
        lastDamageTime = Time.time;
        isHealing = false; 
        StopAllCoroutines();
    }

    private IEnumerator RegenerateHealth()
    {
        isHealing = true;
        while (health.GetCurrentHealth() < health.GetMaxHealth())
        {
            health.Heal(healAmountPerTick);
            yield return new WaitForSeconds(healInterval);
        }
        isHealing = false;
    }
}