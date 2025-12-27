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

    private Vector3 savedMousePosition;

    public string ActionName => actionName;
    public KeyCode CurrentKey { get; private set; }

    public System.Action OnKeyChanged;

    [HideInInspector] public ControlMenuManager manager;

    private bool isRebinding = false;
    private Coroutine blinkCoroutine;
    private KeyCode previousKey;

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
        keyText.color = Color.black; 
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
        if (manager != null && !manager.CanStartRebind(this)) return;
        StartRebind();
    }


    private void StartRebind()
    {
        isRebinding = true;
        highlightPanel.SetActive(true);
        previousKey = CurrentKey;

        if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);

        blinkCoroutine = StartCoroutine(BlinkKeyText());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        manager?.NotifyRebindStarted(this);
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

            if (manager != null && manager.IsKeyInUse(e.keyCode, this))
            {
                keyText.text = "Usado";
                keyText.color = Color.red;
                Invoke(nameof(RestorePreviousKey), 1f);
                return;
            }

            CurrentKey = e.keyCode;
            UpdateKeyText();
            FinishRebind(true);
        }

        if (e.type == EventType.MouseDown)
        {
            KeyCode mouseKey = KeyCode.None;
            switch (e.button)
            {
                case 0: mouseKey = KeyCode.Mouse0; break;
                case 1: mouseKey = KeyCode.Mouse1; break;
                case 2: mouseKey = KeyCode.Mouse2; break;
                case 3: mouseKey = KeyCode.Mouse3; break;
                case 4: mouseKey = KeyCode.Mouse4; break;
                case 5: mouseKey = KeyCode.Mouse5; break;
                case 6: mouseKey = KeyCode.Mouse6; break;
            }

            if (mouseKey != KeyCode.None)
            {
                if (manager != null && manager.IsKeyInUse(mouseKey, this))
                {
                    keyText.text = "Usado";
                    keyText.color = Color.red;
                    Invoke(nameof(RestorePreviousKey), 1f);
                    return;
                }

                CurrentKey = mouseKey;
                UpdateKeyText();
                FinishRebind(true);
            }
        }
    }

    private void RestorePreviousKey()
    {
        CurrentKey = previousKey; 
        UpdateKeyText();
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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        highlightPanel.SetActive(false);

        manager?.NotifyRebindFinished(this);

        if (changed)
            OnKeyChanged?.Invoke();
    }
}