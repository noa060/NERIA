// ============================================
// 9. InteractionHintDisplayer.cs - 상호작용 힌트 표시
// ============================================

using UnityEngine;

public class InteractionHintDisplayer : MonoBehaviour
{
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private PlayerController player;

    private IInteractable currentInteractable = null;

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            player.transform.position + player.transform.forward,
            0.5f,
            player.transform.forward,
            detectionRange
        );

        IInteractable newInteractable = null;

        foreach (RaycastHit hit in hits)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                newInteractable = interactable;
                break;
            }
        }

        // 이전 상호작용 객체에서 힌트 숨김
        if (currentInteractable != null && currentInteractable != newInteractable)
        {
            currentInteractable.HideHint();
        }

        // 새로운 상호작용 객체에 힌트 표시
        if (newInteractable != null && newInteractable != currentInteractable)
        {
            newInteractable.ShowHint();
        }

        currentInteractable = newInteractable;
    }
}