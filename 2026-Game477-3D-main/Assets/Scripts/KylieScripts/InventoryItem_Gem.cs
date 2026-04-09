using UnityEngine;

public class InventoryItem_Gem : InventoryItem
{
    public string color;
    public KylieRoom room;
    public void Grab()
    {
        room.jewelCollect(color);
        gameObject.SetActive(false);
        //Inventory.Instance.Add(this);
    }
}
