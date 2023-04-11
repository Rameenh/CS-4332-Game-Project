using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 1; //only allow the player to have 1 item for now

    private List<InventoryItem> listOfItems = new List<InventoryItem>(); //our list of items

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public void AddItem(InventoryItem item)
    {
        if(listOfItems.Count < SLOTS)//ensures no overflow of carried items
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if(collider.enabled)
            {
                collider.enabled = false;

                listOfItems.Add(item);

                item.OnPickup();
                
                if(ItemAdded != null){
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
}
