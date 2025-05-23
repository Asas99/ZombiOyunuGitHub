using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public ItemType type;
    public int effectAmount;

    public Transform player;          // Player Transform'u
    public GameObject textObject;     // Text objesi (Canvas veya 3D Text)
    public float showDistance = 3f;   // Görünme mesafesi

    private bool playerInRange = false; // Oyuncu menzilde mi?

    void Update()
    {
        if (player == null || textObject == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= showDistance)
        {
            if (!textObject.activeSelf)
                textObject.SetActive(true);

            playerInRange = true;

            // E tuşuna basıldıysa itemi alma işlemi
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickupItem();
            }
        }
        else
        {
            if (textObject.activeSelf)
                textObject.SetActive(false);

            playerInRange = false;
        }
    }

    void TryPickupItem()
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
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Envanter dolu! Obje alınamadı.");
        }
    }
}
