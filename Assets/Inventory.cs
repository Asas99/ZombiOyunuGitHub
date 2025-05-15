using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public InventoryUI inventoryUI;

    public void AddItem(InventoryItem newItem)
    {
        items.Add(newItem);
        inventoryUI.AddItemToUI(newItem);
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
        inventoryUI.RefreshUI(items);
    }
}
