using UnityEngine;

public class DeathEffectHandler : MonoBehaviour
{
    private Health healthComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthComponent = GetComponentInParent<Health>();
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
        if (healthComponent != null)
        {
            healthComponent.DestroySelf();
        }
    }
}
