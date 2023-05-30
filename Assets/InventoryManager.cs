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

    public bool LoseLootCheck(InventoryObject lootToLose)
    {
        bool canBeRemoved = false;

        foreach (InventorySlot slot in lootToLose.Container)
        {
            canBeRemoved = resourceInventory.RemoveItemCheck(slot.item, slot.amount);
        }

        if (canBeRemoved)
        {
            LoseLoot(lootToLose);
            return true;
        }
        else
        {
            Debug.Log("Problem removing items from inventory. Amount might not be anough or some item is not in the inventory at all.");
            return false;
        }
    }

    public void LoseLoot(InventoryObject lootToLose)
    {
        foreach (InventorySlot slot in lootToLose.Container)
        {
            Debug.Log("Removing pack of stuff");
            resourceInventory.RemoveItem(slot.item, slot.amount);
        }
    }
}
