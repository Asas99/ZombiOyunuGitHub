using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    public string Name;
    public string Tag;
    public float Damage;
    public float CurrentAmmo, MaxAmmo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
