using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    
    public void OnHandlePickupItem(InventorySystem playerInventory)
    {
        playerInventory.AddWeapon(referenceItem);
        playerInventory.Equip(referenceItem);
    }
}
