using State = evilLabStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;


public enum evilLabStates
{
	IDLE,
	GREEN,
	BLUE,
	RED,
	CLEAR,
}

public class evilLabPuzzleFSM : MonoBehaviour
{
	public State myState { get; private set; }
	public List<State> sequence = new List<State>();
	private List<State> key = new List<State>();
	public GameObject emerald;

	//private bool seqSuccess = false;

	private void Awake()
	{
		myState = State.IDLE;

		key.Add(State.GREEN);
		key.Add(State.BLUE);
		key.Add(State.RED);

	}


	public void ChangeState(State newState)
	{
		print(newState); 
		if (newState != State.RED || newState != State.CLEAR)
		{
			myState = newState;
			sequence.Add(newState);

			bool inaccuracy1 = sequence.Except(key).Any();
			bool inaccuracy2 = key.Except(sequence).Any();

			if (sequence.Count >= 3 && (inaccuracy1 || inaccuracy2))
			{
				print("sequence:");
				print(sequence[0]);
				print(sequence[1]);
				print(sequence[2]);
				print("key");
				print(key[0]);
                print(key[1]);
                print(key[2]);
				sequence.Clear();
				myState = State.IDLE;
				SceneManager.LoadScene("CalebRoom");
			} else if (sequence.Count >= 3 && !inaccuracy1 && !inaccuracy2)
			{
				emerald.SetActive(true);
				myState = State.CLEAR;
			}

		}
		
	}

}
