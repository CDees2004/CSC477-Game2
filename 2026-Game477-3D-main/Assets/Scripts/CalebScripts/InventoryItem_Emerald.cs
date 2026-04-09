using UnityEngine;

public class InventoryItem_Emerald : InventoryItem
{
    public override void Use()
    {
        print("chaos control");
        GameManager.Instance.MarkPuzzleComplete("CalebPuzzle");
    }
}
