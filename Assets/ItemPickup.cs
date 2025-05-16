using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public GameManager.ItemType type;
    public int effectAmount;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddItem(new GameManager.InventoryItem
            {
                itemName = itemName,
                icon = icon,
                type = type,
                effectAmount = effectAmount
            });

            Destroy(gameObject);
        }
    }
}
