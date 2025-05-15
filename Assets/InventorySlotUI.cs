using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage;
    private InventoryItem item;

    public void Set(InventoryItem newItem)
    {
        item = newItem;
        iconImage.sprite = item.icon;
    }

    public void OnClick()
    {
        Debug.Log("T�klanan e�ya: " + item.itemName);
        // OptionsPanel a��lacak
    }
}
