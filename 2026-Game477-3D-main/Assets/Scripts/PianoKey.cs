using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public string keyID;   // "A", "B", "C" ... "1", "2" etc.
    public int clipIndex;  // 0-12
    public PianoUI pianoUI;

    public void OnKeyPressed()
    {
        pianoUI.PressKey(keyID, clipIndex);
    }
}