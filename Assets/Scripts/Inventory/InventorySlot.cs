using System.Collections;
using System.Collections.Generic;
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

    public void Set(InventoryItem item)
    {
        icon.sprite = item?.data.icon;
        label.text = item?.data.displayName ?? "";
    }

    public void Awake()
    {
        // Revise for materials slot
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
        if (_inventorySystem.inventory.Count > 0)
        {
            Debug.Log("Updating inventory slot");
            foreach (var item in _inventorySystem.inventory)
            {
                // Revise for materials slot
                Set(item);
            }
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
