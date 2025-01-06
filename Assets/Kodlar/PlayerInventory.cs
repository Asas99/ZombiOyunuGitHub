using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public ItemInfo[] ItemInfos;
    public WeaponEquipManager WeaponEquipManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #region ÖnAyarlar
        foreach (var Item in ItemInfos)
        {
            if (Item.Name == "colt ammo")
            {
                Item.MaxQuantity = (int)(WeaponEquipManager.Weapons[0].GetComponent<WeaponInfo>().chargersize * 2);
            }
            if (Item.Name == "ak47 ammo")
            {
                Item.MaxQuantity = (int)(WeaponEquipManager.Weapons[1].GetComponent<WeaponInfo>().chargersize * 2);
            }
            if (Item.Name == "krag ammo")
            {
                Item.MaxQuantity = (int)(WeaponEquipManager.Weapons[2].GetComponent<WeaponInfo>().chargersize * 2);
            }
            if (Item.Name == "remington ammo")
            {
                Item.MaxQuantity = (int)(WeaponEquipManager.Weapons[3].GetComponent<WeaponInfo>().chargersize * 2);
            }
            if (Item.Name == "revolver ammo")
            {
                Item.MaxQuantity = (int)(WeaponEquipManager.Weapons[4].GetComponent<WeaponInfo>().chargersize * 2);
            }
            if (Item.Name == "winchester ammo")
            {
                Item.MaxQuantity = (int)(WeaponEquipManager.Weapons[5].GetComponent<WeaponInfo>().chargersize * 2);
                Item.MaxQuantity = (int)(WeaponEquipManager.Weapons[6].GetComponent<WeaponInfo>().chargersize * 2);
            }
        }
        #endregion
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
    public bool IsCurrentlyHaving;
    public int Quantity;
    public int MaxQuantity;
    public Sprite sprite;
}

