using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // each puzzle needs some unique ID 
    // set in inspector 
    public string puzzleName; 
    public bool isCompleted { get; private set; }

    public void CompletePuzzle()
    {
        if (isCompleted) return;

        isCompleted = true;
        GameManager.Instance.MarkPuzzleComplete(puzzleName); 
    }

}
