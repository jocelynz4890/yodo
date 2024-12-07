using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] 
    private Image icon;
    
    [SerializeField] 
    private TextMeshProUGUI label;
    
    private InventorySystem _inventorySystem;

    private void Set(InventoryItem item)
    {
        icon.sprite = item?.data.icon;
        label.text = item?.data.displayName ?? "";
    }

    public void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        label = transform.Find("Label").GetComponent<TextMeshProUGUI>();
        
        _inventorySystem = GetComponentInParent<InventorySystem>();
        if (_inventorySystem == null)
        {
            Debug.LogError("InventorySystem not found in parent hierarchy.");
            return;
        }
        InventorySystem.OnInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        if (_inventorySystem.weapon != null)
        {
            _inventorySystem.itemDictionary.TryGetValue(_inventorySystem.weapon, out var item);
            Debug.Log("Updating inventory slot");
            Set(item);
        }
        else
        {
            Debug.Log("Clearing inventory slot");
            Set(null);
        }
    }
    
    private void OnDestroy()
    {
        InventorySystem.OnInventoryChangedEvent -= OnUpdateInventory;
    }
}
