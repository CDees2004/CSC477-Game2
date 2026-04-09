using UnityEngine;
using PuzzleState = FsmPuzzleState;

public enum FsmPuzzleState
{
    NoProgress,
    OneShelfSolved,
    TwoShelvesSolved,
    PuzzleComplete,
}

public class PuzzleController : MonoBehaviour
{
    // singleton 
    public static PuzzleController Instance { get; private set; }
    public PuzzleState PuzzleState { get; private set; }
    // a list of book objects
    public BookshelfSlot[] slots;
    // thing we spawn that is used to complete the game 
    public GameObject winObject;


    private void Awake()
    {
        Instance = this;
    }

    public void CheckSolved()
    {
        int solvedCount = 0;

        foreach (var slot in slots)
        {
            if (!slot.isFilled)
                solvedCount++;
        }

        // handles the actual puzzle state swapping
        switch (solvedCount)
        {
            case 0:
                // really shouldn't come up, should be default
                // added just in case 
                ChangeState(PuzzleState.NoProgress);
                break;

            case 1:
                ChangeState(PuzzleState.OneShelfSolved);
                break;

            case 2:
                ChangeState(PuzzleState.TwoShelvesSolved);
                break;

            case 3:
                ChangeState(PuzzleState.PuzzleComplete);
                break;
        }
    }

    public void ChangeState(PuzzleState newState)
    {
        //no redundant swaps
        if (PuzzleState == newState) return;

        PuzzleState = newState;

        switch (PuzzleState)
        {
            case PuzzleState.NoProgress:
                print("No shelves solved");
                // keep all light objects turned off
                break;

            case PuzzleState.OneShelfSolved:
                print("One shelf solved");
                // turn on one light object
                break;

            case PuzzleState.TwoShelvesSolved:
                print("Two shelves solved");
                // turn on two light objects
                break;

            case PuzzleState.PuzzleComplete:
                print("All shelves solved");
                // turn on all three light objects
                winObject.SetActive(true);
                break;
        }
    }



}
