using UnityEngine;

public class SimpleCollect : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController player)
    {
        Debug.Log("수집됨!");
        UIManager.instance.ShowHint("Great");

        // 1. 상자의 콜라이더를 즉시 비활성화하여 플레이어와의 상호작용 충돌을 완전히 끊습니다.
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 2. 상자를 곧바로 파괴합니다.
        Destroy(gameObject);
    }

    public void ShowHint() { }
    public void HideHint() { }
}