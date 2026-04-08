using UnityEngine;
using TMPro;

public class WordButtons : MonoBehaviour
{
    public KylieRoom room;
    public TMP_Text buttonText;
    public void Grab()
    {
        print(buttonText.text);
        if (buttonText.text == "dry")
        {
            room.WaterChangeState(WaterPuzzleStates.DRY);
        }
        else if (buttonText.text == "rainy")
        {
            room.WaterChangeState(WaterPuzzleStates.RAINY);
        }
        else if (buttonText.text == "sun")
        {
            room.SkyChangeState(SkyPuzzleStates.DAY);
        }
        else if (buttonText.text == "moon")
        {
            room.SkyChangeState(SkyPuzzleStates.NIGHT);
        }
    }
}
