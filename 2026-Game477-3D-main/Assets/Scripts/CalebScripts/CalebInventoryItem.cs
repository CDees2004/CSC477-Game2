using UnityEngine;

public class CalebInventoryItem : InventoryItem
{
    public GameObject console;
    public override void Use()
    {
        print("shouldn't see this one either");
    }
} 