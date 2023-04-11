using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItem { //all item objects will need the following:
    string Name { get; } //the name of the item
    
    //Sprite Image { get; } we don't need sprites to items right now
    
    void OnPickup(); //called when item is collected by player
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(InventoryItem item)
    {
        Item = item;
    }

    public InventoryItem Item;
}
