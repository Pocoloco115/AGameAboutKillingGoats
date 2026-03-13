using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementUIItem : MonoBehaviour
{
    [SerializeField] private string id;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite lockedIcon;

    private bool _isUnlocked = false;
    public string Id => id;
    public bool IsUnlocked => _isUnlocked;

    void Awake()
    {
        var def = AchievementManager.Instance.GetDefinition(id);

        if (def != null)
        {
            titleText.text = def.title;
            descriptionText.text = def.description;
            iconImage.sprite = _isUnlocked ?  AchievementManager.Instance.GetIcon(def.icon) : lockedIcon;
        }
        else
        {
            Debug.LogWarning($"No se encontró definición para logro con id {id}");
        }
    }



    public void Unlock()
    {
        _isUnlocked = true;

        var def = AchievementManager.Instance.GetDefinition(id);
        if (def != null)
        {
            iconImage.sprite = AchievementManager.Instance.GetIcon(def.icon);
        }
    }

}
