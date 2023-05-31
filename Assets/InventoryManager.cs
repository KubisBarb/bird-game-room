using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private int gems;
    private const string GemsKey = "Gems";

    public InventoryObject resourceInventory;
    public InventoryObject decorationInventory;

    private void Awake()
    {
        // Load the coins value from PlayerPrefs when the game starts
        gems = PlayerPrefs.GetInt(GemsKey);
    }

    private void OnDestroy()
    {
        // Save the coins value to PlayerPrefs when the game closes or object is destroyed
        PlayerPrefs.SetInt(GemsKey, gems);
        PlayerPrefs.Save();
    }

    public void LootLocation(Location location, BirdObject bird)
    {
        InventoryObject lootFromLocation = location.CalculateLoot();    // This the the loot location has produced
        lootFromLocation = GetSortedInventoryByRarity(lootFromLocation, location.lootTable);    // These are the same items but sorted by rarity

        InventoryObject finalLoot = ScriptableObject.CreateInstance<InventoryObject>();

        // We pick the best items intelligence times

        for (int j = 0; j < bird.intelligence; j++)
        {
            if (bird.capacity - 1 > j)
            {
                if (lootFromLocation.Container[0].amount > 0)
                {
                    finalLoot.AddItem(lootFromLocation.Container[0].item, 1);
                    lootFromLocation.RemoveItem(lootFromLocation.Container[0].item, 1);
                }
                else
                {
                    Debug.Log("Empty inventory slot found.");
                }
            }
            else
            {
                Debug.Log("Birds intelligence is higher that it's capacity");
            }
        }

        // We mix the remaining items and pick the rest of it randomly

        List<ItemObject> mixedLoot = new List<ItemObject>();

        foreach (InventorySlot slot in lootFromLocation.Container)
        {
            for (int j = 0; j < slot.amount; j++)
            {
                mixedLoot.Add(slot.item);
            }
        }

        // Check if we don't have too many items in inventory already

        if (finalLoot.GetTotalItemCount(finalLoot) != bird.intelligence)
        {
            Debug.Log("Birds intelligence picked more items than allowed. Difference: " + (finalLoot.GetTotalItemCount(finalLoot) != bird.intelligence).ToString());
        }
        else
        {
            // We choose randomly what to add

            if (mixedLoot.Count > 0 && finalLoot.GetTotalItemCount(finalLoot) < bird.capacity)
            {
                for (int i = 0; i < bird.capacity - bird.intelligence; i++)
                {
                    var randomItem = GetRandomItem(mixedLoot);
                    finalLoot.AddItem(randomItem, 1);
                    mixedLoot.Remove(randomItem);
                }
            }
        }

        ReceiveLoot(finalLoot);
    }

    public InventoryObject GetSortedInventoryByRarity(InventoryObject inventoryToSort, LootTable lootTable)
    {
        InventoryObject sortedInventory = ScriptableObject.CreateInstance<InventoryObject>();
        sortedInventory.Container = inventoryToSort.Container;

        sortedInventory.Container.Sort((a, b) => GetItemRarity(lootTable, a.item).CompareTo(GetItemRarity(lootTable, b.item)));

        return sortedInventory;
    }

    private float GetItemRarity(LootTable lootTable, ItemObject item)
    {
        return lootTable.GetRarityByItem(item);
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
            Debug.Log("Removing pack of stuff from inventory");
            resourceInventory.RemoveItem(slot.item, slot.amount);
        }
    }

    public ItemObject GetRandomItem<ItemObject>(List<ItemObject> list)
    {
        if (list.Count == 0)
        {
            return default(ItemObject);
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
