using UnityEngine;

public class InventoryItem_Emerald : InventoryItem
{

    public string puzzlename;

    public override void Use()
    {
        print("chaos control");
        GameManager.Instance.MarkPuzzleComplete(puzzlename);
    }
}
