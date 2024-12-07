using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private PlayerController _controller;
    private BuildingPlacement _buildSettings;
    
    public Dictionary<InventoryItemData, InventoryItem> itemDictionary { get; private set; }
    public InventoryItemData weapon { get; private set; }
    
    public static event Action OnInventoryChangedEvent;
    
    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _buildSettings = GetComponent<BuildingPlacement>();
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        
        ItemObject item = GetComponentInChildren<ItemObject>();
        Debug.Log($"Weapon: {item.referenceItem.displayName}");
        AddWeapon(item.referenceItem);
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        var value = itemDictionary.GetValueOrDefault(referenceData, null);
        return value;
    }

    public void AddWeapon(InventoryItemData referenceData)
    {
        InventoryItem newItem = new InventoryItem(referenceData);
        itemDictionary.Add(referenceData, newItem);
        weapon = referenceData;
        if (referenceData.displayName == "Axe")
        {
            _buildSettings.SetHasToolbox(true);
        }
        else
        {
            _controller.canFire = true;
        }
        Debug.Log($"Added {referenceData.displayName} to player inventory");
        OnInventoryChangedEvent?.Invoke();
    }

    public void RemoveWeapon(InventoryItemData referenceData)
    {
        weapon = null;
        _controller.canFire = false;
        _buildSettings.SetHasToolbox(false);
        itemDictionary.Remove(referenceData);
        Debug.Log($"Removed {referenceData.displayName} from player inventory");
        OnInventoryChangedEvent?.Invoke();
    }
    
    public void Equip(InventoryItemData referenceData)
    {
        Debug.Log($"Equipping {referenceData.displayName}");
        GameObject gunContainer = GameObject.Find("GunContainer");
        Quaternion rotation = referenceData.displayName == "Axe" ? referenceData.prefab.transform.rotation : gunContainer.transform.rotation;
        var handheld = Instantiate(referenceData.prefab, gunContainer.transform.position, rotation);
        handheld.transform.SetParent(gunContainer.transform);
    }

    public void Unequip()
    {
        GameObject gunContainer = GetComponentInChildren<GunContainer>().gameObject;
        var handheld = gunContainer.transform.GetChild(0);
        Debug.Log($"Unequipping {handheld.name}");
        gunContainer.transform.DetachChildren();
        handheld.tag = "CanPickUp";
    }
}
