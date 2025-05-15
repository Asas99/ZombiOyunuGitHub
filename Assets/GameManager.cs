using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public Sprite icon;
        public ItemType type;
        public int effectAmount;
        public GameObject worldPrefab; // Dünyaya düþecek prefab
    }

    public enum ItemType { Food, Drink }

    [Header("UI")]
    public GameObject inventoryPanel;
    public GameObject optionsPanel;
    public Transform slotParent;
    public GameObject slotPrefab;
    public Button useButton;
    public Button dropButton;
    public Slider thirstSlider;
    public Slider hungerSlider;

    [Header("Item Settings")]
    public Sprite waterIcon;

    [Header("Drop Settings")]
    public Transform player; // Oyuncunun konumu

    private List<InventoryItem> items = new();
    private InventoryItem currentItem;
    private GameObject currentSlot;

    void Start()
    {
        inventoryPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        GameObject slot = Instantiate(slotPrefab, slotParent);
        slot.GetComponent<Image>().sprite = item.icon;

        Button btn = slot.GetComponent<Button>();
        btn.onClick.AddListener(() => OnItemClick(item, slot));
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
        if (currentItem.type == ItemType.Drink)
        {
            thirstSlider.value += currentItem.effectAmount;
            Debug.Log("Su içildi, susuzluk azaldý");
        }
        else if (currentItem.type == ItemType.Food)
        {
            hungerSlider.value += currentItem.effectAmount;
            Debug.Log("Yemek yendi, açlýk azaldý");
        }

        RemoveCurrentItem();
    }

    void DropItem()
    {
        if (currentItem.worldPrefab != null && player != null)
        {
            Vector3 dropPosition = player.position + player.forward * 1.5f;
            Instantiate(currentItem.worldPrefab, dropPosition, Quaternion.identity);
        }

        RemoveCurrentItem();
    }

    void RemoveCurrentItem()
    {
        items.Remove(currentItem);
        Destroy(currentSlot);
        optionsPanel.SetActive(false);
    }
}
