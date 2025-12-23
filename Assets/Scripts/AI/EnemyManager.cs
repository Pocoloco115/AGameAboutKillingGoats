using System.Collections;
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
    public bool isPlayerActive;
    private Vector3 targetPoint;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        enemyRigidbody.linearDamping = drag;
        enemyRigidbody.angularDamping = angularDrag;
        RandomTargetPoint();
    }

    private void FixedUpdate()
    {
        isPlayerActive = player.transform.GetChild(1).GetChild(1).gameObject.activeInHierarchy == true;
        if (isPlayerActive)
        {
            PlayerFollower();
        }
        else
        {
            MoveToTargetPoint();
        }
        LimitVelocity();
    }

    private void PlayerFollower()
    {
        if (!isPlayerActive) return;
        enemyRigidbody.transform.LookAt(player.transform);
        enemyRigidbody.AddForce(enemyRigidbody.transform.forward * speed, ForceMode.Acceleration);
    }
    private void MoveToTargetPoint()
    {
        enemyRigidbody.transform.LookAt(targetPoint);
        Vector3 dir = (targetPoint - transform.position).normalized;
        enemyRigidbody.AddForce(dir * speed, ForceMode.Acceleration);
    }

    private void RandomTargetPoint()
    {
        float range = 10f; 
        targetPoint = new Vector3(
            transform.position.x + Random.Range(-range, range),
            transform.position.y,
            transform.position.z + Random.Range(-range, range)
        );
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
        else
        {
            RandomTargetPoint();
        }
    }
}