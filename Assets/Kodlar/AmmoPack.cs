using UnityEngine;
using UnityEngine.UI;

public class AmmoPack : MonoBehaviour
{
    public GameObject[] CompatibleWeapons;
    public int AmmoCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectAmmo()
    {
        for (int i = 0; i < gameObject.GetComponent<AmmoPack>().CompatibleWeapons.Length; i++)
        {
            gameObject.GetComponent<AmmoPack>().CompatibleWeapons[i].GetComponent<WeaponInfo>()
.CurrentAmmo += gameObject.GetComponent<AmmoPack>().AmmoCount;
        }
    }
}
