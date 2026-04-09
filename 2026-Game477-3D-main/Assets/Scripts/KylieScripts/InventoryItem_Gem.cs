using UnityEngine;

public class InventoryItem_Gem : InventoryItem
{
    public string color;
    public KylieRoom room;
    public void Grab()
    {
        room.jewelCollect(color);
        gameObject.SetActive(false);
        KylieSoundManager.Play(CustomSoundType.GEM);
        //Inventory.Instance.Add(this);
    }
}
