using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageVignette : MonoBehaviour
{
    [SerializeField] private Volume volume;        
    [SerializeField] private Health playerHealth;  
    private Vignette vignette;

    private float targetIntensity = 0f;
    private int hitCount = 0; 

    void Start()
    {
        volume.profile.TryGet(out vignette);
    }

    void Update()
    {
        float healthPercent = (float)playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();

        if (healthPercent >= 0.99f)
        {
            hitCount = 0;          
            targetIntensity = 0f;  
        }

        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetIntensity, Time.deltaTime * 3f);
    }

    public void OnHit()
    {
        hitCount++;

        if (hitCount == 1)
            targetIntensity = 0.4f; 
        else
            targetIntensity = 0.5f; 
    }
}