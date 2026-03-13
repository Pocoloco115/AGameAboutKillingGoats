using UnityEngine;

public class LongShotFeedback : MonoBehaviour
{
    public static LongShotFeedback Instance;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fxObject;

    private void Awake()
    {
        Instance = this;
        fxObject.SetActive(false);
    }

    public void Play()
    {
        fxObject.SetActive(true);
        animator.SetTrigger("Play");
    }
}
