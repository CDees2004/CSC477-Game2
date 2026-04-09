using UnityEngine;

public class InventoryItem_WinPuzzle : InventoryItem
{
    // using this item to complete the puzzle 
    public override void Use()
    {
        print("used puzzle item"); 
        GameManager.Instance.MarkPuzzleComplete("ChandlerPuzzle"); 
    }
}
