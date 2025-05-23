using UnityEngine;
using UnityEngine.UI;

public class SetWeaponTexts : MonoBehaviour
{
    public Image WeaponImage;
    public Sprite WeaponSprite;
    public Text AmmoText;
    public WeaponEquipManager weaponEquipManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (string.IsNullOrEmpty(weaponEquipManager.selectedTag))
        {
            WeaponImage.gameObject.SetActive(false);
            AmmoText.gameObject.SetActive(false);
        }
        else
        {
            WeaponImage.gameObject.SetActive(true);
            AmmoText.gameObject.SetActive(true);
            if (GameObject.FindGameObjectWithTag(weaponEquipManager.selectedTag).gameObject.GetComponent<WeaponInfo>().sprite != null)
            {
                WeaponImage.sprite = GameObject.FindGameObjectWithTag(weaponEquipManager.selectedTag).gameObject.GetComponent<WeaponInfo>().sprite;
            }
            
            AmmoText.text = GameObject.FindGameObjectWithTag(weaponEquipManager.selectedTag).gameObject.GetComponent<WeaponInfo>().CurrentAmmo + " / " + GameObject.FindGameObjectWithTag(weaponEquipManager.selectedTag).gameObject.GetComponent<WeaponInfo>().AmmoInCharger;
        }
    }
}
