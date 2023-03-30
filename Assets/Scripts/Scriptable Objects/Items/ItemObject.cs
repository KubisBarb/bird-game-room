using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    Default,
    Resource,
    Decoration
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemCategory type;
    [TextArea(15,20)]
    public string description;
}
