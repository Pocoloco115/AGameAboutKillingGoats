using UnityEngine;

public class UICounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMPro.TextMeshProUGUI counterText;
    private WeaponController weaponController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponController = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = weaponController.EnemyCounter.ToString();
    }
}
