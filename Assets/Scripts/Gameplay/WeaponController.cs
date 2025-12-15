using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 10;
    [SerializeField] private int ammo = 30;
    [SerializeField] private float reloadTime;
    [SerializeField] private PlayerInputHandler m_InputHandler;

    [Header("Visual Effects")]
    [SerializeField] private GameObject impactEffectPrefab;

    [Header("Animations")]
    [SerializeField] private Animator weaponAnimator;
    private int currentAmmo;
    private bool isReloading = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;
        if(m_InputHandler.GetShootInputDown())
        {
            HandleShootWeapon();
        }
        if(m_InputHandler.GetReloadInputDown() && currentAmmo < ammo)
        {
            ReloadWeapon();
        }

    }

    private void HandleShootWeapon()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, range))
        {
            Vector3 shootDirection = (hitInfo.point - shootOrigin.position).normalized;
            Debug.DrawRay(shootOrigin.position, shootDirection * range, Color.red, 1f);

            if (impactEffectPrefab != null)
            {
                Instantiate(impactEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            }
        }

        weaponAnimator.SetTrigger("Fire");
    }
    private void ReloadWeapon()
    {
        if(isReloading) return;
        if(currentAmmo < 0) return;
        if(currentAmmo == ammo) return;
        StartCoroutine(ReloadCoroutine());
    }
    public void FinishReload()
    {
        currentAmmo = ammo;
        isReloading = false;
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        weaponAnimator.SetTrigger("Reload");
        yield return null;
    }
}
