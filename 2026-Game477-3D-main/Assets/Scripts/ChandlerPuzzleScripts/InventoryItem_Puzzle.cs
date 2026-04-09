using UnityEngine;

public class InventoryItem_Puzzle : InventoryItem
{
    public string puzzlename;

    // using this item to complete the puzzle 
    public override void Use()
    {
        print("used puzzle item"); 
        GameManager.Instance.MarkPuzzleComplete(puzzlename); 
    }
}
