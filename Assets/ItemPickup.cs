using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameManager.InventoryItem itemData;

    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<GameManager>().AddItem(itemData);
            Destroy(gameObject); // item yerden kaybolur
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }
}
