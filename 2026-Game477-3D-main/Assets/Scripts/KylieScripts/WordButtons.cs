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
            KylieSoundManager.Play(CustomSoundType.RAIN);
        }
        else if (buttonText.text == "rainy")
        {
            room.WaterChangeState(WaterPuzzleStates.RAINY);
            KylieSoundManager.Play(CustomSoundType.RAIN);
        }
        else if (buttonText.text == "sun")
        {
            room.SkyChangeState(SkyPuzzleStates.DAY);
            KylieSoundManager.Play(CustomSoundType.DAYCYCLE);
        }
        else if (buttonText.text == "moon")
        {
            room.SkyChangeState(SkyPuzzleStates.NIGHT);
            KylieSoundManager.Play(CustomSoundType.DAYCYCLE);
        }
        else if (buttonText.text == "cloudy")
        {
            room.CloudChangeState(CloudPuzzleStates.CLOUDY);
            KylieSoundManager.Play(CustomSoundType.CLOUDS);
        }
        else if (buttonText.text == "clear")
        {
            room.CloudChangeState(CloudPuzzleStates.CLEAR);
            KylieSoundManager.Play(CustomSoundType.CLOUDS);
        }
        else if (buttonText.text == "plenty")
        {
            room.PlantChangeState(PlantPuzzleStates.PLENTY);
            KylieSoundManager.Play(CustomSoundType.BUNNY);
        }
        else if (buttonText.text == "scarce")
        {
            room.PlantChangeState(PlantPuzzleStates.SCARCE);
            KylieSoundManager.Play(CustomSoundType.BUNNY);
        }
        else if (buttonText.text == "fed")
        {
            room.AnimalChangeState(AnimalPuzzleStates.FED);
            KylieSoundManager.Play(CustomSoundType.EATEN);
        }
        else if (buttonText.text == "hungry")
        {
            room.AnimalChangeState(AnimalPuzzleStates.HUNGRY);
            KylieSoundManager.Play(CustomSoundType.EATEN);
        }
    }
}
