using UnityEngine;
using UnityEngine.UI;

public class BuildingLoot : MonoBehaviour
{
    public bool CanCollect, IsCollected;
    public PlayerInventory inventory;
    public int ColtCount;
    public int GofretCount;
    public int TahtaCount;
    public int Ak47Count;
    public int KragJergensenCount;
    public int RemingtonCount;
    public int RevolverCount;
    public int SpringfieldCount ;
    public int Winchester1897Count;
    public int Winchester1894Count;
    public int ColtAmmoCount;
    public int Ak47AmmoCount;
    public int KragJergessenAmmoCount;
    public int RemingtonAmmoCount;
    public int RevolverAmmoCount;
    public int SpringfieldAmmoCount;
    public int WinchesterAmmoCount;


    public void Awake()
    {
    ColtCount = Random.Range(0, 2);
    GofretCount = Random.Range(0, 2);
    TahtaCount = Random.Range(0, 2);
    Ak47Count = Random.Range(0, 2);
    KragJergensenCount = Random.Range(0, 2);
    RemingtonCount = Random.Range(0, 2);
    RevolverCount = Random.Range(0, 2);
    SpringfieldCount = Random.Range(0, 2);
    Winchester1897Count = Random.Range(0, 2);
    Winchester1894Count = Random.Range(0, 2);
    ColtAmmoCount = Random.Range(0, 2);
    Ak47AmmoCount = Random.Range(0, 2);
    KragJergessenAmmoCount = Random.Range(0, 2);
    RemingtonAmmoCount = Random.Range(0, 2);
    RevolverAmmoCount = Random.Range(0, 2);
    SpringfieldAmmoCount = Random.Range(0, 2);
    WinchesterAmmoCount = Random.Range(0, 2);
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindFirstObjectByType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (!IsCollected)
            {
                if (CanCollect)
                {
                    inventory.ItemInfos[0].Quantity += ColtCount;
                    inventory.ItemInfos[1].Quantity += GofretCount;
                    inventory.ItemInfos[2].Quantity += TahtaCount;
                    inventory.ItemInfos[2].Quantity += Ak47Count;
                    inventory.ItemInfos[3].Quantity += KragJergensenCount;
                    inventory.ItemInfos[4].Quantity += RemingtonCount;
                    inventory.ItemInfos[5].Quantity += RevolverCount;
                    inventory.ItemInfos[6].Quantity += SpringfieldCount;
                    inventory.ItemInfos[7].Quantity += Winchester1897Count;
                    inventory.ItemInfos[8].Quantity += Winchester1894Count;
                    inventory.ItemInfos[9].Quantity += ColtAmmoCount;
                    inventory.ItemInfos[10].Quantity += Ak47AmmoCount;
                    inventory.ItemInfos[11].Quantity += KragJergessenAmmoCount;
                    inventory.ItemInfos[12].Quantity += RemingtonAmmoCount;
                    inventory.ItemInfos[13].Quantity += RevolverAmmoCount;
                    inventory.ItemInfos[14].Quantity += SpringfieldAmmoCount;
                    inventory.ItemInfos[15].Quantity += WinchesterAmmoCount;
                    IsCollected = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanCollect = true;  
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanCollect = false;
        }
    }
}
