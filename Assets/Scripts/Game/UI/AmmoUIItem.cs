using UnityEngine;
using UnityEngine.UI;

public class AmmoUIItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Image ammoImage;
    [SerializeField] private Animator animator;

    private bool isFull = true;

    public void SetFullInstant()
    {
        isFull = true;
        ammoImage.sprite = fullSprite;
        if (animator != null)
        {
            animator.Rebind();          
            animator.Play("bullet_full");  
        }
    }

    public void SetEmptyInstant()
    {
        isFull = false;
        ammoImage.sprite = emptySprite;
            if (animator != null)
            {   
                animator.Play("bullet_empty2");
        }
    }

    public void PlayShoot()
    {
        if (!isFull)
        {
            return;
        }

        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }
    }

    public void OnShootAnimationEnd()
    {
        isFull = false;
        ammoImage.sprite = emptySprite;
    }
}
