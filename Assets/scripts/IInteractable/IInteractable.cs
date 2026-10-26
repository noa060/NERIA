// ============================================
// 3. IInteractable.cs - 상호작용 인터페이스
// ============================================

using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerController player);
    void ShowHint();
    void HideHint();
}