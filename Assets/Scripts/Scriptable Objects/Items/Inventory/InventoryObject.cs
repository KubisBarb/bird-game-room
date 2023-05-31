using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                hasItem = true;
                if (_item.type != ItemCategory.Decoration) //checks if the item type isn't decoration so there's no duplicates
                {
                    Container[i].AddAmount(_amount);
                }
                else
                {
                    Debug.Log("Duplicate of decoration! Item not added to inventory");
                }
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

    public bool RemoveItemCheck(ItemObject item, int amount)
    {
        int problems = 1;

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == item)
            {
                if (!(Container[i].amount >= amount))
                {
                    Debug.Log("Not enough " + item.name + " in inventory!");
                    problems++;
                }
                else
                {
                    problems--;
                }
            }
        }

        if (problems > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RemoveItem(ItemObject item, int amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == item)
            {
                if (Container[i].amount >= amount)
                {
                    Container[i].RemoveAmount(amount);
                    if (Container[i].amount == 0)
                    {
                        Container.RemoveAt(i);
                    }
                }
                else
                {
                    Debug.Log("Not enough " + item.name + " in inventory!");
                }
            }
        }
    }

    public int GetMaterialAmount(ItemObject objectToSearch)
    {
        foreach (InventorySlot slot in Container)
        {
            if (slot.item == objectToSearch)
            {
                return slot.amount;
            }
        }

        Debug.Log("Material not found in inventory");
        return 0;
    }

    public int GetTotalItemCount(InventoryObject inventory)
    {
        int totalCount = 0;

        foreach (InventorySlot slot in inventory.Container)
        {
            totalCount += slot.amount;
        }

        return totalCount;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
