using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public WeaponController weaponController;

    public void OnReloadAnimationFinished()
    {
        if (weaponController != null)
        {
            weaponController.FinishReload();
        }
    }
}