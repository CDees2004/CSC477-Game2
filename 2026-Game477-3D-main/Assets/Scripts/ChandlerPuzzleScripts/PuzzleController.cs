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

    // a list of book objects
    public Books[] slots;
    // thing we spawn that is used to complete the game 
    public GameObject winObject; 


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void CheckSolved()
    {
        foreach (var slot in slots)
        {
            if (!slot.isFilled)
                return; 
        }
    }

    private void Solve()
    {
        winObject.SetActive(true);
        print("Chandler puzzle win obj spawned");
    }

    public void ChangeState(PuzzleState newState)
    {
        // no redundant swaps 
        //if (PuzzleState == newState) return;

        //switch (PuzzleState)
        //{

        //}
    }



}
