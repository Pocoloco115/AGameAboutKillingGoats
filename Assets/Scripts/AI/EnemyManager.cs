using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 3f;
    private Rigidbody enemyRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFollower();
    }

    private void PlayerFollower()
    {
        enemyRigidbody.transform.LookAt(player.transform);
        enemyRigidbody.AddForce(enemyRigidbody.transform.forward * speed);
    }
    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
