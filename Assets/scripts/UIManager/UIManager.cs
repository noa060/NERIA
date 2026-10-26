using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] private CanvasGroup hintPanel;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private CanvasGroup dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void ShowHint(string text)
    {
        if (hintText != null)
        {
            hintText.text = text;
        }

        // hintPanel이 연결되어 있을 때만 페이드 효과 실행 (에러 방지)
        if (hintPanel != null)
        {
            StartCoroutine(FadeCanvasGroup(hintPanel, 0, 1, 0.2f));
        }
    }

    public void HideHint()
    {
        if (hintPanel != null)
        {
            StartCoroutine(FadeCanvasGroup(hintPanel, 1, 0, 0.2f));
        }
    }

    public void ShowDialogue(string name, string text, float duration = 3f)
    {
        if (speakerName != null) speakerName.text = name;
        if (dialogueText != null) dialogueText.text = text;
        StartCoroutine(ShowDialogueCoroutine(duration));
    }

    private IEnumerator ShowDialogueCoroutine(float displayDuration)
    {
        if (dialoguePanel != null)
            yield return StartCoroutine(FadeCanvasGroup(dialoguePanel, 0, 1, 0.3f));

        yield return new WaitForSeconds(displayDuration);

        if (dialoguePanel != null)
            yield return StartCoroutine(FadeCanvasGroup(dialoguePanel, 1, 0, 0.3f));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup group, float startAlpha, float endAlpha, float duration)
    {
        if (group == null) yield break;

        float elapsed = 0f;
        group.alpha = startAlpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        group.alpha = endAlpha;
    }
}