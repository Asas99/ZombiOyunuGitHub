using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxCapacity = 5; // Envanter kapasitesi
    public List<InventoryItem> items = new List<InventoryItem>();
    public InventoryUI inventoryUI;

    public void AddItem(InventoryItem newItem)
    {
        if (items.Count >= maxCapacity)
        {
            Debug.Log("Envanter dolu! Daha fazla eþya eklenemez.");
            return;
        }

        items.Add(newItem);
        inventoryUI.AddItemToUI(newItem);
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
        inventoryUI.RefreshUI(items);
    }
}
