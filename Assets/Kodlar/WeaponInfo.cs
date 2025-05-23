using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    public string Name;
    public string Tag;
    public float Damage;
    public float CurrentAmmo;
    public float chargersize;
    public float AmmoInCharger;
    public Sprite sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tag = gameObject.tag;
        if (CurrentAmmo >= chargersize)
        {
            AmmoInCharger = chargersize;
        }
        else
        {
            AmmoInCharger = CurrentAmmo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
