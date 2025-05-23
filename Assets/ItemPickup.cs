using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public ItemType type;
    public int effectAmount;

    public GameObject textObject;   // Mesafeye göre gösterilecek text objesi
    public float showDistance = 3f; // Mesafe sınırı

    private Transform player;
    private bool playerInRange = false; // Mesafe içindeyse true olur

    void Awake()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogWarning("Player objesi bulunamadı! Tag 'Player' olarak ayarlı mı?");
        }

        if (textObject != null)
            textObject.SetActive(false);
    }

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

    // Direkt objenin triggerına girince otomatik alma
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryPickupItem();
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
