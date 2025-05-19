using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;
    public ItemType itemType;
    public int restoreAmount;

    // ✅ Bu satırı ekle
    public GameObject slotObject;
}

public enum ItemType
{
    Food,
    Drink,
    Stick,
    Lighter,
    RawFish,
    CookedFish,
    FuelCan
}
