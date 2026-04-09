using UnityEngine;

public class InventoryItem_PuzzleBook : InventoryItem
{
    public string bookID;

    public override void Use()
    {
        var slot = BookshelfInteract.Instance.currentBookSlot; 

        if(slot == null)
        {
            print("No shelf selected");
            return;
        }

        bool placed = slot.TryPlace(bookID);

        if (placed)
        {
            print("Book placed");
            // remove from inventory
            gameObject.SetActive(false);
        }
        else
        {
            print("Incorrect Slot");
        }
    }
}
