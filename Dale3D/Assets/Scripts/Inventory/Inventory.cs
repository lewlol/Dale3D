using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public void AddItem(Item item, int amount)
    {
        foreach(InventorySlot invS in inventorySlots)
        {
            if(invS.item == item)
            {
                invS.itemAmount += amount;
                return;
            }
        }

        InventorySlot newSlot = new InventorySlot();
        newSlot.item = item;
        newSlot.itemAmount = amount;
        inventorySlots.Add(newSlot);
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int itemAmount;
}
