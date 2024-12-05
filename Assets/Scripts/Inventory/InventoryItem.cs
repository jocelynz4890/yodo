using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }
    

    public InventoryItem(InventoryItemData source, int amount = 1)
    {
        data = source;
        AddToStack(amount);
    }
    
    public void AddToStack(int amount = 1)
    {
        stackSize += amount;
    }
    
    public void RemoveFromStack(int amount = 1)
    {
        // stackSize -= (min amount, stackSize)
        stackSize -= amount;
    }
}
