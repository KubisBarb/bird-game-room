using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceFamily
{
    Yarn,
    Feather,
    Pottery,
    Paper,
    Bamboo,
    Coffee,
    Plant
}

[CreateAssetMenu(fileName = "New Resource Object", menuName = "Inventory System/Items/Resource")]
public class ResourceObject : ItemObject
{
    public Sprite icon;
    public string displayedName;
    public ResourceFamily resourceFamily;
    //public int quantity;

    public void Awake()
    {
        type = ItemCategory.Resource;
    }
}
