using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    
    public void OnHandlePickupItem(InventorySystem playerInventory)
    {
        playerInventory.Add(referenceItem);
    }
    
    public void OnHandleDropItem(InventorySystem playerInventory)
    {
        playerInventory.Remove(referenceItem);
    }
}
