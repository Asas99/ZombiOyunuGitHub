using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;

    public void AddItemToUI(InventoryItem item)
    {
        GameObject newSlot = Instantiate(slotPrefab, slotParent);
        newSlot.GetComponent<InventorySlotUI>().Set(item);
    }

    public void RefreshUI(List<InventoryItem> items)
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        foreach (var item in items)
            AddItemToUI(item);
    }
}
