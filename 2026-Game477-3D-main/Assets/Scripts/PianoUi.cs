using UnityEngine;
using UnityEngine.UI;

public class PianoUI : MonoBehaviour
{
    [Header("Sequence Display")]
    public Image[] sequenceSlots;
    public Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public Color filledColor = new Color(0.2f, 0.8f, 0.2f, 1f);

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] keyClips;
    public AudioClip wrongClip;

    private string[] solution = { "A", "4", "E", "2", "C" };
    private string[] currentSequence = new string[5];
    private int currentIndex = 0;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ResetSequence();
    }

    public void PressKey(string keyID, int clipIndex)
    {
        if (currentIndex >= 5) return;

        // play note
        if (clipIndex >= 0 && clipIndex < keyClips.Length && keyClips[clipIndex] != null)
            audioSource.PlayOneShot(keyClips[clipIndex]);

        currentSequence[currentIndex] = keyID;
        sequenceSlots[currentIndex].color = filledColor;
        currentIndex++;

        if (currentIndex == 5)
            CheckSolution();
    }

    private void CheckSolution()
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentSequence[i] != solution[i])
            {
                if (wrongClip != null)
                    audioSource.PlayOneShot(wrongClip);
                Invoke(nameof(ResetSequence), 1.0f);
                return;
            }
        }

        // correct! add puzzleManager call here later
        Debug.Log("Puzzle complete!");
    }

    public void ResetSequence()
    {
        currentIndex = 0;
        currentSequence = new string[5];
        foreach (var slot in sequenceSlots)
            if (slot != null) slot.color = emptyColor;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}