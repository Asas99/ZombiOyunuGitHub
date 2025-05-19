using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public ItemType type; // ✅ GameManager.ItemType değil
    public int effectAmount;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryItem newItem = new InventoryItem
            {
                itemName = itemName,
                icon = icon,
                itemType = type, // ✅ type yerine itemType
                restoreAmount = effectAmount
            };

            GameManager.Instance.AddItem(newItem);
            Destroy(gameObject);
        }
    }
}
