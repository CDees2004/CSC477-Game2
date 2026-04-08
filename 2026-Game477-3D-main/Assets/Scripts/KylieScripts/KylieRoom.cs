using System;
using System.Collections.Generic;
using UnityEngine;
using WaterState = WaterPuzzleStates;
using SkyState = SkyPuzzleStates;

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

public class KylieRoom : PuzzleManager
{
    public WaterState WState { get; private set; }
    public SkyState SState { get; private set; }
    public GameObject pondwater;
    public GameObject sun;
    public GameObject moon;

    private void Start()
    {
        WState = WaterState.RAINY;
        SState = SkyState.DAY;
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
    }

    public void WaterChangeState(WaterState newState)
    {
        WState = newState;
    }
    public void SkyChangeState(SkyState newState)
    {
        SState = newState;
    }
}
