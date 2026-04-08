using UnityEngine;

public enum FsmPuzzleState
{
    Idle, 
    InProgress, 
    Completed,
}

public class ChandlerRoom : PuzzleManager
{
    // each room is a simple switch case FSM that calls 
    // complete room function in superclass 
    private void Start()
    {

    }

    private void Update()
    {

    }
}
