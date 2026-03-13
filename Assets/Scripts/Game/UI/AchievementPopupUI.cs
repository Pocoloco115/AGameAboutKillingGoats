using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AchievementPopupUI : MonoBehaviour
{
    public static AchievementPopupUI Instance;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Animator animator;

    [SerializeField] private float displayTime = 2.5f;

    private Queue<PopupData> queue = new();
    private bool isShowing = false;

    private struct PopupData
    {
        public Sprite icon;
        public string title;

        public PopupData(Sprite i, string t)
        {
            icon = i;
            title = t;
        }
    }

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(Sprite achievementIcon, string achievementTitle)
    {
        animator.gameObject.SetActive(true);
        queue.Enqueue(new PopupData(achievementIcon, achievementTitle));

        if (!isShowing)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        isShowing = true;

        while (queue.Count > 0)
        {
            PopupData data = queue.Dequeue();

            icon.sprite = data.icon;
            title.text = data.title;

            gameObject.SetActive(true);
            animator.SetTrigger("Show");

            yield return new WaitForSeconds(displayTime);

            animator.SetTrigger("Hide");

            yield return new WaitForSeconds(0.5f); 
        }

        gameObject.SetActive(false);
        isShowing = false;
    }
}
