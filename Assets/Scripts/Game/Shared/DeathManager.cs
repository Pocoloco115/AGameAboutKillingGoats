using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject deathAnimation;
    [SerializeField] private Transform deathAnimationOrigin;
    private Rigidbody rb;
    private Collider col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = TryGetComponent<Rigidbody>(out Rigidbody rigidbody) ? rigidbody : null;
        col =TryGetComponent<Collider>(out Collider collider) ? collider : null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDeath()
    {
        if (!gameObject.CompareTag("Player"))
        {
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero;
            }
            if (col != null)
            {
                col.enabled = false;
            }
            Object.FindAnyObjectByType<EnemySpawner>().RemoveEnemy(gameObject);
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        Instantiate(deathAnimation, deathAnimationOrigin.position, Quaternion.identity, transform);
    }
}
