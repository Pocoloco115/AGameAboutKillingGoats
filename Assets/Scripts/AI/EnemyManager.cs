using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float drag = 0.5f;
    [SerializeField] private float maxVelocity = 5f;
    [SerializeField] private float angularDrag = 0.5f;

    private Rigidbody enemyRigidbody;
    private GameObject player;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        enemyRigidbody.linearDamping = drag;
        enemyRigidbody.angularDamping = angularDrag;
    }

    private void FixedUpdate()
    {
        PlayerFollower();
        LimitVelocity();
    }

    private void PlayerFollower()
    {
        enemyRigidbody.transform.LookAt(player.transform);
        enemyRigidbody.AddForce(enemyRigidbody.transform.forward * speed, ForceMode.Acceleration);
    }

    private void LimitVelocity()
    {
        if (enemyRigidbody.linearVelocity.magnitude > maxVelocity)
        {
            enemyRigidbody.linearVelocity = enemyRigidbody.linearVelocity.normalized * maxVelocity;
        }
    }

    public void TakeDamage()
    {
        Object.FindAnyObjectByType<EnemySpawner>().RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
