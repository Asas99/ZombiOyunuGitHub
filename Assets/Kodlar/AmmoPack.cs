using UnityEngine;
using UnityEngine.UI;

public class AmmoPack : MonoBehaviour
{
    public GameObject[] CompatibleWeapons;
    public int AmmoCount;
    public Animator animator;
    public PlayerInventory playerInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GameObject.FindAnyObjectByType<CharacterController>().GetComponent<Animator>();
        playerInventory = GameObject.FindAnyObjectByType<PlayerInventory>();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectAmmo();
        }
    }

    public void CollectAmmo()
    {
        for (int i = 0; i < gameObject.GetComponent<AmmoPack>().CompatibleWeapons.Length; i++)
        {
            gameObject.GetComponent<AmmoPack>().CompatibleWeapons[i].GetComponent<WeaponInfo>().CurrentAmmo += gameObject.GetComponent<AmmoPack>().AmmoCount;
        }
        //playerInventory.ItemInfos[16].Quantity++;
        //gameObject.GetComponent<AmmoPack>().CollectAmmo();
        AnimatorManager.SetAllAnimatorBools(animator, "Take item");
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
    }
}