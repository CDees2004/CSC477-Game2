using UnityEngine;
using UnityEngine.UI;
using State = FsmPianoState;
using System.Collections;

public enum FsmPianoState
{
    IDLE,
    PLAYING,
    SOLVED
}

public class PianoUI : MonoBehaviour
{
    public State State { get; private set; }

    [Header("Sequence Display")]
    public Image[] sequenceSlots;
    public Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public Color filledColor = new Color(0.2f, 0.8f, 0.2f, 1f);

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] keyClips;
    public AudioClip wrongClip;

    [Header("References")]
    public Animator doorAnimator;
    public PianoInteract pianoInteract;

    private string[] solution = { "A", "4", "E", "2", "C" };
    private string[] currentSequence = new string[5];
    private int currentIndex = 0;

    private bool inputBlocked = false;

    private void Start()
    {
        ChangeState(State.IDLE);
    }

    private void Update()
    {
        switch (State)
        {
            case State.IDLE:
                // waiting for player to press a key
                break;

            case State.PLAYING:
                // actively inputting sequence, handled via PressKey
                break;

            case State.SOLVED:
                // puzzle complete, nothing to do
                break;
        }
    }

    public void ChangeState(State newState)
    {
        State = newState;

        switch (newState)
        {
            case State.IDLE:
                ResetSequence();
                break;

            case State.PLAYING:
                // nothing extra needed on enter
                break;

            case State.SOLVED:
                if (doorAnimator != null) doorAnimator.Play("DoorOpen");
                if (pianoInteract != null) pianoInteract.SetPuzzleComplete();
                break;
        }
    }

    private IEnumerator BlockInputBriefly()
    {
        inputBlocked = true;
        yield return new WaitForSeconds(0.2f);
        inputBlocked = false;
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(BlockInputBriefly());
        ChangeState(State.IDLE);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log("Focus changed: " + hasFocus);
        if (hasFocus && gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void PressKey(string keyID, int clipIndex)
    {
        if (inputBlocked) return;
        if (State == State.SOLVED) return;
        if (currentIndex >= 5) return;

        if (clipIndex >= 0 && clipIndex < keyClips.Length && keyClips[clipIndex] != null)
            audioSource.PlayOneShot(keyClips[clipIndex]);

        ChangeState(State.PLAYING);

        currentSequence[currentIndex] = keyID;
        sequenceSlots[currentIndex].color = filledColor;
        currentIndex++;

        if (currentIndex == 5)
            CheckSolution();
    }

    private void CheckSolution()
    {
        Debug.Log("Input: " + string.Join(", ", currentSequence));
        Debug.Log("Solution: " + string.Join(", ", solution));
        for (int i = 0; i < 5; i++)
        {
            if (currentSequence[i] != solution[i])
            {
                if (wrongClip != null)
                    audioSource.PlayOneShot(wrongClip);
                Invoke(nameof(WrongSequence), 1.0f);
                return;
            }
        }

        ChangeState(State.SOLVED);
    }

    private void WrongSequence()
    {
        ChangeState(State.IDLE);
    }

    public void ResetSequence()
    {
        currentIndex = 0;
        currentSequence = new string[5];
        foreach (var slot in sequenceSlots)
            if (slot != null) slot.color = emptyColor;
    }
}