// ============================================
// 4. InventoryManager.cs - 인벤토리 관리
// ============================================

using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    [SerializeField] private TextMeshProUGUI collectionCountText;

    [System.Serializable]
    public class CollectibleItem
    {
        public int id;
        public string name;
        public string description;
        public float collectTime;
    }

    private List<CollectibleItem> collectedItems = new List<CollectibleItem>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void AddItem(int id, string name, string description)
    {
        CollectibleItem item = new CollectibleItem
        {
            id = id,
            name = name,
            description = description,
            collectTime = Time.time
        };

        collectedItems.Add(item);
        UpdateUI();

        // 효과음
        AudioManager.instance?.PlaySound("pickup");
    }

    public int GetItemCount() => collectedItems.Count;

    public List<CollectibleItem> GetAllItems() => collectedItems;

    public void ClearInventory() => collectedItems.Clear();

    private void UpdateUI()
    {
        if (collectionCountText != null)
        {
            collectionCountText.text = $"수집: {collectedItems.Count}";
        }
    }
}