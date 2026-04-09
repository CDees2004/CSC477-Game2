using System;
using System.Collections.Generic;
using UnityEngine;
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

public class KylieRoom : PuzzleManager
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

    private void Start()
    {
        WState = WaterState.RAINY;
        SState = SkyState.DAY;
        CState = CloudState.CLOUDY;
        PState = PlantState.SCARCE;
        AState = AnimalState.FED;
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
                break;

            case SkyState.NIGHT:
                moon.SetActive(true);
                sun.SetActive(false);
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
                rabbit.SetActive(true);
                break;
        }

        switch (AState)
        {
            case AnimalState.FED:
                deadrabbit.SetActive(false);
                break;

            case AnimalState.HUNGRY:
                deadrabbit.SetActive(true);
                rabbit.SetActive(false);
                break;
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
}
