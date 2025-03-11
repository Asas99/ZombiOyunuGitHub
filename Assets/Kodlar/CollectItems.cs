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
        if (GameObject.FindAnyObjectByType<PlayerInventory>()!= null)
        {
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
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
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
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "Krag-Jergensen":
                if (playerInventory.ItemInfos[4].Quantity < playerInventory.ItemInfos[4].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("Krag-Jergensen");
                    playerInventory.ItemInfos[4].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "remington":
                if (playerInventory.ItemInfos[5].Quantity < playerInventory.ItemInfos[5].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("remington");
                    playerInventory.ItemInfos[5].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "revolver":
                if (playerInventory.ItemInfos[6].Quantity < playerInventory.ItemInfos[6].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("revolver");
                    playerInventory.ItemInfos[6].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = false;
                    gameObject.transform.GetChild(0).transform.gameObject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.transform.GetChild(0).transform.gameObject.transform.GetChild(0).transform.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "springfield":
                if (playerInventory.ItemInfos[7].Quantity < playerInventory.ItemInfos[7].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("springfield");
                    playerInventory.ItemInfos[7].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "winchester1897":
                if (playerInventory.ItemInfos[8].Quantity < playerInventory.ItemInfos[8].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("winchester1897");
                    playerInventory.ItemInfos[8].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "winchester1894":
                if (playerInventory.ItemInfos[9].Quantity < playerInventory.ItemInfos[9].MaxQuantity)
                {
                    weaponEquipManager.AssignWeapon("winchester1894");
                    playerInventory.ItemInfos[9].Quantity++;
                    //playerInventory.ItemInfos[3].IsCurrentlyHaving = true;
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;

            case "colt ammo":
                if (playerInventory.ItemInfos[10].Quantity < playerInventory.ItemInfos[10].MaxQuantity)
                {
                    playerInventory.ItemInfos[10].Quantity++;
                    gameObject.GetComponent<AmmoPack>().CollectAmmo();
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "ak 47 ammo":
                if (playerInventory.ItemInfos[11].Quantity < playerInventory.ItemInfos[11].MaxQuantity)
                {
                    playerInventory.ItemInfos[11].Quantity++;
                    gameObject.GetComponent<AmmoPack>().CollectAmmo();
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "krag ammo":
                if (playerInventory.ItemInfos[12].Quantity < playerInventory.ItemInfos[12].MaxQuantity)
                {
                    playerInventory.ItemInfos[12].Quantity++;
                    gameObject.GetComponent<AmmoPack>().CollectAmmo();
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "remington ammo":
                if (playerInventory.ItemInfos[13].Quantity < playerInventory.ItemInfos[13].MaxQuantity)
                {
                    playerInventory.ItemInfos[13].Quantity++;
                    gameObject.GetComponent<AmmoPack>().CollectAmmo();
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "revolver ammo":
                if (playerInventory.ItemInfos[14].Quantity < playerInventory.ItemInfos[14].MaxQuantity)
                {
                    playerInventory.ItemInfos[14].Quantity++;
                    gameObject.GetComponent<AmmoPack>().CollectAmmo();
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "winchester ammo":
                if (playerInventory.ItemInfos[15].Quantity < playerInventory.ItemInfos[15].MaxQuantity)
                {
                    playerInventory.ItemInfos[15].Quantity++;
                    gameObject.GetComponent<AmmoPack>().CollectAmmo();
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;
            case "springfield ammo":
                if (playerInventory.ItemInfos[16].Quantity < playerInventory.ItemInfos[16].MaxQuantity)
                {
                    playerInventory.ItemInfos[16].Quantity++;
                    gameObject.GetComponent<AmmoPack>().CollectAmmo();
                    AnimatorManager.SetAllAnimatorBools(animator, "Take item");
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(Text);
                }
                break;

            case "virüs belgeleri":
                GameObject.FindObjectOfType<MissionManager>().FindVirusDocuments = true;
                break;
        }
    }
}

