using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WaterState = WaterPuzzleStates;
using SkyState = SkyPuzzleStates;
using CloudState = CloudPuzzleStates;
using PlantState = PlantPuzzleStates;
using AnimalState = AnimalPuzzleStates;

public enum WaterPuzzleStates
{
    DRY,
    RAINY
}
public enum SkyPuzzleStates
{
    DAY,
    NIGHT
}
public enum CloudPuzzleStates
{
    CLOUDY,
    CLEAR
}
public enum PlantPuzzleStates
{
    SCARCE,
    PLENTY
}
public enum AnimalPuzzleStates
{
    FED,
    HUNGRY
}

public class KylieRoom : MonoBehaviour
{
    public WaterState WState { get; private set; }
    public SkyState SState { get; private set; }
    public CloudState CState { get; private set; }
    public PlantState PState { get; private set; }
    public AnimalState AState { get; private set; }
    public GameObject pondwater;
    public GameObject sun;
    public GameObject moon;
    public GameObject clouds;
    public GameObject rabbit;
    public GameObject deadrabbit;
    public GameObject yellowjewel;
    public GameObject redjewel;

    private bool blueJewelCollect;
    private bool yellowJewelCollect;
    private bool redJewelCollect;

    private void Start()
    {
        WState = WaterState.RAINY;
        SState = SkyState.DAY;
        CState = CloudState.CLOUDY;
        PState = PlantState.SCARCE;
        AState = AnimalState.FED;
        redjewel.SetActive(false);
        yellowjewel.SetActive(false);
    }

    private void Update()
    {
        switch (WState)
        {
            case WaterState.DRY:
                pondwater.SetActive(false);
                break;

            case WaterState.RAINY:
                pondwater.SetActive(true);
                break;
        }

        switch (SState)
        {
            case SkyState.DAY:
                moon.SetActive(false);
                sun.SetActive(true);
                if (yellowJewelCollect == false)
                {
                    yellowjewel.SetActive(false);
                }
                break;

            case SkyState.NIGHT:
                moon.SetActive(true);
                sun.SetActive(false);
                if (yellowJewelCollect == false)
                {
                    yellowjewel.SetActive(true);
                }
                break;
        }

        switch (CState)
        {
            case CloudState.CLOUDY:
                clouds.SetActive(true);
                break;

            case CloudState.CLEAR:
                clouds.SetActive(false);
                break;
        }

        switch (PState)
        {
            case PlantState.SCARCE:
                rabbit.SetActive(false);
                break;

            case PlantState.PLENTY:
                if (AState != AnimalState.HUNGRY)
                {
                    rabbit.SetActive(true);
                }
                break;
        }

        switch (AState)
        {
            case AnimalState.FED:
                deadrabbit.SetActive(false);
                if (redJewelCollect == false)
                {
                    redjewel.SetActive(false);
                }
                break;

            case AnimalState.HUNGRY:
                if (PState == PlantState.PLENTY && SState == SkyState.NIGHT)
                {
                    deadrabbit.SetActive(true);
                    rabbit.SetActive(false);
                    if (redJewelCollect == false)
                    {
                        redjewel.SetActive(true);
                    }
                }
                break;
        }

        if (redJewelCollect == true && blueJewelCollect == true && yellowJewelCollect == true)
        {
            SceneManager.LoadScene("MainRoom");
        }
    }

    public void WaterChangeState(WaterState newState)
    {
        WState = newState;
    }
    public void SkyChangeState(SkyState newState)
    {
        SState = newState;
    }
    public void CloudChangeState(CloudState newState)
    {
        CState = newState;
    }
    public void PlantChangeState(PlantState newState)
    {
        PState = newState;
    }
    public void AnimalChangeState(AnimalState newState)
    {
        AState = newState;
    }

    public void jewelCollect(string color)
    {
        if (color == "blue")
        {
            blueJewelCollect = true;
        }
        else if(color == "red")
        {
            redJewelCollect = true;
        }
        else if (color == "yellow")
        {
            yellowJewelCollect = true;
        }
    }
}
