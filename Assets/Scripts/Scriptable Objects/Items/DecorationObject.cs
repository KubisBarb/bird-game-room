using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decoration Object", menuName = "Inventory System/Items/Decoration")]
public class DecorationObject : ItemObject
{
    public string displayedName;
    public bool obtained;
    public GameObject GameModel;
    public GameObject initialPosition;
    //silhouette pic
    //normal pic
    //descriptioon
    //color variations
    //3D model prefab
    //required task
    //position?

    public void Awake()
    {
        type = ItemCategory.Decoration;
    }
}
