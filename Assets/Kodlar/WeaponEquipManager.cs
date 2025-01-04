using System.Runtime.ExceptionServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipManager : MonoBehaviour
{
    public GameObject[] Weapons;

    [Header("Mevcut Sialh �zellikleri")]
    public string Name;
    public float Damage;
    public float CurrentAmmo, MaxAmmo;
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
                //Tag = Weapon.GetComponent<WeaponInfo>().Tag;
                Damage = Weapon.GetComponent<WeaponInfo>().Damage;
                CurrentAmmo = Weapon.GetComponent<WeaponInfo>().CurrentAmmo;
                MaxAmmo = Weapon.GetComponent<WeaponInfo>().MaxAmmo;
                return true;
            }
        }
        return false;
    }
}
