using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public ItemInfo[] ItemInfos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // 0- Çöl kartalý
    // 1- Gofret
    // 2- Tahta
    void Update()
    {
    }
}

[System.Serializable]
public class ItemInfo
{
    public string Name;
    public int Quantity;
    public int MaxQuantity;
}

