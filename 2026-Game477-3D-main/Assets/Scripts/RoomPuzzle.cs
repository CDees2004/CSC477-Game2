using UnityEngine;

public class RoomPuzzle : MonoBehaviour
{
    // each puzzle needs some unique ID 
    public string puzzleName; 
    public bool isCompleted { get; private set; }

    public void CompletePuzzle()
    {
        if (isCompleted) return;

        isCompleted = true;
        GameManager.Instance.MarkPuzzleComplete(puzzleName); 
    }

}
