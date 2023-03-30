using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public InventoryObject resourceInventory;

    public void ClearResourceInventory()
    {
        resourceInventory.Container.Clear();
    }
}
