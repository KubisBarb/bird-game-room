using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject resourceInventory;

    /*public void AddToInventory(ItemObject _item, int _amount)
    {
        Debug.Log("ManagerAddedToInventory");
        resourceInventory.AddItem(_item, _amount);
    }*/

    public void LootLocation(Location location)
    {
        InventoryObject lootPack = location.CalculateLoot();
        ReceiveLoot(lootPack);
    }

    public void ReceiveLoot(InventoryObject calculatedLoot)
    {
        foreach (InventorySlot slot in calculatedLoot.Container)
        {
            Debug.Log("Received pack of stuff");
            resourceInventory.AddItem(slot.item, slot.amount);
        }
    }
}
