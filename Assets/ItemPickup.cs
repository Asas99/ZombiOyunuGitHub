using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public ItemType type;
    public int effectAmount;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryItem newItem = new InventoryItem
            {
                itemName = itemName,
                icon = icon,
                itemType = type,
                restoreAmount = effectAmount
            };

            bool added = GameManager.Instance.AddItem(newItem);

            if (added)
            {
                Destroy(gameObject); // Sadece envantere eklenebildiyse yok et
            }
            else
            {
                Debug.Log("Envanter dolu! Obje alınamadı.");
            }
        }
    }
}
