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
    
    private InventorySystem inventorySystem;

    public void Set(InventoryItem item)
    {
        Debug.Log(item != null ? "Updating UI inventory slot." : "Clearing UI inventory slot.");
        icon.sprite = item?.data.icon;
        label.text = item?.data.displayName ?? "";
    }

    public void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        label = transform.Find("Label").GetComponent<TextMeshProUGUI>();
        
        inventorySystem = GetComponentInParent<InventorySystem>();
        if (inventorySystem == null)
        {
            Debug.LogError("InventorySystem not found in parent hierarchy.");
            return;
        }
        InventorySystem.OnInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        foreach (var item in inventorySystem.inventory)
        {
            Set(item);
        }
    }
    
    private void OnDestroy()
    {
        InventorySystem.OnInventoryChangedEvent -= OnUpdateInventory;
    }
}
