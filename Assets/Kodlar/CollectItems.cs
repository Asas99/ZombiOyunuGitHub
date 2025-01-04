using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    public string TagName;
    public PlayerInventory playerInventory;
    [Header("Animatör")]
    public float Dist, MaxCollectDist;
    public GameObject Text;
    public Animator animator;
    private WeaponEquipManager weaponEquipManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TagName = transform.tag;
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        if (Text != null)
        {
            Text.SetActive(false);
            weaponEquipManager = FindAnyObjectByType<WeaponEquipManager>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (AlexAnimator.GetCurrentAnimatorStateInfo(0).IsName("Take item"))
        //{
        //    if (AlexAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        //    {
        //        AlexAnimator.SetBool("Take item", false);
        //    }
        //}
        Dist = Vector3.Distance(transform.position, GameObject.FindAnyObjectByType<PlayerInventory>().gameObject.transform.position);

        foreach (var item in playerInventory.ItemInfos)
        {
            if (item.Quantity > item.MaxQuantity)
            {
                item.Quantity = item.MaxQuantity;
            }
        }
        if (Dist < MaxCollectDist)
        {
            if (Text != null)
            {
                Text.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Collect();

            }
        }
        else
        {
            if(Text != null)
            {
                Text.SetActive(false);
            }
        }
    }

    public void Collect()
    {

        switch (TagName)
        {
            case "Colt":
                if (playerInventory.ItemInfos[0].Quantity < playerInventory.ItemInfos[0].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("Colt");
                    playerInventory.ItemInfos[0].Quantity++;
                    //playerInventory.ItemInfos[0].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;

            case "gofret":
                if (playerInventory.ItemInfos[1].Quantity < playerInventory.ItemInfos[1].MaxQuantity)
                {
                    playerInventory.ItemInfos[1].Quantity++;
                    //playerInventory.ItemInfos[1].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    Destroy(gameObject);
                }
                break;

            case "tahta":
                if (playerInventory.ItemInfos[2].Quantity < playerInventory.ItemInfos[2].MaxQuantity)
                {
                    playerInventory.ItemInfos[2].Quantity ++;
                    //playerInventory.ItemInfos[2].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    Destroy(gameObject);
                }
                break;

            case "ak47":
                if (playerInventory.ItemInfos[3].Quantity < playerInventory.ItemInfos[3].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("ak47");
                    playerInventory.ItemInfos[3].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;
            case "Krag-Jergensen":
                if (playerInventory.ItemInfos[4].Quantity < playerInventory.ItemInfos[4].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("Krag-Jergensen");
                    playerInventory.ItemInfos[4].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;
            case "remington":
                if (playerInventory.ItemInfos[5].Quantity < playerInventory.ItemInfos[5].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("remington");
                    playerInventory.ItemInfos[5].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;
            case "revolver":
                if (playerInventory.ItemInfos[6].Quantity < playerInventory.ItemInfos[6].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("revolver");
                    playerInventory.ItemInfos[6].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;
            case "springfield":
                if (playerInventory.ItemInfos[7].Quantity < playerInventory.ItemInfos[7].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("springfield");
                    playerInventory.ItemInfos[7].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;
            case "winchester1897":
                if (playerInventory.ItemInfos[8].Quantity < playerInventory.ItemInfos[8].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("springfield");
                    playerInventory.ItemInfos[8].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;
            case "winchester1894":
                if (playerInventory.ItemInfos[9].Quantity < playerInventory.ItemInfos[9].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("springfield");
                    playerInventory.ItemInfos[9].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.SetActive(false);
                }
                break;

            case "virüs belgeleri":
                GameObject.FindObjectOfType<MissionManager>().FindVirusDocuments = true;
                break;
        }
    }
}

