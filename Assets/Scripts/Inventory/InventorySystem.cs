using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public List<InventoryItem> inventory { get; private set; }
    private PlayerController _controller;
    private BuildingPlacement _buildSettings;
    
    public InventoryItemData weapon { get; private set; }
    
    public InventoryItemData material { get; private set; }
    
    public static event Action OnInventoryChangedEvent;
    
    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _buildSettings = GetComponent<BuildingPlacement>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        inventory = new List<InventoryItem>();
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        var value = _itemDictionary.GetValueOrDefault(referenceData, null);
        return value;
    }
    
    public void Add(InventoryItemData referenceData)
    {
        int amount = referenceData.type == "RESOURCE" ? 30 : 1;
        if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack(amount);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData, amount);
            inventory.Add(newItem);
            _itemDictionary.Add(referenceData, newItem);
            if (referenceData.type == "RESOURCE")
            {
                material = referenceData;
            }
            else
            {
                weapon = referenceData;
                if (referenceData.displayName == "Hammer")
                {
                    _buildSettings.SetHasToolbox(true);
                }
                else
                {
                    _controller.canFire = true;
                }
            }
        }
        Debug.Log("Item added to player inventory");
        OnInventoryChangedEvent?.Invoke();
    }
    
    public void Drop(InventoryItemData referenceData)
    {
        if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            int amount = referenceData.type == "RESOURCE" ? value.stackSize / 2 : 1;
            value.RemoveFromStack(amount);
            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                _itemDictionary.Remove(referenceData);
                if (referenceData.type == "RESOURCE")
                {
                    material = null;
                    _buildSettings.SetHasMaterials(false);
                }
                else
                {
                    weapon = null;
                    _controller.canFire = false;
                    _buildSettings.SetHasToolbox(false);
                }
            }
        }
        Debug.Log($"Removed {referenceData.displayName} from player inventory");
        OnInventoryChangedEvent?.Invoke();
    }
    
    public void Equip(InventoryItemData referenceData)
    {
        Debug.Log($"Equipping {referenceData.displayName}");
        GameObject gunContainer = GameObject.Find("GunContainer");
        var handheld = Instantiate(referenceData.prefab, gunContainer.transform.position, gunContainer.transform.rotation);
        handheld.transform.SetParent(gunContainer.transform);
    }

    public void Unequip()
    {
        GameObject gunContainer = GetComponentInChildren<GunContainer>().gameObject;
        var handheld = gunContainer.transform.GetChild(0);
        Debug.Log($"Unequipping {handheld.name}");
        gunContainer.transform.DetachChildren();
        // handheld.tag = "CanPickUp";
    }
}
