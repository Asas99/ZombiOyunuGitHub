using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    public string TagName;
    public PlayerInventory playerInventory;
    [Header("Animatör")]
    public Animator AlexAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TagName = transform.tag;
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AlexAnimator.GetCurrentAnimatorStateInfo(0).IsName("Take item"))
        {
            if (AlexAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                AlexAnimator.SetBool("Take item", false);
            }
        }

        foreach (var item in playerInventory.ItemInfos)
        {
            if (playerInventory.ItemInfos[0].Quantity > playerInventory.ItemInfos[0].MaxQuantity)
            {
                playerInventory.ItemInfos[0].Quantity = playerInventory.ItemInfos[0].MaxQuantity;
            }
        }
    }

    public void OnMouseDown()
    {

        switch (TagName)
        {
            case "Çöl kartalý":
                if (playerInventory.ItemInfos[0].Quantity < playerInventory.ItemInfos[0].MaxQuantity)
                {
                    playerInventory.ItemInfos[0].Quantity++;
                    //playerInventory.ItemInfos[0].IsCurrentlyHaving = true;
                    AlexAnimator.SetBool("Take item", true);
                    Destroy(gameObject);
                }
                break;

            case "gofret":
                if (playerInventory.ItemInfos[1].Quantity < playerInventory.ItemInfos[1].MaxQuantity)
                {
                    playerInventory.ItemInfos[1].Quantity++;
                    //playerInventory.ItemInfos[1].IsCurrentlyHaving = true;
                    AlexAnimator.SetBool("Take item", true);
                    Destroy(gameObject);
                }
                break;

            case "tahta":
                if (playerInventory.ItemInfos[2].Quantity < playerInventory.ItemInfos[2].MaxQuantity)
                {
                    playerInventory.ItemInfos[2].Quantity += 5;
                    //playerInventory.ItemInfos[2].IsCurrentlyHaving = true;
                    AlexAnimator.SetBool("Take item", true);
                    Destroy(gameObject);
                }
                break;

            case "virüs belgeleri":
                GameObject.FindObjectOfType<MissionManager>().FindVirusDocuments = true;
                break;
        }
    }
}

