using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocationObject : ScriptableObject
{
    [Header("Basic Settings")]
    public GameObject prefab;
    public float searchDurationMinutes;

    [Space(10)]
    public string[] birdSpeciesRequirement;
    [Header("Loot Table")]
    public LootTable lootTable;

    [Space(10)]
    [Header("Extra")]
    [TextArea(5, 20)]
    public string description;

    public Sprite icon;
    public GameObject panelPrefab;

}

