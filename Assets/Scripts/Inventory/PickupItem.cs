using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public InventorySystem inventorySystem;

    public void Awake()
    {
        if (inventorySystem == null)
        {
            inventorySystem = GetComponent<InventorySystem>();
            if (inventorySystem == null)
            {
                Debug.LogError("InventorySystem not found on player.");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InventoryItem"))
        {
            Debug.Log("Collided with inventory item");
            ItemObject item = other.GetComponent<ItemObject>();
            if (item != null)
            {
                item.OnHandlePickupItem(inventorySystem);
                Destroy(other.gameObject);
            }
        }
    }

    private void Update()
    {
        // If drop item key pressed, call item.OnHandleDropItem
    }
}
