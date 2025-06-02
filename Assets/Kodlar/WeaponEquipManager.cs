using System.Runtime.ExceptionServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipManager : MonoBehaviour
{
    public GameObject[] Weapons;
    [Header("Mevcut Silah özellikleri")]
    public string Name;
    public string selectedTag;
    public float Damage;
    public float CurrentAmmo, MaxAmmo;
    public float ChargerSize;
    public float BulletInCharger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public bool AssignWeapon(string Tag)
    {
        foreach (var Weapon in Weapons)
        {
            if (Tag == Weapon.GetComponent<WeaponInfo>().Tag)
            {
                Name = Weapon.GetComponent<WeaponInfo>().name;
                selectedTag = Weapon.GetComponent<WeaponInfo>().Tag;
                Damage = Weapon.GetComponent<WeaponInfo>().Damage;
                CurrentAmmo = Weapon.GetComponent<WeaponInfo>().CurrentAmmo;
                BulletInCharger = Weapon.GetComponent<WeaponInfo>().AmmoInCharger;
                if (CurrentAmmo >= Weapon.GetComponent<WeaponInfo>().chargersize)
                {
                    Weapon.GetComponent<WeaponInfo>().AmmoInCharger = Weapon.GetComponent<WeaponInfo>().chargersize;
                }
                if (CurrentAmmo < Weapon.GetComponent<WeaponInfo>().chargersize)
                {
                    Weapon.GetComponent<WeaponInfo>().AmmoInCharger = Weapon.GetComponent<WeaponInfo>().CurrentAmmo;
                }
                ChargerSize = Weapon.GetComponent<WeaponInfo>().chargersize;
                return true;
            }
        }
        return false;
    }
    public void DecreaseBullet()
    {

        if (!selectedTag.Contains("winchester"))
        {
            foreach (var Weapon in Weapons)
            {
                if (selectedTag == Weapon.GetComponent<WeaponInfo>().Tag)
                {
                    CurrentAmmo = Weapon.GetComponent<WeaponInfo>().CurrentAmmo;
                    Weapon.GetComponent<WeaponInfo>().CurrentAmmo--;
                    Weapon.GetComponent<WeaponInfo>().AmmoInCharger--;
                    //Weapon.GetComponent<WeaponInfo>().CurrentAmmo = CurrentAmmo;

                }
            }
        }
        else
        {
            Weapons[6].GetComponent<WeaponInfo>().CurrentAmmo--;
            Weapons[7].GetComponent<WeaponInfo>().CurrentAmmo--;
            Weapons[6].GetComponent<WeaponInfo>().AmmoInCharger--;
            Weapons[7].GetComponent<WeaponInfo>().AmmoInCharger--;
            Weapons[6].GetComponent<WeaponInfo>().CurrentAmmo = CurrentAmmo;
            Weapons[7].GetComponent<WeaponInfo>().CurrentAmmo = CurrentAmmo;
        }
    }
}
