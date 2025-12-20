using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float maxHeightForFallDamage;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private bool isPlayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        isPlayer = gameObject.CompareTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FallDamage();
    }
    public void TakeDamage(int damageAmount)
    {
        if(isDead) return;
        currentHealth -= damageAmount;
        GetComponent<HealManager>()?.OnDamageTaken();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void FallDamage()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            float height = transform.position.y - hitInfo.point.y;
            if (height > maxHeightForFallDamage)
            {
                Destroy(gameObject);
            }
        }
    }
    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<DeathManager>()?.SetDeath();
    }
    public void DestroySelf()
    {
        if (isPlayer)
        {
            Debug.Log("Player has died. Reloading scene...");
            Invoke(nameof(ReloadScene), 0.01f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
    public bool IsDead()
    {
        return isDead;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
