using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject inventoryPanel;
    public GameObject optionsPanel;
    public Transform slotParent;
    public GameObject slotPrefab;
    public Button useButton;
    public Button dropButton;
    public Slider thirstSlider;
    public Slider hungerSlider;
    public GameObject cookButton;

    public GameObject firePrefab;
    public GameObject stickPrefab;
    public GameObject cookedFishPrefab;
    public Transform fireSpawnPoint;

    private List<InventoryItem> items = new();
    private InventoryItem currentItem;
    private GameObject currentSlot;
    public int maxItemCount = 20;

    void Awake() => Instance = this;

    void Start()
    {
        inventoryPanel.SetActive(false);
        optionsPanel.SetActive(false);
        cookButton.SetActive(false);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        CheckCraftingConditions();
    }

    public bool AddItem(InventoryItem item)
{
    if (items.Count >= maxItemCount)
    {
        Debug.LogWarning("Envanter dolu! Eşya alınamadı.");
        return false;
    }

    GameObject slot = Instantiate(slotPrefab, slotParent);
    slot.GetComponent<Image>().sprite = item.icon;
    item.slotObject = slot;
    items.Add(item);

    Button btn = slot.GetComponent<Button>();
    btn.onClick.AddListener(() => OnItemClick(item, slot));

    return true;
}

    void OnItemClick(InventoryItem item, GameObject slot)
    {
        currentItem = item;
        currentSlot = slot;
        optionsPanel.SetActive(true);

        useButton.onClick.RemoveAllListeners();
        dropButton.onClick.RemoveAllListeners();

        useButton.onClick.AddListener(() => UseItem());
        dropButton.onClick.AddListener(() => DropItem());
    }

    void UseItem()
    {
        if (currentItem.itemType == ItemType.Drink)
        {
            thirstSlider.value += currentItem.restoreAmount;
        }
        else if (currentItem.itemType == ItemType.Food)
        {
            hungerSlider.value += currentItem.restoreAmount;
        }

        RemoveCurrentItem();
    }

    void DropItem()
    {
        RemoveCurrentItem();
    }

    void RemoveCurrentItem()
    {
        items.Remove(currentItem);
        Destroy(currentItem.slotObject);
        optionsPanel.SetActive(false);
    }

    void CheckCraftingConditions()
    {
        bool hasRawFish = items.Any(i => i.itemType == ItemType.RawFish);
        bool hasLighter = items.Any(i => i.itemType == ItemType.Lighter);
        int stickCount = items.Count(i => i.itemType == ItemType.Stick);

        cookButton.SetActive(hasRawFish && hasLighter && stickCount >= 5);
    }

    public void CookFish()
    {
        bool hasRawFish = RemoveItem(ItemType.RawFish);
        bool hasLighter = RemoveItem(ItemType.Lighter);
        bool removedSticks = RemoveItems(ItemType.Stick, 5);

        if (hasRawFish && hasLighter && removedSticks)
        {
            GameObject sticks = Instantiate(stickPrefab, fireSpawnPoint.position, Quaternion.identity);
            GameObject fire = Instantiate(firePrefab, fireSpawnPoint.position + Vector3.up * 0.5f, Quaternion.identity);

            StartCoroutine(FinishCooking(sticks, fire));
        }
        else
        {
            Debug.LogWarning("Gerekli malzemeler eksik.");
        }
    }

    IEnumerator<WaitForSeconds> FinishCooking(GameObject sticks, GameObject fire)
    {
        yield return new WaitForSeconds(5f);

        Destroy(fire);
        Destroy(sticks);

        Vector3 spawnPos = fireSpawnPoint.position + Vector3.up * 0.2f;
        Instantiate(cookedFishPrefab, spawnPos, Quaternion.identity);
    }

    public bool HasItem(ItemType type)
    {
        return items.Any(i => i.itemType == type);
    }

    public bool RemoveItem(ItemType type)
    {
        InventoryItem item = items.FirstOrDefault(i => i.itemType == type);
        if (item != null)
        {
            items.Remove(item);
            Destroy(item.slotObject);
            return true;
        }
        return false;
    }

    bool RemoveItems(ItemType type, int count)
    {
        var foundItems = items.Where(i => i.itemType == type).Take(count).ToList();
        if (foundItems.Count == count)
        {
            foreach (var item in foundItems)
            {
                items.Remove(item);
                Destroy(item.slotObject);
            }
            return true;
        }
        return false;
    }
}
