// ============================================
// 8. NPCController.cs - NPC 기본 템플릿
// ============================================

using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] private string npcName = "NPC";
    [SerializeField] private string[] dialogues;
    [SerializeField] private Animator animator;

    private int dialogueIndex = 0;

    public void Interact(PlayerController player)
    {
        if (dialogueIndex < dialogues.Length)
        {
            UIManager.instance?.ShowDialogue(npcName, dialogues[dialogueIndex], 3f);
            animator.SetTrigger("Talk");
            dialogueIndex++;
        }
    }

    public void ShowHint()
    {
        UIManager.instance?.ShowHint("E - 대화하기");
    }

    public void HideHint()
    {
        UIManager.instance?.HideHint();
    }
}