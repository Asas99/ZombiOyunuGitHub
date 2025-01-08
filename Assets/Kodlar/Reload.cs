using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    public WeaponInfo WeaponInfo;
    public WeaponEquipManager WeaponEquipManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WeaponInfo = GetComponent<WeaponInfo>();
        WeaponEquipManager = GameObject.FindAnyObjectByType<WeaponEquipManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        ReloadCharger();
    }
    public void ReloadCharger()
    {
        foreach (var Weapon in WeaponEquipManager.Weapons)
        {
            if (Weapon.GetComponent<WeaponInfo>().Tag == WeaponEquipManager.selectedTag)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (Weapon.GetComponent<WeaponInfo>().AmmoInCharger == 0)
                    {
                        if (Weapon.GetComponent<WeaponInfo>().CurrentAmmo >= Weapon.GetComponent<WeaponInfo>().chargersize)
                        {
                            Weapon.GetComponent<WeaponInfo>().AmmoInCharger = Weapon.GetComponent<WeaponInfo>().chargersize;
                            WeaponEquipManager.BulletInCharger = Weapon.GetComponent<WeaponInfo>().AmmoInCharger;
                        }
                        if (Weapon.GetComponent<WeaponInfo>().CurrentAmmo < Weapon.GetComponent<WeaponInfo>().chargersize)
                        {
                            Weapon.GetComponent<WeaponInfo>().AmmoInCharger = Weapon.GetComponent<WeaponInfo>().CurrentAmmo;
                            WeaponEquipManager.BulletInCharger = Weapon.GetComponent<WeaponInfo>().AmmoInCharger;
                        }
                    }
                }
            }
        }
    }
}
