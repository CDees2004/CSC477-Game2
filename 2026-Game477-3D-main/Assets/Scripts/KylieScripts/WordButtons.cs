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
        else if (buttonText.text == "cloudy")
        {
            room.CloudChangeState(CloudPuzzleStates.CLOUDY);
        }
        else if (buttonText.text == "clear")
        {
            room.CloudChangeState(CloudPuzzleStates.CLEAR);
        }
        else if (buttonText.text == "plenty")
        {
            room.PlantChangeState(PlantPuzzleStates.PLENTY);
        }
        else if (buttonText.text == "scarce")
        {
            room.PlantChangeState(PlantPuzzleStates.SCARCE);
        }
        else if (buttonText.text == "fed")
        {
            room.AnimalChangeState(AnimalPuzzleStates.FED);
        }
        else if (buttonText.text == "hungry")
        {
            room.AnimalChangeState(AnimalPuzzleStates.HUNGRY);
        }
    }
}
