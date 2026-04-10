using UnityEngine;

public class PuzzleCompletionObject : MonoBehaviour
{
    public string puzzleName; // set in Inspector, must match what you pass to MarkPuzzleComplete

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsPuzzleComplete(puzzleName))
            Destroy(gameObject);
    }
}