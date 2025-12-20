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
    private Animator animator;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(5);
            GetComponent<Health>()?.TakeDamage(10);
        }
    }
}
