using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public ItemObject theItem;
    public PlayerBehaviour player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }
    public void OnMouseDown()
    {
        player.resourceInventory.AddItem(theItem, 1);
        Destroy(this.gameObject);
        
    }

}
