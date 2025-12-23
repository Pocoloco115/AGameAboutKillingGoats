using UnityEngine;

public class DeathEffectHandler : MonoBehaviour
{
    private DeathManager deathManagerComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathManagerComponent = GetComponentInParent<DeathManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main == null) return;
        transform.LookAt(Camera.main.transform);
        transform.forward = -Camera.main.transform.forward;
    }
    public void TriggerDeathEffect()
    {
        if (deathManagerComponent != null)
        {
            deathManagerComponent.DestroySelf();
            Destroy(gameObject);
        }
    }
}
