using UnityEngine;
using System.Collections.Generic;

public class UIAmmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<AmmoUIItem> ammoItems;
    [SerializeField] private WeaponController weaponController;

    private int lastAmmo = -1;

    void Start()
    {
        RefreshAll();
    }

    void Update()
    {
        if (weaponController.CurrentAmmo != lastAmmo)
        {
            HandleAmmoChanged(weaponController.CurrentAmmo);
        }
    }

    private void HandleAmmoChanged(int newAmmo)
    {
        if (newAmmo < lastAmmo)
        {
            int index = newAmmo;
            if (index >= 0 && index < ammoItems.Count)
            {
                ammoItems[index].PlayShoot();
            }
        }

        if (newAmmo > lastAmmo)
        {
            RefreshAll();
        }

        lastAmmo = newAmmo;
    }

    private void RefreshAll()
    {
        int ammoCount = weaponController.CurrentAmmo;
        lastAmmo = ammoCount;

        for (int i = 0; i < ammoItems.Count; i++)
        {
            if (i < ammoCount)
            {
                ammoItems[i].SetFullInstant();

            }
            else
            {
                ammoItems[i].SetEmptyInstant();
            }
        }
    }

    public void ForceReload()
    {
        RefreshAll();
    }
}
