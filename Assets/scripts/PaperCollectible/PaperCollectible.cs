// ============================================
// 5. PaperCollectible.cs - 낙서 수집 아이템
// ============================================

using UnityEngine;
using System.Collections;

public class PaperCollectible : MonoBehaviour, IInteractable
{
    [SerializeField] private int paperId;
    [SerializeField] private string paperName = "낙서";
    [SerializeField] private string paperDescription;
    [SerializeField] private float collectAnimDuration = 0.5f;

    private bool isCollected = false;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void Interact(PlayerController player)
    {
        if (isCollected) return;

        isCollected = true;

        // 인벤토리에 추가
        InventoryManager.instance.AddItem(paperId, paperName, paperDescription);

        // 애니메이션
        StartCoroutine(CollectAnimation());
    }

    private IEnumerator CollectAnimation()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * 2f;

        while (elapsed < collectAnimDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / collectAnimDuration;

            transform.position = Vector3.Lerp(startPos, endPos, t);
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, t);

            yield return null;
        }

        Destroy(gameObject);
    }

    public void ShowHint()
    {
        UIManager.instance?.ShowHint("GREAT");
    }

    public void HideHint()
    {
        UIManager.instance?.HideHint();
    }
}