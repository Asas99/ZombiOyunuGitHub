using System.Runtime.ExceptionServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipManager : MonoBehaviour
{
    public GameObject[] Weapons;
    [Header("Mevcut Sialh özellikleri")]
    public string Name;
    public string selectedTag;
    public float Damage;
    public float CurrentAmmo, MaxAmmo;
    public float ChargerSize;
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
                MaxAmmo = Weapon.GetComponent<WeaponInfo>().MaxAmmo;
                ChargerSize = Weapon.GetComponent<WeaponInfo>().chargersize;
                return true;
            }
        }
        return false;
    }
    public void DecreaseBullet()
    {
        foreach (var Weapon in Weapons)
        {
            if (selectedTag == Weapon.GetComponent<WeaponInfo>().Tag)
            {
                Weapon.GetComponent<WeaponInfo>().CurrentAmmo = CurrentAmmo;
            }
        }
    }
}
