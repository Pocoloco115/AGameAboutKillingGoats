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
    private float fallStartHeight;
    private bool isFalling;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleFallDetection();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;
        currentHealth -= damageAmount;
        Object.FindAnyObjectByType<DamageVignette>()?.OnHit();
        GetComponent<HealManager>()?.OnDamageTaken();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void HandleFallDetection()
    {
        bool grounded = false;

        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
        {
            grounded = controller.isGrounded;
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
            }
        }

        if (!isFalling && !grounded)
        {
            isFalling = true;
            fallStartHeight = transform.position.y;
        }

        if (isFalling && grounded)
        {
            isFalling = false;
            float fallDistance = fallStartHeight - transform.position.y;

            if (fallDistance > maxHeightForFallDamage)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<DeathManager>()?.SetDeath();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DangerZone"))
        {
            Die();
        }
    }
}