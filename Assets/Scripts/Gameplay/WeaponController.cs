using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 10;
    [SerializeField] private int ammo = 5;
    [SerializeField] private float reloadTime;
    [SerializeField] private PlayerInputHandler m_InputHandler;
    [SerializeField] private float fireRate = 0.1f;
    private float nextTimeToFire = 0f;
    private int enemyCounter = 0;
    public int EnemyCounter
    {
        get { return enemyCounter; }
        set { enemyCounter = value; }
    }

    [Header("Visual Effects")]
    [SerializeField] private GameObject impactEffectPrefab;

    [Header("Animations")]
    [SerializeField] private Animator weaponAnimator;
    private int currentAmmo;
    private bool isReloading = false;
    public int CurrentAmmo
    {
        get { return currentAmmo; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if(m_InputHandler.GetShootInputDown() && Time.time >= nextTimeToFire)
        {
            HandleShootWeapon();
            nextTimeToFire = Time.time + fireRate;
        }
        if(m_InputHandler.GetReloadInputDown() && currentAmmo < ammo)
        {
            ReloadWeapon();
        }

    }

    private void HandleShootWeapon()
    {
        if (IsWeaponEmpty())
        {
            AudioManager.Instance.PlaySFX("EmptyShoot");
            return;
        }
        AudioManager.Instance.PlaySFX("Shoot");
        currentAmmo--;
        GameStats.Instance.AddShotFired();
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, range))
        {
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                int shotDistance = (int)Vector3.Distance(shootOrigin.position, hitInfo.point);
                Debug.Log($"Hit enemy at distance: {shotDistance} units");
                if (shotDistance >= 25)
                {
                    LongShotFeedback.Instance.Play();
                }
                enemyCounter++;
                GameStats.Instance.AddGoatKill(shotDistance);
                hitInfo.collider.GetComponent<Health>().TakeDamage(damage);
            }

            Vector3 shootDirection = (hitInfo.point - shootOrigin.position).normalized;
            Debug.DrawRay(shootOrigin.position, shootDirection * range, Color.red, 1f);

            if (impactEffectPrefab != null && !hitInfo.collider.CompareTag("Top"))
            {
                Instantiate(impactEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            }
        }
        else
        {
            Vector3 endPoint = ray.origin + ray.direction * range;
            Vector3 shootDirection = (endPoint - shootOrigin.position).normalized;
            Debug.DrawRay(shootOrigin.position, shootDirection * range, Color.red, 1f);
        }

        weaponAnimator.SetTrigger("Fire");
    }

    public bool IsWeaponEmpty()
    {
        return currentAmmo <= 0;
    }
    private void ReloadWeapon()
    {
        if(isReloading) return;
        if(currentAmmo < 0) return;
        if(currentAmmo == ammo) return;
        AudioManager.Instance.PlaySFX("Reload");
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