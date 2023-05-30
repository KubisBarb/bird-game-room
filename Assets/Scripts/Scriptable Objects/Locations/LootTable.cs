using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LootTable", menuName = "Location System/Loot Table")]
public class LootTable : ScriptableObject
{
    public List<LootOption> lootOptions = new List<LootOption>();
}

[System.Serializable]
#nullable enable
public class LootOption
{
    public string? resourceName;
    public ItemObject? resourceObject;
    public float rarityChance = 0f;
    public int minQuantity = 0;
    public int maxQuantity = 0;
}
#nullable disable