using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location Object", menuName = "Location System/Location")]
public class Location : LocationObject
{
    public InventoryObject CalculateLoot()
    {
        // Creates a temporary inventory for the items location produced
        //InventoryObject lootPack = new InventoryObject();
        InventoryObject lootPack = ScriptableObject.CreateInstance<InventoryObject>();

        foreach (LootOption option in lootTable.lootOptions)
        {
            // Calculates change of item being included in the output list
            float randomChance = Random.Range(0f, 1f);

            if (randomChance <= option.rarityChance)
            {
                // If successful, calculates quantity of that item and adds it to the inventory
                int randomAmount = Random.Range(option.minQuantity, option.maxQuantity + 1);
                lootPack.AddItem(option.resourceObject, randomAmount);
            }
            
        }

        return lootPack;
    }
}
