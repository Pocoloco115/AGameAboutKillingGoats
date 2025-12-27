using TMPro;
using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI ammoTextMesh;
    [SerializeField] private WeaponController weaponController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ammoTextMesh.text = weaponController.CurrentAmmo.ToString();
    }
}
