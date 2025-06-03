using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeaponInHand : MonoBehaviour
{
    public GameObject[] WeaponsInHand;
    public WeaponEquipManager weaponEquipManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (weaponEquipManager != null && !string.IsNullOrEmpty(weaponEquipManager.selectedTag))
        {
            foreach (var weapon in WeaponsInHand)
            {
                if (weapon.CompareTag(weaponEquipManager.selectedTag))
                {
                    weapon.SetActive(true);
                    //print("bulundu tag" + weaponEquipManager.selectedTag);
                }
                else
                {
                    weapon.SetActive(false);
                }
            }
        }
        if (weaponEquipManager != null && string.IsNullOrEmpty(weaponEquipManager.selectedTag))
        {
            foreach (var weapon in WeaponsInHand)
            {
                if (weapon.tag != weaponEquipManager.selectedTag)
                {
                    //print(weapon.gameObject.name + "IsDeactivated");
                    weapon.SetActive(false);
                }
            }
        }
    }
}
