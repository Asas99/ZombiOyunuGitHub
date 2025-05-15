using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;
    public ItemType itemType;
    public int restoreAmount;
}

public enum ItemType
{
    Food,
    Drink
}
