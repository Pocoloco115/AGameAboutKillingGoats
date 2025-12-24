using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class ControlActionPrefab : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private GameObject highlightPanel;

    [Header("Config")]
    [SerializeField] private string actionName;
    [SerializeField] private KeyCode defaultKey = KeyCode.None;

    public string ActionName => actionName;
    public KeyCode CurrentKey { get; private set; }

    public System.Action OnKeyChanged;

    [HideInInspector] public ControlMenuManager manager;

    private bool isRebinding = false;
    private Coroutine blinkCoroutine;

    private void Awake()
    {
        highlightPanel.SetActive(false);
        CurrentKey = defaultKey;
        actionText.text = actionName;
        UpdateKeyText();
    }

    public void SetKey(KeyCode newKey)
    {
        CurrentKey = newKey;
        UpdateKeyText();
    }

    private void UpdateKeyText()
    {
        keyText.text = CurrentKey == KeyCode.None ? "?" : CurrentKey.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isRebinding)
            highlightPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isRebinding)
            highlightPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (isRebinding) return;

        StartRebind();
    }

    private void StartRebind()
    {
        isRebinding = true;
        highlightPanel.SetActive(false);

        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        blinkCoroutine = StartCoroutine(BlinkKeyText());
    }

    private IEnumerator BlinkKeyText()
    {
        while (isRebinding)
        {
            keyText.enabled = !keyText.enabled;
            yield return new WaitForSeconds(0.4f);
        }
        keyText.enabled = true;
    }

    private void OnGUI()
    {
        if (!isRebinding) return;

        Event e = Event.current;
        if (e == null) return;

        if (e.isKey)
        {
            if (e.keyCode == KeyCode.Escape)
            {
                FinishRebind(false);
                return;
            }

            CurrentKey = e.keyCode;
            UpdateKeyText();
            FinishRebind(true);
        }
    }

    private void FinishRebind(bool changed)
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        isRebinding = false;
        keyText.enabled = true;

        if (changed)
            OnKeyChanged?.Invoke();
    }
}