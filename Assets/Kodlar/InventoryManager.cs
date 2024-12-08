using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryCanvas;
    private PlayerInventory playerInventory;

    public SlotManager[] slotManagers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.FindAnyObjectByType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryCanvas.active = !InventoryCanvas.active;
        }

        foreach (SlotManager slotManager in slotManagers)
        {
            foreach (var itemInfo in playerInventory.ItemInfos)
            {
                if (itemInfo.Quantity > 0)
                {
                    if (!itemInfo.IsCurrentlyHaving)
                    {
                        if (slotManager.ObjectName == "")
                        {
                            print(itemInfo.IsCurrentlyHaving + ", s" + itemInfo.Name);
                            slotManager.ObjectName = itemInfo.Name;
                            slotManager.ProductQuantity = itemInfo.Quantity;
                            itemInfo.IsCurrentlyHaving = true;
                        }
                    }
                    if (itemInfo.IsCurrentlyHaving)
                    {
                        if (slotManager.ObjectName == itemInfo.Name)
                        {
                            slotManager.ProductQuantity = itemInfo.Quantity;
                            slotManager.QuantityText.text = itemInfo.Quantity + "";
                        }

                    }
                }
                if (itemInfo.IsCurrentlyHaving)
                {

                    if (itemInfo.Quantity <= 0)
                    {
                        slotManager.ObjectName = "";
                        itemInfo.IsCurrentlyHaving = false;
                        slotManager.ProductQuantity = 0;
                        slotManager.QuantityText.text = "";
                    }
                }

            }
        }
    }
}

[System.Serializable]
public class SlotManager
{
    [Header("Yuvalar")]
    public string ObjectName;
    public int ProductQuantity;
    [Header("Envanter UI'ý")]
    public Image image;
    public Text QuantityText;
}
