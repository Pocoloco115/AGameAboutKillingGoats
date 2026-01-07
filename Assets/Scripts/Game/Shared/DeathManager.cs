#nullable enable
using UnityEngine;
using UnityEngine.UIElements;

public class DeathManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject deathAnimation;
    [SerializeField] private Transform deathAnimationOrigin;
    [SerializeField] private GameObject? gameOverPanel;
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
        else
        {
            Transform grandChild = transform.GetChild(1).GetChild(1);
            grandChild.gameObject.SetActive(false);
        }

        Instantiate(deathAnimation, deathAnimationOrigin.position, Quaternion.identity, transform);
    }
    public void DestroySelf()
    {
        if (gameObject.CompareTag("Player"))
        {
            SetGameOver();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void SetGameOver()
    {
        if(gameOverPanel != null)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            GetComponent<PlayerInputHandler>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<WeaponController>().enabled = false;
            Invoke(nameof(DisplayGameOverPanel), 0.2f);
        }
    }
    private void DisplayGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}