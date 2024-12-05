using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    
    public void OnHandlePickupItem(InventorySystem playerInventory)
    {
        playerInventory.Add(referenceItem);
        playerInventory.Equip(referenceItem);
    }
}
